using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrerHubJobTheJobBoard.myexception
{
    class SalaryCalculationHandling : Exception
    {
        public SalaryCalculationHandling(string message) : base(message)
        {
        }
    }
}
