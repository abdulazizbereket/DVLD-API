namespace DVLD_API.DTOs
{
    public class PersonDTO
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string NationalNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public short Gendor { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int NationalityCountryID { get; set; }
        public string CountryName { get; set; }

        public PersonDTO()
        {
            PersonID = -1;
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            NationalNo = "";
            DateOfBirth = DateTime.Now;
            Gendor = 0;
            Email = "";
            Phone = "";
            Address = "";
            NationalityCountryID = -1;
            CountryName = "";
        }
    }
}