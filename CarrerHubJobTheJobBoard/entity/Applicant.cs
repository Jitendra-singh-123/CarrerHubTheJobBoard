using CarrerHubJobTheJobBoard.DAO;
using CarrerHubJobTheJobBoard.myexception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarrerHubJobTheJobBoard.entity
{
    public class Applicant
    {
        public int ApplicantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Resume { get; set; }

        // Constructor
        public Applicant() { }
        public Applicant(int applicantID, string firstName, string lastName, string email, string phone, string resume = null)
        {
            ApplicantID = applicantID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Resume = resume;
        }

        /// <summary>
        /// Creates a new applicant profile with the provided information after validating the email format.
        /// </summary>
        /// <param name="email">The email address of the applicant.</param>
        /// <param name="firstName">The first name of the applicant.</param>
        /// <param name="lastName">The last name of the applicant.</param>
        /// <param name="phone">The phone number of the applicant.</param>
        public void CreateProfile(string email, string firstName, string lastName, string phone)
        {
            try
            {
                ValidateEmailFormat(email); // Validate email format before creating the profile

                Applicant a = new Applicant
                {
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Phone = phone,
                };

                DatabaseManager.InsertApplicant(a);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }//done

        /// <summary>
        /// Validates the email format using a regular expression.
        /// </summary>
        /// <param name="email">The email address to be validated.</param>
        /// <exception cref="InvalidEmailFormatException">Thrown when the email format is invalid.</exception>
        private void ValidateEmailFormat(string email)
        {
            // Simple email format validation using regular expression
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

            if (!Regex.IsMatch(email, emailPattern))
            {
                throw new InvalidEmailFormatException("Invalid email format. Please enter a valid email address.");
            }
        }
        // Method to apply for a job
        /// <summary>
        /// Allows an applicant to apply for a job by providing the job ID and a cover letter.
        /// </summary>
        /// <param name="jobID">The ID of the job the applicant is applying for.</param>
        /// <param name="coverLetter">The cover letter submitted with the job application.</param>
        public void ApplyForJob(int jobID, string coverLetter)
        {
            try
            {
                Console.WriteLine("Enter Applicant ID");
                int applicantid = int.Parse(Console.ReadLine());
                DatabaseManager.FindApplicantByID(applicantid);
                DatabaseManager.ApplyForJob(applicantid, jobID, coverLetter);
            }
            catch(Exception e)
            {
                // Console.WriteLine(e.Message);
                throw;
            }
           
        }//done
    }
}
