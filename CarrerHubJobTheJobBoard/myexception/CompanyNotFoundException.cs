using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrerHubJobTheJobBoard.myexception
{
    class CompanyNotFoundException: Exception
    {
        public override string Message
        {
            get
            {
                return "Company not found with the entered Company id";
            }
        }
    }
}
