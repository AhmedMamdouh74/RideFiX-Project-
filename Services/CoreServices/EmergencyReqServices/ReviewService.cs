using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Service.Exception_Implementation.ArgumantNullException;
using Service.Specification_Implementation;
using Service.Specification_Implementation.RequestSpecifications;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.ReviewsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices.EmergencyReqServices
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRequestServices requestServices;
        private readonly IMapper mapper;
        public ReviewService(IUnitOfWork _unitOfWork, 
            IMapper _mapper,
            IRequestServices requestServices
            )
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            this.requestServices = requestServices;
        }
        public async Task AddReviewAsync(AddReviewDTO addReview) 
        {
            if (addReview == null)
            {
                throw new ReviewNullException();
            }

            var spec = new EmergencyRequestTotalSpecification(addReview.RequestId);

            var emergencyRequest = await unitOfWork.GetRepository<EmergencyRequest, int>().GetByIdAsync(spec);
            var technician = await requestServices.EmergencyTechnicianID(emergencyRequest.Id);
            addReview.TechnicianId = technician.TechnicianID;

            var review = mapper.Map<Review>(addReview);

            await unitOfWork.GetRepository<Review, int>().AddAsync(review);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
