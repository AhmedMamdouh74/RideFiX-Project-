using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.Enums
{
    public enum RequestState
    {
        Answered = 1,
        Rejected = 2,
        Waiting = 3,
        Cancelled = 4,
        Completed = 5,
        // for request in home page , once the tech apply the state for request will be applied 
        // that state will help in front to change the state based on
        Applied = 6
    }
}
