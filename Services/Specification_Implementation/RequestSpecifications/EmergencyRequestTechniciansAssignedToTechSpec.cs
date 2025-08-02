using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using SharedData.Enums;

namespace Service.Specification_Implementation.RequestSpecifications
{
    public class EmergencyRequestTechniciansAssignedToTechSpec : Specification<EmergencyRequestTechnicians, int>
    {
        public EmergencyRequestTechniciansAssignedToTechSpec(int technicianId, RequestState requestState) : base(ert =>
            ert.TechnicianId == technicianId &&
            ert.CallStatus == requestState)
        {
            AddInclude(ert => ert.EmergencyRequests);
            AddInclude(ert => ert.EmergencyRequests.category);
            AddInclude(ert => ert.EmergencyRequests.CarOwner);
            AddInclude(ert => ert.EmergencyRequests.CarOwner.ApplicationUser);
        }
    }
}
