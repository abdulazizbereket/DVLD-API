namespace DVLD_API.DTOs
{
    public class ApplicationDTO
    {
        public int ApplicationID { get; set; }
        public int PersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public int ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int UserID { get; set; }

        public ApplicationDTO()
        {
            ApplicationID = -1;
            PersonID = -1;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = -1;
            ApplicationStatus = 1;
            LastStatusDate = DateTime.Now;
            PaidFees = 0;
            UserID = -1;
        }
    }
}