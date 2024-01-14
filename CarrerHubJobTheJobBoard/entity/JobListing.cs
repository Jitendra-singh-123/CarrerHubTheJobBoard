﻿
using CarrerHubJobTheJobBoard.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrerHubJobTheJobBoard.entity
{
    public class JobListing
    {
        public int JobID { get; set; }
        public int CompanyID { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobLocation { get; set; }
        public decimal Salary { get; set; }
        public string JobType { get; set; }
        public DateTime PostedDate { get; set; }
        private List<Applicant> Applicants { get; set; } = new List<Applicant>();


        public JobListing() { }
        // Constructor
        public JobListing(int jobID, int companyID, string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType, DateTime postedDate)
        {
            JobID = jobID;
            CompanyID = companyID;
            JobTitle = jobTitle;
            JobDescription = jobDescription;
            JobLocation = jobLocation;
            Salary = salary;
            JobType = jobType;
            PostedDate = postedDate;
        }

        public void Apply(int applicantID, string coverLetter)
        {
            Console.WriteLine("Enter Job id:");
            int jobid = int.Parse(Console.ReadLine());
            DatabaseManager.ApplyForJob(jobid, applicantID,coverLetter);

        }

        public List<Applicant> GetApplicants()
        {
            return DatabaseManager.GetApplicants();
        }

    }


    // Method to retrieve a list of applicants who have applied for the job

}
