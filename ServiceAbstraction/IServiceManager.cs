using ServiceAbstraction.CoreServicesAbstractions;
using ServiceAbstraction.CoreServicesAbstractions.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IServiceManager
    {

        ITechnicianService technicianService { get; }
        IRequestServices requestServices { get; }
        ITechnicianRequestEmergency technicianRequestEmergency { get; }
        ICategoryService categoryService { get; }
        IReviewService reviewService { get; }

        ICarOwnerService carOwnerService { get; }
        IUserProfileService userProfileService { get; }


    }
}
