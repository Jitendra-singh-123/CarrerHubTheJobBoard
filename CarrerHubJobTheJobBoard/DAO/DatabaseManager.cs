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
        static SqlConnection conn;

        /// <summary>
        /// Calculates the average salary from the 'Jobs' table and handles exceptions related to negative salaries.
        /// </summary>
        /// <returns>The calculated average salary.</returns>
        public static decimal CalculateAverageSalary()
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();

                    // Check if there is any negative salary in the table
                    bool hasNegativeSalary = HasNegativeSalary(conn);

                    if (hasNegativeSalary)
                    {
                        throw new SalaryCalculationHandling("Some salary is negative, cannot calculate the average salary");
                    }

                    string query = "SELECT AVG(Salary) AS AverageSalary FROM Jobs";

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
            catch (SalaryCalculationHandling ex)
            {
                // Handle your custom exception (e.g., display an error message)
                Console.WriteLine($"Error: {ex.Message}");
                return -1; // Or any other suitable value indicating an error
            }
            catch (Exception ex)
            {
                // Handle other exceptions (e.g., display a generic error message)
                Console.WriteLine($"Error: {ex.Message}");
                return -1; // Or any other suitable value indicating an error
            }
        }

        private static bool HasNegativeSalary(SqlConnection conn)
        {
            string checkNegativeSalaryQuery = "SELECT COUNT(*) FROM Jobs WHERE Salary < 0";

            using (SqlCommand cmd = new SqlCommand(checkNegativeSalaryQuery, conn))
            {
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }


        /// <summary>
        /// Finds a job by its unique identifier (JobID) in the 'Jobs' table.
        /// </summary>
        /// <param name="id">The JobID to search for.</param>
        /// <returns>True if the job is found; otherwise, throws a JobNotFoundException.</returns>
        public static bool FindJobByID(int id)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
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
            }
            catch (Exception e)
            {
                throw;

            }
        }



        /// <summary>
        /// Finds an applicant by their unique identifier (ApplicantID) in the 'Applicants' table.
        /// </summary>
        /// <param name="id">The ApplicantID to search for.</param>
        /// <returns>True if the applicant is found; otherwise, throws an ApplicantNotFoundException.</returns>
        public static bool FindApplicantByID(int id)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
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
            }

            catch (Exception ex)
            {
                // Console.WriteLine($"Exception: {ex.Message}");
                throw;

            }
        }

        /// <summary>
        /// Finds a company by its unique identifier (CompanyID) in the 'Companies' table.
        /// </summary>
        /// <param name="id">The CompanyID to search for.</param>
        /// <returns>True if the company is found; otherwise, throws a CompanyNotFoundException.</returns>
        public static bool FindCompanyByID(int id)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
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
            }

            catch (Exception ex)
            {
                // Console.WriteLine($"Exception: {ex.Message}");
                throw;

            }
        }

        /// <summary>
        /// Finds an application by its unique identifier (ApplicationID) in the 'Applications' table.
        /// </summary>
        /// <param name="id">The ApplicationID to search for.</param>
        /// <returns>True if the application is found; otherwise, throws an ApplicationNotFoundException.</returns>
        public static bool FindApplicationByID(int id)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
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
            }

            catch (Exception ex)
            {
                // Console.WriteLine($"Exception: {ex.Message}");
                throw;

            }
        }

        /// <summary>
        /// Inserts a job listing into the 'Jobs' table.
        /// </summary>
        /// <param name="job">The job listing to be inserted.</param>
        public static void InsertJobListing(JobListing job)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
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
            }
            catch (Exception e)
            {
                throw;
            }
        }


        /// <summary>
        /// Retrieves a list of all applicants from the 'Applicants' table.
        /// </summary>
        /// <returns>A list of Applicant objects representing the applicants in the database.</returns>
        public static List<Applicant> GetApplicants()
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string getApplicantsQuery = "SELECT * FROM Applicants";
                    List<Applicant> applicantsList = new List<Applicant>();
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

                        applicantsList.Add(applicant);
                    }
                    return applicantsList;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Applies for a job by creating an entry in the 'Applications' table.
        /// Checks if the application date is greater than the job posted date before proceeding.
        /// </summary>
        /// <param name="applicantID">The ID of the applicant applying for the job.</param>
        /// <param name="jobID">The ID of the job being applied for.</param>
        /// <param name="coverLetter">The cover letter submitted with the application.</param>
        public static void ApplyForJob(int applicantID, int jobID, string coverLetter)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Inserts a new applicant into the 'Applicants' table.
        /// </summary>
        /// <param name="applicant">The Applicant object containing details of the applicant.</param>
        public static void InsertApplicant(Applicant applicant)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
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
            }
            catch (Exception e)
            {
                Console.WriteLine("Sorry!" + e.Message);
            }
        }

        /// <summary>
        /// Retrieves a list of companies from the 'Companies' table.
        /// </summary>
        /// <returns>A List of Company objects representing companies in the database.</returns>
        public static List<Company> GetCompanies()
        {

            try
            {
                using (conn = UtilClass.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Companies";
                    List<Company> companyList = new List<Company>();
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

                        companyList.Add(comp);
                    }
                    return companyList;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }


        /// <summary>
        /// Retrieves a list of job applications for a specific job from the 'Applications' table.
        /// </summary>
        /// <param name="jobid">The ID of the job for which applications are to be retrieved.</param>
        /// <returns>A List of JobApplication objects representing applications for the specified job.</returns>
        public static List<JobApplication> GetApplicationsForJob(int jobid)
        {
            List<JobApplication> ja = new List<JobApplication>();
            try
            {
                using (conn = UtilClass.GetConnection())
                {
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
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a list of job listings with salaries within the specified range from the 'Jobs' table.
        /// </summary>
        /// <param name="min">The minimum salary value for the range.</param>
        /// <param name="max">The maximum salary value for the range.</param>
        /// <returns>A List of JobListing objects representing job listings within the specified salary range.</returns>
        public static List<JobListing> JobsWithinSpecifiedRangeOfSalary(int min, int max)
        {
            try
            {
                using (conn = UtilClass.GetConnection())
                {
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
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a list of all job listings from the 'Jobs' table.
        /// </summary>
        /// <returns>A List of JobListing objects representing all job listings in the database.</returns>
        public static List<JobListing> GetJobListings()
        {
            List<JobListing> jobListings = new List<JobListing>();
            try
            {
                using (conn = UtilClass.GetConnection())
                {
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
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
