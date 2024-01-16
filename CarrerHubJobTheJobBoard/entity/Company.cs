using CarrerHubJobTheJobBoard.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrerHubJobTheJobBoard.entity
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        private List<JobListing> JobListings { get; set; } = new List<JobListing>();

        // Constructor
        public Company() { }
        public Company(int companyID, string companyName, string location)
        {
            CompanyID = companyID;
            CompanyName = companyName;
            Location = location;
        }

        /// <summary>
        /// Posts a new job listing on the job board with the provided information.
        /// </summary>
        /// <param name="cmpid">The ID of the company posting the job.</param>
        /// <param name="jobTitle">The title of the job listing.</param>
        /// <param name="jobDescription">The description of the job listing.</param>
        /// <param name="jobLocation">The location of the job listing.</param>
        /// <param name="salary">The salary offered for the job listing.</param>
        /// <param name="jobType">The type or category of the job listing.</param>

        public void PostJob(int cmpid, string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType)
        {
            JobListing jobListing = new JobListing
            {
                CompanyID = cmpid,
                JobTitle = jobTitle,
                JobDescription = jobDescription,
                JobLocation = jobLocation,
                Salary = salary,
                JobType = jobType,

            };

            // Insert the job listing into the database
            DatabaseManager.InsertJobListing(jobListing);

        }//done

        /// <summary>
        /// Retrieves a list of job listings posted by the company.
        /// </summary>
        /// <returns>A list of JobListing objects representing the posted jobs.</returns>
        // Method to retrieve a list of job listings posted by the company
        public List<JobListing> GetJobs()//done
        {
            return DatabaseManager.GetJobListings();
        }

    }
}
