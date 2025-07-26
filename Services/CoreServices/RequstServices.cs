using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.AspNetCore.Http.HttpResults;
using Service.Exception_Implementation;
using Service.Exception_Implementation.AlreadyFound;
using Service.Exception_Implementation.ArgumantNullException;
using Service.Exception_Implementation.BadRequestExceptions;
using Service.Exception_Implementation.NotFoundExceptions;
using Service.Specification_Implementation;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.RequestsDTOs;
using SharedData.DTOs.TechnicianDTOs;
using SharedData.Enums;

namespace Service.CoreServices
{
    public class RequstServices : IRequestServices
    {
        private readonly ITechnicianService technicianService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public RequstServices(IUnitOfWork _unitOfWork,
            IMapper _mapper,
            ITechnicianService technicianService)
        {

            unitOfWork = _unitOfWork;
            mapper = _mapper;
            this.technicianService = technicianService;
        }

        public async Task CancelAll(int CarOwnerID)
        {
            var spec = new CancelledRquestSpecification(CarOwnerID);
            var emergencyRequests = await unitOfWork.EmergencyRequestRepository.GetAllAsync(spec);
            if (emergencyRequests == null || !emergencyRequests.Any())
            {
                throw new RequestNotFoundException();
            }
            foreach (var emergencyRequest in emergencyRequests)
            {
                if (emergencyRequest.CallStatus == RequestState.Waiting)
                {
                    emergencyRequest.CallStatus = RequestState.Cancelled;
                    await unitOfWork.EmergencyRequestRepository.UpdateAsync(emergencyRequest);
                }
                if (emergencyRequest.CallStatus == RequestState.Answered && emergencyRequest.EmergencyRequests.IsCompleted == false)
                {
                    emergencyRequest.CallStatus = RequestState.Cancelled;
                    emergencyRequest.EmergencyRequests.EndTimeStamp = DateTime.UtcNow;
                    await unitOfWork.EmergencyRequestRepository.UpdateAsync(emergencyRequest);
                }
            }
            await unitOfWork.SaveChangesAsync();

        }

        public async Task CompleteRequest(int requestId)
        {
            var emergencyRequest = await unitOfWork.GetRepository<EmergencyRequest, int>().GetByIdAsync(requestId);
            if (emergencyRequest == null)
            {
                throw new RequestNotFoundException();
            }
            if (emergencyRequest.IsCompleted)
            {
                throw new RequestAlreadyCompletedException();
            }

            emergencyRequest.IsCompleted = true;
            emergencyRequest.EndTimeStamp = DateTime.UtcNow;
            emergencyRequest.CompeletRequestDate = DateOnly.FromDateTime(DateTime.UtcNow);
            unitOfWork.GetRepository<EmergencyRequest, int>().Update(emergencyRequest);
            await unitOfWork.SaveChangesAsync();
            var emergencyRequestTechnicians = await unitOfWork.GetRepository<EmergencyRequestTechnicians, int>().GetByIdAsync(requestId);
            if (emergencyRequestTechnicians != null)
            {
                emergencyRequestTechnicians.CallStatus = RequestState.Completed;
                unitOfWork.EmergencyRequestRepository.UpdateAsync(emergencyRequestTechnicians);
                await unitOfWork.SaveChangesAsync();
            }

        }

