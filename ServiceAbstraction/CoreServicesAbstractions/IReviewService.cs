using SharedData.DTOs.ReviewsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.CoreServicesAbstractions
{
    public interface IReviewService
    {
        public Task AddReviewAsync(AddReviewDTO addReview);
    }
}
