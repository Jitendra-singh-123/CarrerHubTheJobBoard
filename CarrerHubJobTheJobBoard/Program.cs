﻿using System;
using System.Collections.Generic;
using CarrerHubJobTheJobBoard.entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarrerHubJobTheJobBoard.DAO;

namespace CarrerHubJobTheJobBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice;
            try
            {
                do
                {
                    Console.WriteLine("\n-------------------------------------------Menu Driven program-----------------------------------------------------");
                    Console.WriteLine("\n1.Apply for the job");
                    Console.WriteLine("2. Create Profile");
                    Console.WriteLine("3. Post Job");
                    Console.WriteLine("4. Get Job");
                    Console.WriteLine("5. Insert Job Application ");
                    Console.WriteLine("6. Get Applicants ");
                    Console.WriteLine("7.  Get Companies");
                    Console.WriteLine("8.  Get Application for Specific job listing");
                    Console.WriteLine("9. Search Jobs within specified range of salary");
                    Console.WriteLine("10. calculate average salary");

                    Console.WriteLine("Enter your choice ");
                    Console.WriteLine();
                    choice = int.Parse(Console.ReadLine());

                    Company company = new Company();
                    Applicant applicant = new Applicant();
                    JobListing job = new JobListing();
                    switch (choice)
                    {

                        case 1:
                            Console.WriteLine("Enter Job Id");
                            try
                            {
                                int jobId = int.Parse(Console.ReadLine());
                                DatabaseManager.FindJobByID(jobId);

                                Console.WriteLine("Enter Cover Letter");
                                string coverLetter = Console.ReadLine();
                                applicant.ApplyForJob(jobId, coverLetter);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;

                        case 2:
                            try
                            {
                                Console.WriteLine("Enter email,first name,last name and phone number");
                                string email = Console.ReadLine();
                                string firstName = Console.ReadLine();
                                string lastName = Console.ReadLine();
                                string phoneNumber = Console.ReadLine();
                                applicant.CreateProfile(email, firstName, lastName, phoneNumber);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;

                        case 3:
                            Console.WriteLine("Enter companyId, title, descirption, job Location, salary and job type");
                            try
                            {
                                int compID = int.Parse(Console.ReadLine());
                                DatabaseManager.FindCompanyByID(compID);
                                string title = Console.ReadLine();
                                string desc = Console.ReadLine();
                                string loc = Console.ReadLine();
                                decimal salary = decimal.Parse(Console.ReadLine());
                                string type = Console.ReadLine();
                                company.PostJob(compID, title, desc, loc, salary, type);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;

                        case 4:
                            List<JobListing> jobListings = new List<JobListing>();
                            jobListings = company.GetJobs();
                            foreach (JobListing j in jobListings)
                            {
                                Console.WriteLine($"\nJobID : {j.JobID}\nCompanyID : {j.CompanyID}\nJob title : {j.JobTitle}\nDescription: {j.JobDescription}\nLocation: {j.JobLocation}\nSalary: {j.Salary}\nJob Type: {j.JobType}");
                            }
                            break;


                        case 5:
                            Console.WriteLine("Enter applicant id and cover letter");
                            try
                            {
                                int appId = int.Parse(Console.ReadLine());
                                DatabaseManager.FindApplicantByID(appId);
                                string coverLetter = Console.ReadLine();

                                job.Apply(appId, coverLetter);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case 6:
                            List<Applicant> applicantsList = new List<Applicant>();
                            applicantsList = job.GetApplicants();
                            foreach (Applicant ap in applicantsList)
                            {
                                Console.WriteLine($"\nApplicantId : {ap.ApplicantID}\nFirst Name: {ap.FirstName}\nLast Name: {ap.LastName}\nEmail: {ap.Email}\nResume: {ap.Resume}\nPhone Number: {ap.Phone}");
                            }
                            break;
                        case 7:
                            List<Company> companyList = new List<Company>();
                            companyList = DatabaseManager.GetCompanies();
                            foreach (Company cp in companyList)
                            {
                                Console.WriteLine($"\nCompanyID : {cp.CompanyID}\nName: {cp.CompanyName}\nLocation: {cp.Location}");
                            }
                            break;
                        case 8:
                            try
                            {
                                List<JobApplication> jobApplicationList = new List<JobApplication>();
                                Console.WriteLine("Enter jobid for which you want to retrieve job application");
                                int jobID = int.Parse(Console.ReadLine());
                                DatabaseManager.FindJobByID(jobID);
                                jobApplicationList = DatabaseManager.GetApplicationsForJob(jobID);
                                foreach (JobApplication j in jobApplicationList)
                                {
                                    Console.WriteLine($"\nApplicationID : {j.ApplicationID}\nJobID: {j.JobID}\nApplicantID: {j.ApplicantID}\nApplication Date: {j.ApplicationDate}\nCover Letter: {j.CoverLetter}");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case 9:
                            Console.WriteLine("Enter min and max range of salary:");
                            try
                            {
                                int min = int.Parse(Console.ReadLine());
                                int max = int.Parse(Console.ReadLine());
                                DatabaseManager.JobsWithinSpecifiedRangeOfSalary(min, max);
                                List<JobListing> jobListing = new List<JobListing>();
                                jobListing = DatabaseManager.JobsWithinSpecifiedRangeOfSalary(min, max);
                                foreach (JobListing j in jobListing)
                                {
                                    Console.WriteLine($"\nJobID : {j.JobID}\nCompanyID : {j.CompanyID}\nJob title : {j.JobTitle}\nDescription: {j.JobDescription}\nLocation: {j.JobLocation}\nSalary: {j.Salary}\nJob Type: {j.JobType}");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;

                        case 10:
                            Console.WriteLine("Average salary: " + DatabaseManager.CalculateAverageSalary());
                            break;
                        default:
                            Console.WriteLine("Invalid Option");
                            break;

                    }
                } while (choice != 0);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            Console.ReadLine();


        }
    }
}
