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

        // Method to retrieve a list of job listings posted by the company
        public List<JobListing> GetJobs()//done
        {
            return DatabaseManager.GetJobListings();
        }

    }
}
