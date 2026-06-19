using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using DVLD_API.DTOs;

namespace DVDLDataAccessLayer
{
    public class clsPersonDataLayer
    {
        // جلب جميع الأشخاص
        public static List<PersonDTO> GetAllPersons()
        {
            List<PersonDTO> personsList = new List<PersonDTO>();
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);

            string Query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, 
                                   People.ThirdName, People.LastName, People.DateOfBirth,
                                   People.Gendor, People.Address, People.Phone, People.Email, 
                                   People.NationalityCountryID, Countries.CountryName
                            FROM People INNER JOIN Countries 
                            ON People.NationalityCountryID = Countries.CountryID";

            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PersonDTO person = new PersonDTO
                    {
                        PersonID = (int)reader["PersonID"],
                        NationalNo = (string)reader["NationalNo"],
                        FirstName = (string)reader["FirstName"],
                        SecondName = (string)reader["SecondName"],
                        ThirdName = reader["ThirdName"] != DBNull.Value ? (string)reader["ThirdName"] : "",
                        LastName = (string)reader["LastName"],
                        DateOfBirth = (DateTime)reader["DateOfBirth"],
                        Gendor = (short)reader["Gendor"],
                        Address = (string)reader["Address"],
                        Phone = (string)reader["Phone"],
                        Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : "",
                        NationalityCountryID = (int)reader["NationalityCountryID"],
                        CountryName = (string)reader["CountryName"]
                    };
                    personsList.Add(person);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في GetAllPersons: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return personsList;
        }

        // إضافة شخص جديد
        public static int AddPerson(string FirstName, string SecondName, string ThirdName, string LastName,
                           string NationalNo, DateTime DateOfBirth, short Gendor, string Address,
                           string Phone, string Email, int NationalityCountryID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            int PersonID = -1;
            
            string Query = @"INSERT INTO People 
                           (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, 
                            Address, Phone, Email, NationalityCountryID)
                           VALUES
                           (@nationalNo, @firstName, @secondName, @thirdName, @lastName, @dateOfBirth, 
                            @gendor, @address, @phone, @email, @nationalityCountryID);
                           SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@firstname", FirstName);
            command.Parameters.AddWithValue("@secondname", SecondName);
            command.Parameters.AddWithValue("@thirdname", ThirdName != "" && ThirdName != null ? (object)ThirdName : DBNull.Value);
            command.Parameters.AddWithValue("@lastname", LastName);
            command.Parameters.AddWithValue("@nationalNo", NationalNo);
            command.Parameters.AddWithValue("@dateofbirth", DateOfBirth);
            command.Parameters.AddWithValue("@gendor", Gendor);
            command.Parameters.AddWithValue("@address", Address);
            command.Parameters.AddWithValue("@phone", Phone);
            command.Parameters.AddWithValue("@email", Email != "" && Email != null ? (object)Email : DBNull.Value);
            command.Parameters.AddWithValue("@nationalitycountryid", NationalityCountryID);

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int id))
                {
                    PersonID = id;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في AddPerson: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return PersonID;
        }

        // تحديث بيانات الشخص
        public static bool UpdatePerson(int PersonID, string FirstName, string SecondName, string ThirdName, 
                            string LastName, string NationalNo, DateTime DateOfBirth, short Gendor, 
                            string Address, string Phone, string Email, int NationalityCountryID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            
            string Query = @"UPDATE People SET
                            NationalNo = @NationalNo,
                            FirstName = @FirstName,
                            SecondName = @SecondName,
                            ThirdName = @ThirdName,
                            LastName = @LastName,
                            DateOfBirth = @DateOfBirth,
                            Gendor = @Gendor,
                            Address = @Address,
                            Phone = @Phone,
                            Email = @Email,
                            NationalityCountryID = @NationalityCountryID
                        WHERE PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@personID", PersonID);
            command.Parameters.AddWithValue("@nationalNo", NationalNo);
            command.Parameters.AddWithValue("@firstname", FirstName);
            command.Parameters.AddWithValue("@secondname", SecondName);
            command.Parameters.AddWithValue("@thirdname", ThirdName != "" && ThirdName != null ? (object)ThirdName : DBNull.Value);
            command.Parameters.AddWithValue("@lastname", LastName);
            command.Parameters.AddWithValue("@dateofbirth", DateOfBirth);
            command.Parameters.AddWithValue("@gendor", Gendor);
            command.Parameters.AddWithValue("@address", Address);
            command.Parameters.AddWithValue("@phone", Phone);
            command.Parameters.AddWithValue("@email", Email != "" && Email != null ? (object)Email : DBNull.Value);
            command.Parameters.AddWithValue("@nationalitycountryid", NationalityCountryID);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في UpdatePerson: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return (RowsAffected > 0);
        }

        // جلب شخص بالمعرف
        public static bool GetPersonByID(int PersonID, ref string FirstName, ref string SecondName, 
                           ref string ThirdName, ref string LastName, ref string NationalNo, 
                           ref DateTime DateOfBirth, ref short Gendor, ref string Address,
                           ref string Phone, ref string Email, ref int NationalityCountryID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            bool IsFound = false;
            string Query = "SELECT * FROM People WHERE PersonID = @personID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@personID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = reader["ThirdName"] != DBNull.Value ? (string)reader["ThirdName"] : "";
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (short)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : "";
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في GetPersonByID: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        // جلب شخص برقم الهوية
        public static bool GetPersonByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, 
                            ref string SecondName, ref string ThirdName, ref string LastName,
                            ref DateTime DateOfBirth, ref short Gendor, ref string Email,
                            ref string Phone, ref string Address, ref int NationalityCountryID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            bool IsFound = false;
            string Query = "SELECT * FROM People WHERE NationalNo = @nationalNo";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@nationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    PersonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = reader["ThirdName"] != DBNull.Value ? (string)reader["ThirdName"] : "";
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (short)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : "";
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في GetPersonByNationalNo: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        // حذف شخص
        public static bool DeletePersonByPersonID(int PersonID)
        {
            int RowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            string Query = "DELETE FROM People WHERE PersonID = @personID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@personID", PersonID);

            try
            {
                connection.Open();
                RowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في DeletePersonByPersonID: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return (RowAffected > 0);
        }

        // التحقق من وجود شخص برقم الهوية
        public static bool IsPersonIsExist(string NationalNo)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            bool Exist = false;
            string Query = "SELECT 1 FROM People WHERE NationalNo = @nationalNo";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@nationalNo", NationalNo);
            
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Exist = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في IsPersonIsExist: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return Exist;
        }

        // التحقق من وجود شخص بالمعرف
        public static bool IsPersonIsExist(int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessLayerSetting.ConnectionString);
            bool Exist = false;
            string Query = "SELECT 1 FROM People WHERE PersonID = @personID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@personID", PersonID);
            
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Exist = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطأ في IsPersonIsExist: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return Exist;
        }
    }
}