using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrerHubJobTheJobBoard.myexception
{
    class JobNotFoundException: Exception
    {
        public override string Message
        {
            get
            {
                return "Job not found with the entered Job id";
            }
        }
    }
}
