using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Service.Exception_Implementation.NotFoundExceptions;
using ServiceAbstraction.CoreServicesAbstractions.Admin;
using SharedData.DTOs.Admin.TechnicianCategory;
using SharedData.DTOs.Admin.Users;

namespace Service.CoreServices.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        // Users
        public async Task<List<ReadUsersDTO>> GetAllUsersAsync()
        {
            var users = userManager.Users.ToList();
            if (!users.Any()) throw new UsersNotFoundException("no users found");
            var result = new List<ReadUsersDTO>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                result.Add(new ReadUsersDTO
                {
                    Id = user.Id,
                    FullName = user.Name,
                    Role = roles.FirstOrDefault(),
                    IsActivated = user.IsActivated
                });
            }

            return result;
        }

        public async Task<bool> SoftDeleteUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) throw new UsersNotFoundException("no user found with this id");
            user.isDeleted = true;
            await userManager.UpdateAsync(user);
            return true;
        }

        public async Task<bool> RestoreUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new UsersNotFoundException("no user found with this id");
            user.IsActivated = true;
            await userManager.UpdateAsync(user);
            return true;
        }

        // Categories
        public async Task<List<ReadTCategoryDTO>> GetAllCategoriesAsync()
        {
            var repo = unitOfWork.GetRepository<TCategory, int>();
            var categories = await repo.GetAllAsync();
            if (categories == null ||! categories.Any()) throw new CategoriesNotFoundException("there is no categories");
            return categories.Select(c => new ReadTCategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                IsDeleted = c.IsDeleted
            }).ToList();
        }

        public async Task<ReadTCategoryDTO> CreateCategoryAsync(CreateTCategoryDTO dto)
        {
            var repo = unitOfWork.GetRepository<TCategory, int>();
            var entity = new TCategory { Name = dto.Name, IsDeleted = false };
            await repo.AddAsync(entity);
            await unitOfWork.SaveChangesAsync();

            return new ReadTCategoryDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                IsDeleted = entity.IsDeleted
            };
        }

        public async Task<bool> UpdateCategoryAsync(int id, UpdateTCategoryDTO dto)
        {
            var repo = unitOfWork.GetRepository<TCategory, int>();
            var category = await repo.GetByIdAsync(id);
            if (category == null) if (category == null) throw new CategoriesNotFoundException("there is no category with this id");

            category.Name = dto.Name;
            repo.Update(category);
            await unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteCategoryAsync(int id)
        {
            var repo = unitOfWork.GetRepository<TCategory, int>();
            var category = await repo.GetByIdAsync(id);
            if (category == null) if (category == null) throw new CategoriesNotFoundException("there is no category with this id");

            category.IsDeleted = true;
            repo.Update(category);
            await unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RestoreCategoryAsync(int id)
        {
            var repo = unitOfWork.GetRepository<TCategory, int>();
            var category = await repo.GetByIdAsync(id);
            if (category == null) throw new CategoriesNotFoundException("there is no category with this id");

            category.IsDeleted = false;
            repo.Update(category);
            await unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<ReadUsersDTO>> GetAllTechniciansAsync()
        {
            var technicians = await userManager.GetUsersInRoleAsync("Technician");
            return technicians.Select(u => new ReadUsersDTO
            {
                Id = u.Id,
                FullName = u.Name,
                IsActivated = u.IsActivated,
            }).ToList();
        }

        public async Task<List<ReadUsersDTO>> GetAllCarOwnersAsync()
        {
            var carOwners = await userManager.GetUsersInRoleAsync("CarOwner");
            return carOwners.Select(u => new ReadUsersDTO
            {
                Id = u.Id,
                FullName = u.Name,
                IsActivated = u.IsActivated,
               // rate=u

            }).ToList();
        }

        public async Task<bool> BanUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new UsersNotFoundException("no user found with this id");
            user.IsActivated = true;
            await userManager.UpdateAsync(user);
            return true;
        }
    }

}
