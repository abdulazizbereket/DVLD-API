using DVDLDataAccessLayer;
using System;
using DVLD_API.DTOs;

namespace DVDLBusinussLayer
{
    public class clsApplication
    {
        public enum enApplicationType
        {
            Local = 1,
            Renew = 2,
            ReplacmentLost = 3,
            ReplacmentDamaged = 4,
            Release = 5,
            NewInternational = 6,
            RetakeTest = 7
        };

        public enum enStatus
        {
            New = 1,
            Canseled = 2,
            Completed = 3
        };

        public enum enMode
        {
            Add = 1,
            Update = 2
        };

        public int ApplicationID { get; set; }
        public int PersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int UserID { get; set; }
        public enApplicationType ApplicationType { get; set; }
        public enStatus ApplicationStatus { get; set; }
        public enMode Mode { get; set; }
        public clsApplicationTypes TypeApp { get; set; }

        // Constructor للـ Add New
        public clsApplication(enApplicationType APPType)
        {
            ApplicationID = -1;
            PersonID = -1;
            ApplicationDate = DateTime.Now;
            ApplicationType = APPType;
            TypeApp = clsApplicationTypes.Find((int)ApplicationType);
            PaidFees = TypeApp.ApplicationFee;
            ApplicationStatus = enStatus.New;
            LastStatusDate = DateTime.Now;
            UserID = -1;
            Mode = enMode.Add;
        }

        // Constructor للـ Update
        private clsApplication(int ApplicationID, int PersonID, DateTime ApplicationDate, int ApplicationTypeID,
                              enStatus ApplicationStatus, DateTime LastStatusDate, int CurrentUserID)
        {
            this.ApplicationID = ApplicationID;
            this.PersonID = PersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationType = (enApplicationType)ApplicationTypeID;
            this.TypeApp = clsApplicationTypes.Find(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = TypeApp.ApplicationFee;
            this.UserID = CurrentUserID;
            Mode = enMode.Update;
        }

        // إضافة تطبيق جديد (Private)
        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(PersonID, ApplicationDate,
                                                                      (int)ApplicationType, (int)ApplicationStatus,
                                                                      PaidFees, UserID);
            return (this.ApplicationID > 0);
        }

        // تحديث التطبيق (Private)
        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(ApplicationID, PersonID, ApplicationDate,
                                                       (int)ApplicationType, (int)ApplicationStatus,
                                                       LastStatusDate, PaidFees, UserID);
        }

        // حفظ التطبيق (Add أو Update)
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateApplication();
            }
            return false;
        }

        // جلب تطبيق بالمعرف
        public static clsApplication GetApplicationByApplicationID(int ApplicationID)
        {
            int PersonID = -1, ApplicationTypeID = -1, UserID = -1, ApplicationStatus = -1;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            decimal PaidFees = 0;

            bool IsFound = clsApplicationData.GetApplicationByID(ApplicationID, ref PersonID, ref ApplicationDate,
                                                                ref ApplicationTypeID, ref ApplicationStatus,
                                                                ref LastStatusDate, ref PaidFees, ref UserID);

            if (IsFound)
            {
                return new clsApplication(ApplicationID, PersonID, ApplicationDate, ApplicationTypeID,
                                         (enStatus)ApplicationStatus, LastStatusDate, UserID);
            }
            return null;
        }

        // تحديث حالة التطبيق
        private bool UpdateApplicationStatus(int ApplicationID, int ApplicationStatus, DateTime LastStatusDate)
        {
            return clsApplicationData.UpdateApplicationStatus(ApplicationID, ApplicationStatus, LastStatusDate);
        }

        // حذف التطبيق
        public bool DeleteApplication(int ApplicationID)
        {
            return clsApplicationData.DeleteApplication(ApplicationID);
        }
    }
}