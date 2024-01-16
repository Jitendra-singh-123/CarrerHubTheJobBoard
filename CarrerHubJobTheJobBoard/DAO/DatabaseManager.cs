using CarrerHubJobTheJobBoard.entity;
using CarrerHubJobTheJobBoard.myexception;
using CarrerHubJobTheJobBoard.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrerHubJobTheJobBoard.DAO
{
    public class DatabaseManager
    {

        public static  decimal CalculateAverageSalary()
        {
            try
            {
                using (SqlConnection conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT AVG(CASE WHEN Salary >= 0 THEN Salary END) AS AverageSalary FROM Jobs";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Execute the query and retrieve the average salary
                        object result = cmd.ExecuteScalar();

                        // Check if the result is not DBNull and not null
                        if (result != DBNull.Value && result != null)
                        {
                            decimal averageSalary = Convert.ToDecimal(result);
                            return averageSalary;
                        }
                        else
                        {
                            // Handle the case when there are no non-negative salaries
                            throw new SalaryCalculationHandling("No non-negative salaries found to calculate the average salary");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., display an error message)
                Console.WriteLine($"Error: {ex.Message}");
                return -1; // Or any other suitable value indicating an error
            }
        }

        public static bool FindJobByID(int id)
        {
            try
            {
                SqlConnection conn = UtilClass.GetConnection();
                conn = UtilClass.GetConnection();
                conn.Open();
                string query = $@"SELECT * FROM Jobs WHERE JobID = {id};";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Console.WriteLine("Job Found");
                    return true;
                }
                else
                {
                    throw new JobNotFoundException();
                }
            }
            catch(Exception e)
            {
                throw;
                
            }
      }
           
        


            public static bool FindApplicantByID(int id)
            {
                try
                {
                    SqlConnection conn = UtilClass.GetConnection();
                    conn = UtilClass.GetConnection();
                    conn.Open();
                    string query = $"SELECT * FROM Applicants WHERE ApplicantID = {id};";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        return true;
                    }
                    else
                    {
                        throw new ApplicantNotFoundException();
                    }
                }
          
                catch (Exception ex)
                {
                    // Console.WriteLine($"Exception: {ex.Message}");
                    throw;

                }
            }
        public static bool FindCompanyByID(int id)
        {
            try
            {
                SqlConnection conn = UtilClass.GetConnection();
                conn = UtilClass.GetConnection();
                conn.Open();
                string query = $@"SELECT * FROM Companies WHERE CompanyID = {id};";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }
                else
                {
                    throw new CompanyNotFoundException();
                }
            }

            catch (Exception ex)
            {
                // Console.WriteLine($"Exception: {ex.Message}");
                throw;

            }
        }


        public static bool FindApplicationByID(int id)
        {
            try
            {
                SqlConnection conn = UtilClass.GetConnection();
                conn = UtilClass.GetConnection();
                conn.Open();
                string query = $@"SELECT * FROM Applications WHERE ApplicationID = {id};";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }
                else
                {
                    throw new ApplicationNotFoundException();
                }
            }

            catch (Exception ex)
            {
                // Console.WriteLine($"Exception: {ex.Message}");
                throw;

            }
        }


        public static void InsertJobListing(JobListing job)
        {
            try
            {
                SqlConnection conn = UtilClass.GetConnection();
                conn.Open();
                string insertJobQuery = @"
                INSERT INTO Jobs (CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate)
                VALUES (@CompanyID, @JobTitle, @JobDescription, @JobLocation, @Salary, @JobType, GETDATE());  SELECT CAST(SCOPE_IDENTITY() AS INT);";
                SqlCommand cmd = new SqlCommand(insertJobQuery, conn);
                cmd.Parameters.AddWithValue("@CompanyID", job.CompanyID);
                cmd.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                cmd.Parameters.AddWithValue("@JobDescription", job.JobDescription);
                cmd.Parameters.AddWithValue("@JobLocation", job.JobLocation);
                cmd.Parameters.AddWithValue("@Salary", job.Salary);
                cmd.Parameters.AddWithValue("@JobType", job.JobType);

                job.JobID = Convert.ToInt32(cmd.ExecuteScalar());
                if (job.JobID > 0)
                {
                    Console.WriteLine("Job is posted Successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to Post Job in Job Table");
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static List<Applicant> GetApplicants()
        {
            try
            {
                SqlConnection conn = UtilClass.GetConnection();
                conn.Open();
                string getApplicantsQuery = "SELECT * FROM Applicants";
                List<Applicant> applicants = new List<Applicant>();
                SqlCommand cmd = new SqlCommand(getApplicantsQuery, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Applicant applicant = new Applicant
                    {
                        ApplicantID = (int)reader["ApplicantID"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Resume = reader["Resume"].ToString()
                    };

                    applicants.Add(applicant);
                }
                return applicants;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void ApplyForJob(int applicantID, int jobID, string coverLetter)
        {
            try
            {
                SqlConnection conn = UtilClass.GetConnection();
                conn.Open();

                // Check if the application date is greater than the job posted date
                string checkDateQuery = "SELECT 1 FROM Jobs WHERE JobID = @JobID AND GETDATE() < PostedDate";
                SqlCommand checkDateCmd = new SqlCommand(checkDateQuery, conn);
                checkDateCmd.Parameters.AddWithValue("@JobID", jobID);

                if (checkDateCmd.ExecuteScalar() != null)
                {
                    // Proceed with the application
                    string applyForJobQuery = @"
                INSERT INTO Applications (JobID, ApplicantID, ApplicationDate, CoverLetter)
                VALUES (@JobID, @ApplicantID, GETDATE(), @CoverLetter)";

                    SqlCommand cmd = new SqlCommand(applyForJobQuery, conn);
                    cmd.Parameters.AddWithValue("@JobID", jobID);
                    cmd.Parameters.AddWithValue("@ApplicantID", applicantID);
                    cmd.Parameters.AddWithValue("@CoverLetter", coverLetter);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Applied for job");
                    }
                    else
                    {
                        Console.WriteLine("Not able to apply");
                    }
                }
                else
                {
                    throw new ApplicationDeadline();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void InsertApplicant(Applicant applicant)
        {
            try
            {
                SqlConnection conn = UtilClass.GetConnection();
                conn.Open();
                string insertApplicantQuery = $@"
                INSERT INTO Applicants (FirstName, LastName, Email, Phone)
                VALUES ('{applicant.FirstName}', '{applicant.LastName}', '{applicant.Email}', '{applicant.Phone}')";
                SqlCommand cmd = new SqlCommand(insertApplicantQuery, conn);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("Data is inserted");
                else
                    Console.WriteLine("Failed to add");
            }
            catch (Exception e)
            {
                Console.WriteLine("Sorry!" + e.Message);
            }
        }

        public static List<Company> GetCompanies()
        {

            try
            {
                SqlConnection conn = UtilClass.GetConnection();
                conn.Open();
                string query = "SELECT * FROM Companies";
                List<Company> cmp = new List<Company>();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Company comp = new Company
                    {
                        CompanyID = reader.GetInt32(0),
                        CompanyName = reader.GetString(1),
                        Location = reader.GetString(2),
                    };

                    cmp.Add(comp);
                }
                return cmp;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static List<JobApplication> GetApplicationsForJob(int jobid)
        {
            List<JobApplication> ja = new List<JobApplication>();
            try
            {
                SqlConnection conn = UtilClass.GetConnection();
                conn.Open();
                string query = $@"SELECT * FROM Applications WHERE JobID = {jobid};";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    JobApplication jobApplication = new JobApplication
                    {
                        ApplicationID = reader.GetInt32(0),
                        JobID = reader.GetInt32(1),
                        ApplicantID = reader.GetInt32(2),
                        ApplicationDate = reader.GetDateTime(3),
                        CoverLetter = reader.GetString(4)
                    };
                    ja.Add(jobApplication);

                }
                return ja;

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static List<JobListing> JobsWithinSpecifiedRangeOfSalary(int min, int max)
        {
            try
            {
                SqlConnection conn = UtilClass.GetConnection();
                conn.Open();
                string query = $@"SELECT * FROM Jobs WHERE Salary BETWEEN {min} AND {max};";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                List<JobListing> jobListings = new List<JobListing>();
                while (reader.Read())
                {
                    JobListing jobListing = new JobListing
                    {
                        JobID = reader.GetInt32(0),
                        CompanyID = reader.GetInt32(1),
                        JobTitle = reader.GetString(2),
                        JobDescription = reader.GetString(3),
                        JobLocation = reader.GetString(4),
                        Salary = reader.GetDecimal(5),
                        JobType = reader.GetString(6),
                        PostedDate = reader.GetDateTime(7)
                    };
                    jobListings.Add(jobListing);
                }
                return jobListings;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static List<JobListing> GetJobListings()
        {
            List<JobListing> jobListings = new List<JobListing>();
            try
            {
                SqlConnection conn = UtilClass.GetConnection();
                conn.Open();
                string query = "SELECT * FROM Jobs";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    JobListing jobListing = new JobListing
                    {
                        JobID = reader.GetInt32(0),
                        CompanyID = reader.GetInt32(1),
                        JobTitle = reader.GetString(2),
                        JobDescription = reader.GetString(3),
                        JobLocation = reader.GetString(4),
                        Salary = reader.GetDecimal(5),
                        JobType = reader.GetString(6),
                        PostedDate = reader.GetDateTime(7)
                    };

                    jobListings.Add(jobListing);
                }
                return jobListings;

            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
