using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Service.Exception_Implementation.NotFoundExceptions;
using Service.Specification_Implementation;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs;
using SharedData.DTOs.RequestsDTOs;
using SharedData.Enums;

namespace Service.CoreServices
{
    public class CarOwnerService : ICarOwnerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CarOwnerService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<RequestBreifDTO> IsRequested(int Id)
        {
            var Repo = unitOfWork.GetRepository<EmergencyRequest, int>();
            var spec = new NotCompletedRequestSpecification(Id);
            var notCompletedRequests = await Repo.GetAllAsync(spec);
            if (notCompletedRequests.Any())
            {
                foreach (var request in notCompletedRequests)
                {
                    if (request.EmergencyRequestTechnicians != null)
                    {
                        foreach (var tech in request.EmergencyRequestTechnicians)
                        {
                            var flag = false;
                            if (tech.CallStatus == RequestState.Answered || tech.CallStatus == RequestState.Waiting)
                            {
                                return mapper.Map<RequestBreifDTO>(request);
                            }
                        }
                    }

                    if (request.TechReverseRequests != null)
                    {
                        foreach (var rev in request.TechReverseRequests)
                        {
                            if (rev.CallState == RequestState.Answered)
                            {
                                return mapper.Map<RequestBreifDTO>(request);
                            }
                        }
                    }
                }
            }
            throw new RequestNotFoundException();
        }

    }
}
