using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrerHubJobTheJobBoard.myexception
{
    class ApplicationNotFoundException:Exception
    {
        public override string Message
        {
            get
            {
                return "Application not found with the entered Application id";
            }
        }
    }
}
