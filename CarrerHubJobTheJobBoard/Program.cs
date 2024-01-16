using System;
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
                    Console.WriteLine("\n-----------------------------------Menu Drive program-----------------------------------------------------");
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

                    Company c = new Company();
                    Applicant applicant = new Applicant();
                    JobListing job = new JobListing();
                    switch (choice)
                    {

                        case 1:
                            Console.WriteLine("Enter Job Id");
                            try
                            {
                                int jobid = int.Parse(Console.ReadLine());
                                DatabaseManager.FindJobByID(jobid);

                                Console.WriteLine("Enter Cover Letter");
                                string s = Console.ReadLine();
                                applicant.ApplyForJob(jobid, s);
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
                                string em = Console.ReadLine();
                                string fn = Console.ReadLine();
                                string ln = Console.ReadLine();
                                string pn = Console.ReadLine();
                                applicant.CreateProfile(em, fn, ln, pn);
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
                                c.PostJob(compID, title, desc, loc, salary, type);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;

                        case 4:
                            List<JobListing> jobListings = new List<JobListing>();
                            jobListings = c.GetJobs();
                            foreach (JobListing j in jobListings)
                            {
                                Console.WriteLine($"\nJobID : {j.JobID}\nCompanyID : {j.CompanyID}\nJob title : {j.JobTitle}\nDescription: {j.JobDescription}\nLocation: {j.JobLocation}\nSalary: {j.Salary}\nJob Type: {j.JobType}");
                            }
                            break;


                        case 5:
                            Console.WriteLine("Enter applicant id and cover letter");
                            try
                            {
                                int appid = int.Parse(Console.ReadLine());
                                DatabaseManager.FindApplicantByID(appid);
                                string coverletter = Console.ReadLine();

                                job.Apply(appid, coverletter);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case 6:
                            List<Applicant> applicants = new List<Applicant>();
                            applicants = job.GetApplicants();
                            foreach (Applicant ap in applicants)
                            {
                                Console.WriteLine($"\nApplicantId : {ap.ApplicantID}\nFirst Name: {ap.FirstName}\nLast Name: {ap.LastName}\nEmail: {ap.Email}\nResume: {ap.Resume}\nPhone Number: {ap.Phone}");
                            }
                            break;
                        case 7:
                            List<Company> cmp = new List<Company>();
                            cmp = DatabaseManager.GetCompanies();
                            foreach (Company cp in cmp)
                            {
                                Console.WriteLine($"\nCompanyID : {cp.CompanyID}\nName: {cp.CompanyName}\nLocation: {cp.Location}");
                            }
                            break;
                        case 8:
                            try
                            {
                                List<JobApplication> ja = new List<JobApplication>();
                                Console.WriteLine("Enter jobid for which you want to retrieve job application");
                                int jobID = int.Parse(Console.ReadLine());
                                DatabaseManager.FindJobByID(jobID);
                                ja = DatabaseManager.GetApplicationsForJob(jobID);
                                foreach (JobApplication j in ja)
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
