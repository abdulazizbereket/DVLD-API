using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DVLD_API.DTOs;

namespace DVDLDataAccessLayer
{
    public class clsUserDataLayer
    {
        // جلب مستخدم بالمعرف
        public static bool GetUserByID(int UserID, ref int PersonID, ref string UserName, 
                                       ref string Password, ref bool IsActive)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            bool IsFound = false;
            string Query = "SELECT * FROM Users WHERE UserID = @userID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@userID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في GetUserByID: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        // جلب مستخدم باسم المستخدم
        public static bool GetUserByUserName(string UserName, ref int UserID, ref int PersonID, 
                                            ref string Password, ref bool IsActive)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            bool IsFound = false;
            string Query = "SELECT * FROM Users WHERE UserName = @userName";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@userName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في GetUserByUserName: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        // إضافة مستخدم جديد
        public static int AddNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            int UserID = -1;
            
            string Query = @"INSERT INTO Users (PersonID, UserName, Password, IsActive)
                           VALUES (@personID, @userName, @password, @isActive);
                           SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@personID", PersonID);
            command.Parameters.AddWithValue("@userName", UserName);
            command.Parameters.AddWithValue("@password", Password);
            command.Parameters.AddWithValue("@isActive", IsActive);

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int id))
                {
                    UserID = id;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في AddNewUser: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return UserID;
        }

        // تحديث بيانات المستخدم
        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            
            string Query = @"UPDATE Users SET
                            PersonID = @PersonID,
                            UserName = @UserName,
                            Password = @Password,
                            IsActive = @IsActive
                        WHERE UserID = @UserID;";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@userID", UserID);
            command.Parameters.AddWithValue("@personID", PersonID);
            command.Parameters.AddWithValue("@userName", UserName);
            command.Parameters.AddWithValue("@password", Password);
            command.Parameters.AddWithValue("@isActive", IsActive);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في UpdateUser: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return (RowsAffected > 0);
        }

        // حذف مستخدم
        public static bool DeleteUser(int UserID)
        {
            int RowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            string Query = "DELETE FROM Users WHERE UserID = @userID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@userID", UserID);

            try
            {
                connection.Open();
                RowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في DeleteUser: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return (RowAffected > 0);
        }
    }
}