using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DVLD_API.DTOs;

namespace DVDLDataAccessLayer
{
    public class clsApplicationData
    {
        // إضافة تطبيق جديد
        public static int AddNewApplication(int PersonID, DateTime ApplicationDate, int ApplicationTypeID, 
                                           int ApplicationStatus, decimal PaidFees, int UserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            int ApplicationID = -1;
            
            string Query = @"INSERT INTO Applications 
                           (PersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, PaidFees, UserID, LastStatusDate)
                           VALUES
                           (@personID, @applicationDate, @applicationTypeID, @applicationStatus, @paidFees, @userID, @lastStatusDate);
                           SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@personID", PersonID);
            command.Parameters.AddWithValue("@applicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@applicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@applicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@paidFees", PaidFees);
            command.Parameters.AddWithValue("@userID", UserID);
            command.Parameters.AddWithValue("@lastStatusDate", DateTime.Now);

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int id))
                {
                    ApplicationID = id;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في AddNewApplication: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return ApplicationID;
        }

        // تحديث التطبيق
        public static bool UpdateApplication(int ApplicationID, int PersonID, DateTime ApplicationDate, 
                                            int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate, 
                                            decimal PaidFees, int UserID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            
            string Query = @"UPDATE Applications SET
                            PersonID = @PersonID,
                            ApplicationDate = @ApplicationDate,
                            ApplicationTypeID = @ApplicationTypeID,
                            ApplicationStatus = @ApplicationStatus,
                            LastStatusDate = @LastStatusDate,
                            PaidFees = @PaidFees,
                            UserID = @UserID
                        WHERE ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@applicationID", ApplicationID);
            command.Parameters.AddWithValue("@personID", PersonID);
            command.Parameters.AddWithValue("@applicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@applicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@applicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@lastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@paidFees", PaidFees);
            command.Parameters.AddWithValue("@userID", UserID);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في UpdateApplication: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return (RowsAffected > 0);
        }

        // جلب تطبيق بالمعرف
        public static bool GetApplicationByID(int ApplicationID, ref int PersonID, ref DateTime ApplicationDate, 
                                             ref int ApplicationTypeID, ref int ApplicationStatus, 
                                             ref DateTime LastStatusDate, ref decimal PaidFees, ref int UserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            bool IsFound = false;
            string Query = "SELECT * FROM Applications WHERE ApplicationID = @applicationID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@applicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    PersonID = (int)reader["PersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (int)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    UserID = (int)reader["UserID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في GetApplicationByID: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        // تحديث حالة التطبيق
        public static bool UpdateApplicationStatus(int ApplicationID, int ApplicationStatus, DateTime LastStatusDate)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            
            string Query = @"UPDATE Applications SET
                            ApplicationStatus = @ApplicationStatus,
                            LastStatusDate = @LastStatusDate
                        WHERE ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@applicationID", ApplicationID);
            command.Parameters.AddWithValue("@applicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@lastStatusDate", LastStatusDate);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في UpdateApplicationStatus: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return (RowsAffected > 0);
        }

        // حذف التطبيق
        public static bool DeleteApplication(int ApplicationID)
        {
            int RowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            string Query = "DELETE FROM Applications WHERE ApplicationID = @applicationID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@applicationID", ApplicationID);

            try
            {
                connection.Open();
                RowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في DeleteApplication: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return (RowAffected > 0);
        }
    }
}