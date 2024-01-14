﻿using CarrerHubJobTheJobBoard.DAO;
using CarrerHubJobTheJobBoard.myexception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public void CreateProfile(string email, string firstName, string lastName, string phone)
        {
            Applicant a = new Applicant
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Phone = phone,
            };

            DatabaseManager.InsertApplicant(a);
        }//done


        // Method to apply for a job
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