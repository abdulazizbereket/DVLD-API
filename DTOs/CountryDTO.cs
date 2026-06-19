namespace DVLD_API.DTOs
{
    public class CountryDTO
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public CountryDTO()
        {
            CountryID = -1;
            CountryName = "";
        }
    }
}