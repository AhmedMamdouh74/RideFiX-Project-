using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;

namespace Service.Specification_Implementation.CarOwnerSpecifications
{
    public class CarOwnerWithUserAndReviewsSpec : Specification<CarOwner, int>
    {
        public CarOwnerWithUserAndReviewsSpec():base()
        {
            AddInclude(t => t.Reviews);
            AddInclude(t => t.ApplicationUser);
        }
        public CarOwnerWithUserAndReviewsSpec(int userId):base(c=>c.Id==userId)
        {
           
            AddInclude(t => t.ApplicationUser);
        }

    }
}