        public async Task CreateRealRequest(RealRequestDTO request)
        {       
            bool isPresent = await IsPresent(request);
            if (!isPresent)
            {
                var carOwnerRepo = unitOfWork.GetRepository<CarOwner, int>();
                var specification = new CarOwnerUserPinSpecification(request);
                var owner = await carOwnerRepo.GetByIdAsync(specification);
                if (owner == null)
                {
                    throw new CarOwnerNotFoundException();
                }
                if (request.pin == owner.ApplicationUser.PIN)
                {
                    var emergancyRequest = new EmergencyRequest
                    {
                        CarOwnerId = request.CarOwnerId,
                        Description = request.Description,
                        Latitude = request.Latitude,
                        Longitude = request.Longitude,
                        IsCompleted = false,
                        TimeStamp = DateTime.UtcNow,
                        EndTimeStamp = null,
                        categoryId = request.categoryId
                    };
                    await unitOfWork.GetRepository<EmergencyRequest, int>().AddAsync(emergancyRequest);
                    await unitOfWork.SaveChangesAsync();


                    if (request.TechnicianIDs != null && request.TechnicianIDs.Any())
                    {
                        foreach (var technicianId in request.TechnicianIDs)
                        {
                            var emergencyRequestTechnicians = new EmergencyRequestTechnicians
                            {
                                EmergencyRequestId = emergancyRequest.Id,
                                TechnicianId = technicianId,
                                CallStatus = RequestState.Waiting
                            };
                            await unitOfWork.EmergencyRequestRepository.AddAsync(emergencyRequestTechnicians);
                        }
                    }

                    await unitOfWork.SaveChangesAsync();
                }
                else
                {
                    throw new PinCodeBadRequestException();
                }
            }
            else
            {
                throw new RequestAlreadyFoundException();
            }
            }

        public async Task<PreRequestDTO> CreateRequestAsync(CreatePreRequestDTO request)
        {

            var spec = new CarOwnerSpecification(request);
            var user = await unitOfWork.GetRepository<CarOwner, int>().GetByIdAsync(spec);

            if (user == null)
            {
                throw new CarOwnerNotFoundException();
            }
            var filteredTechnicians = await technicianService.GetTechniciansByFilterAsync(request);
            if (filteredTechnicians == null || !filteredTechnicians.Any())
            {
                return new PreRequestDTO { };
            }

            return new PreRequestDTO { Technicians = filteredTechnicians };

        }

        public async Task<EmergencyTechnicianID> EmergencyTechnicianID(int requestId)
        {
            if (requestId <= 0)
            {
                throw new EmergencyNullException();
            }
            var emergencyTechnician = await unitOfWork.GetRepository<EmergencyRequest, int>().GetByIdAsync(requestId);
            if (emergencyTechnician == null)
            {
                throw new RequestNotFoundException();
            }
            if(emergencyTechnician.Technician == null )
            {
                throw new TechnicianNotFoundException();
            }
            var mappedTechnician = mapper.Map<EmergencyTechnicianID>(emergencyTechnician);
            return mappedTechnician;
        }

        public async Task<bool> IsPresent(RealRequestDTO request)
        {
            var spec = new WaitingRequestSpecification(request.CarOwnerId);
            var emergencyRequest = await unitOfWork.GetRepository<EmergencyRequest, int>().GetAllAsync(spec);
            if (emergencyRequest == null || !emergencyRequest.Any())
            {
                return false;
            }
            return true;


        }

        public async Task<List<RequestBreifDTO>> RequestBreifDTOs(int carOwnerID)
        {
            var spec = new RequestBreifSpecification(carOwnerID);
            var emergencyRequests = await unitOfWork.GetRepository<EmergencyRequest, int>().GetAllAsync(spec);
            if (emergencyRequests == null || !emergencyRequests.Any())
            {
                throw new RequestNotFoundException();
            }
            var mappedRequests = mapper.Map<List<RequestBreifDTO>>(emergencyRequests);
            return mappedRequests;
        }

        public async Task<RequestDetailsDTO> RequestDetailsDTOs(int requestId)
        {
            var spec = new RequestDetailsSpecification(requestId);
            var emergencyRequest = await unitOfWork.GetRepository<EmergencyRequest, int>().GetByIdAsync(spec);
            if (emergencyRequest == null)
            {
                throw new RequestNotFoundException();
            }
            string city = await technicianService.GetCity(emergencyRequest.Latitude, emergencyRequest.Longitude);
            if(emergencyRequest.Technician == null)
            {
                throw new RequestDetailsException();
            }
            var mappedRequest = new RequestDetailsDTO()
            {
                City = city,
                Description = emergencyRequest.Description,
                TechnicianName = emergencyRequest.Technician.ApplicationUser.Name,
                CategoryName = emergencyRequest.category.Name,
                RequestDate = emergencyRequest.CompeletRequestDate ?? DateOnly.FromDateTime(DateTime.UtcNow)
            };
            return mappedRequest;
        }
    }
}
