using AutoMapper;
using Domain.Contracts;
using Domain.Contracts.SpecificationContracts;
using Domain.Entities;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.IdentityModel.Tokens;
using Service.Exception_Implementation.NotFoundExceptions;
using Service.Specification_Implementation.CarOwnerSpecifications;
using Service.Specification_Implementation.RequestSpecifications;
using Service.Specification_Implementation.TechnicianSpecifications;
using ServiceAbstraction.CoreServicesAbstractions.Admin;
using Services.Specification_Implementation.Emergency;
using SharedData.DTOs.Admin.TechnicianCategory;
using SharedData.DTOs.Admin.Users;
using SharedData.Enums;

namespace Service.CoreServices.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AdminService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }





        #region Private Helpers

        private async Task<List<ReadUsersDTO>> GetUsersAsync<TEntity, TKey>(ISpecification<TEntity, TKey> spec, string notFoundMessage)
            where TEntity : BaseEntity<TKey>
        {
            var repo = unitOfWork.GetRepository<TEntity, TKey>();
            var users = await repo.GetAllAsync(spec);

            if (users == null || !users.Any())
                throw new UsersNotFoundException(notFoundMessage);

            return mapper.Map<List<ReadUsersDTO>>(users);
        }

        private async Task SetUserActivationAsync<TEntity>(ISpecification<TEntity, int> spec, int userId,

            string notFoundMessage,
            Action<TEntity> setActivation)
            where TEntity : BaseEntity<int>
        {
            if (userId <= 0) throw new UsersNotFoundException(notFoundMessage);

            var repo = unitOfWork.GetRepository<TEntity, int>();
            var user = await repo.GetByIdAsync(spec);
            if (user == null) throw new UsersNotFoundException(notFoundMessage);

            setActivation(user);
            repo.Update(user);
            await unitOfWork.SaveChangesAsync();

        }




        private async Task<TCategory> GetCategoryByIdAsync(int id)
        {
            var repo = unitOfWork.GetRepository<TCategory, int>();
            var category = await repo.GetByIdAsync(id);
            if (category == null)
                throw new CategoriesNotFoundException("there is no category with this id");

            return category;
        }

        #endregion

        #region Users
        public async Task<object> GetUsersCountAsync()
        {
            var techniciansCount = await unitOfWork
                .GetRepository<Technician, int>()
                .CountAsync(new TechnicianWithUserAndReviewsSpec());

            var carOwnersCount = await unitOfWork
                .GetRepository<CarOwner, int>()
                .CountAsync(new CarOwnerWithUserAndReviewsSpec());

            return new
            {
                TechniciansCount = techniciansCount,
                CarOwnersCount = carOwnersCount
            };
        }

        public Task<List<ReadUsersDTO>> GetAllCarOwnersAsync()
            => GetUsersAsync(new CarOwnerWithUserAndReviewsSpec(), "there is no car owners");

        public Task<List<ReadUsersDTO>> GetAllTechniciansAsync()
            => GetUsersAsync(new TechnicianWithUserAndReviewsSpec(), "there is no technicians");

        public Task BanTechnianAsync(int userId)
            => SetUserActivationAsync<Technician>(new TechnicianWithUserAndReviewsSpec(userId), userId, "there is no technician with this id", t => t.ApplicationUser.IsActivated = false);

        public Task BanCarOwnerAsync(int userId)
            => SetUserActivationAsync<CarOwner>(new CarOwnerWithUserAndReviewsSpec(userId), userId, "there is no car owner with this id", c => c.ApplicationUser.IsActivated = false);

        public Task ActivateTechnianAsync(int userId)
            => SetUserActivationAsync<Technician>(new TechnicianWithUserAndReviewsSpec(userId), userId, "there is no technician with this id", t => t.ApplicationUser.IsActivated = true);

        public Task ActivateCarOwonerAsync(int userId)
            => SetUserActivationAsync<CarOwner>(new CarOwnerWithUserAndReviewsSpec(userId), userId, "there is no car owner with this id", c => c.ApplicationUser.IsActivated = true);

        #endregion

        #region Categories

        public async Task<List<ReadTCategoryDTO>> GetAllCategoriesAsync()
        {
            var spec = new CategoriesByNameSpec();
            var repo = unitOfWork.GetRepository<TCategory, int>();
            var categories = await repo.GetAllAsync(spec);

            if (categories == null || !categories.Any())
                throw new CategoriesNotFoundException("there is no categories");

            return mapper.Map<List<ReadTCategoryDTO>>(categories);
        }

        public async Task CreateCategoryAsync(CreateTCategoryDTO dto)
        {
            var entity = mapper.Map<TCategory>(dto);
            var repo = unitOfWork.GetRepository<TCategory, int>();
            await repo.AddAsync(entity);
            await unitOfWork.SaveChangesAsync();


        }

        public async Task UpdateCategoryAsync(int id, UpdateTCategoryDTO dto)
        {
            var category = await GetCategoryByIdAsync(id);
            category.Name = dto.Name;

            unitOfWork.GetRepository<TCategory, int>().Update(category);
            await unitOfWork.SaveChangesAsync();

        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);
            category.IsDeleted = true;

            unitOfWork.GetRepository<TCategory, int>().Update(category);
            await unitOfWork.SaveChangesAsync();

        }

        public async Task<object> GetrequestsCountAsync()
        {
            var requestRepo = unitOfWork.GetRepository<EmergencyRequest, int>();

            var allRequestsCount = await requestRepo.CountAsync(new EmergencyRequestTotalSpecification());

            var waitingRequestsCount = await requestRepo.CountAsync(new WaitingRequestSpecification());

            return new
            {
                AllRequestsCount = allRequestsCount,
                WaitingRequestsCount = waitingRequestsCount
            };
        }



        #endregion
        public async Task<object> GetDashboardStatisticsAsync()
        {
            var techRepo = unitOfWork
               .GetRepository<Technician, int>();
            var techniciansCount = await techRepo
               .CountAsync(new TechniciansSpecification());

            var carOwnerRepo = unitOfWork
                .GetRepository<CarOwner, int>();
            var carOwnersCount = await carOwnerRepo
                .CountAsync(new CarOwnerSpecification());

            var totalUsers = techniciansCount + carOwnersCount;
            var firstDayOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            var newTechCount = await techRepo.CountAsync(new TechnicianWithAppUserSpec(firstDayOfMonth));
            var newCarOwnerCount = await carOwnerRepo.CountAsync(new CarOwnerSpecification(firstDayOfMonth));
            var newUsersThisMonth = newTechCount + newCarOwnerCount;
            var percentageNewThisMonth = totalUsers == 0 ? 0 : (double)newUsersThisMonth / totalUsers * 100;
            var Technicians = await techRepo.GetAllWithSpecAsync(new TechnicianWithUserAndReviewsSpec());
            var avgTechRate = Technicians.Any(t => t.reviews.Any()) ?
                Math.Round(Technicians.Where(t => t.reviews.Any()).Average(t => t.reviews.Average(r => r.Rate)), 2)
                : 0;





            double GetUserPercent(int count) =>
            totalUsers == 0 ? 0 : Math.Round((count / (double)totalUsers) * 100, 2);

            // --- Requests ---
            var totalRequests = await unitOfWork.GetRepository<EmergencyRequestTechnicians, int>().CountAsync(new EmergencyRequestTechnicanSpecefication());
            var completedCount = await unitOfWork.GetRepository<EmergencyRequestTechnicians, int>().CountAsync(new EmergencyRequestTechnicanSpecefication(RequestState.Completed));
            var waitingCount = await unitOfWork.GetRepository<EmergencyRequestTechnicians, int>().CountAsync(new EmergencyRequestTechnicanSpecefication(RequestState.Waiting));
            var activeCount = await unitOfWork.GetRepository<EmergencyRequestTechnicians, int>().CountAsync(new EmergencyRequestTechnicanSpecefication(RequestState.Answered));
            var canceledCount = await unitOfWork.GetRepository<EmergencyRequestTechnicians, int>().CountAsync(new EmergencyRequestTechnicanSpecefication(RequestState.Cancelled));

            double GetRequestPercent(int count) =>
            totalRequests == 0 ? 0 : Math.Round((count / (double)totalRequests) * 100, 2);




            return new
            {
                Users = new
                {
                    Technicians = new { Count = techniciansCount, Percent = GetUserPercent(techniciansCount) },
                    CarOwners = new { Count = carOwnersCount, Percent = GetUserPercent(carOwnersCount) },
                    NewUsers = percentageNewThisMonth,
                    Rates = avgTechRate
                },
                Requests = new
                {
                    Completed = new { Count = completedCount, Percent = GetRequestPercent(completedCount) },
                    Waiting = new { Count = waitingCount, Percent = GetRequestPercent(waitingCount) },
                    Active = new { Count = activeCount, Percent = GetRequestPercent(activeCount) },
                    Canceled = new { Count = canceledCount, Percent = GetRequestPercent(canceledCount) }
                }
            };
        }
    }
}
