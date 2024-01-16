using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrerHubJobTheJobBoard.myexception
{
    class ApplicationDeadline: Exception
    {
        public override string Message
        {
            get
            {
                return "sorry!, you are applying for the job after the deadline";
            }
        }
    }
}
