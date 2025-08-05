using SharedData.DTOs.RequestsDTOs;
using SharedData.DTOs.TechnicianDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.CoreServicesAbstractions
{
    public interface IRequestServices
    {
        public Task<PreRequestDTO> CreateRequestAsync(CreatePreRequestDTO request);
        public Task CancelAll(int id);
        public Task CreateRealRequest(RealRequestDTO request);
        public Task<List<RequestBreifDTO>> RequestBreifDTOs(int carOwnerID);
        public Task<RequestDetailsDTO> RequestDetailsDTOs(int requestId);
        public Task CompleteRequest (int requestId);

        public Task<bool> IsPresent(RealRequestDTO request);
        public Task<EmergencyTechnicianID> EmergencyTechnicianID(int requestId);



    }
}
