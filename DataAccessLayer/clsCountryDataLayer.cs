using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DVLD_API.DTOs;

namespace DVDLDataAccessLayer
{
    public class clsCountryDataLayer
    {
        // جلب جميع الدول
        public static List<CountryDTO> GetAllCountries()
        {
            List<CountryDTO> countriesList = new List<CountryDTO>();
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);

            string Query = "SELECT CountryID, CountryName FROM Countries ORDER BY CountryName";
            SqlCommand command = new SqlCommand(Query, connection);
            
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CountryDTO country = new CountryDTO
                    {
                        CountryID = (int)reader["CountryID"],
                        CountryName = (string)reader["CountryName"]
                    };
                    countriesList.Add(country);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في GetAllCountries: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return countriesList;
        }

        // جلب دولة بالمعرف
        public static bool GetCountryByID(int CountryID, ref string CountryName)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            bool IsFound = false;
            string Query = "SELECT * FROM Countries WHERE CountryID = @countryID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@countryID", CountryID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    CountryName = (string)reader["CountryName"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في GetCountryByID: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        // جلب دولة بالاسم
        public static bool GetCountryByName(string CountryName, ref int CountryID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            bool IsFound = false;
            string Query = "SELECT * FROM Countries WHERE CountryName = @countryName";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@countryName", CountryName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    CountryID = (int)reader["CountryID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في GetCountryByName: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }
    }
}