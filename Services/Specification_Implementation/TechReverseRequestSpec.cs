using Domain.Contracts.SpecificationContracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using System.Linq.Expressions;

namespace Service.Specification_Implementation
{
	public class TechReverseRequestSpec : Specification<TechReverseRequest, int>
	{

		public TechReverseRequestSpec(int techId) : base(r => r.TechnicianId == techId)
		{
			AddInclude(r => r.EmergencyRequest);
			AddInclude(r => r.Technician);
		}
		public TechReverseRequestSpec(int requestId, int techId) : base(r => (r.TechnicianId == techId) && (r.EmergencyRequestId == requestId))
		{
			AddInclude(r => r.EmergencyRequest);
			AddInclude(r => r.Technician);

		}
	}
}