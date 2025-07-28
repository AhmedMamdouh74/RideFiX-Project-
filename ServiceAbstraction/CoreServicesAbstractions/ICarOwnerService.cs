using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedData.DTOs;
using SharedData.DTOs.RequestsDTOs;

namespace ServiceAbstraction.CoreServicesAbstractions
{
    public interface ICarOwnerService
    {
        public Task<RequestBreifDTO> IsRequested(int Id);

    }
}
