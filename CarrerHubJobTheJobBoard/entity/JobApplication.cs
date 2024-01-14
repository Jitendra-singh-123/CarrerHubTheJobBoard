using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrerHubJobTheJobBoard.entity
{
   public class JobApplication
    {
        public int ApplicationID { get; set; }
        public int JobID { get; set; }
        public int ApplicantID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string CoverLetter { get; set; }

        // Constructor
        public JobApplication() { }
        public JobApplication(int applicationID, int jobID, int applicantID, DateTime applicationDate, string coverLetter)
        {
            ApplicationID = applicationID;
            JobID = jobID;
            ApplicantID = applicantID;
            ApplicationDate = applicationDate;
            CoverLetter = coverLetter;
        }
    }
}
