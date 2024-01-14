using System;
using System.Runtime.Serialization;

namespace CarrerHubJobTheJobBoard.myexception
{
    [Serializable]
    public class ApplicantNotFoundException : Exception
    {
        public override string Message
        {
            get
            {
                return "Applicant not found with the entered Applicant id";
            }
        }
    }
}