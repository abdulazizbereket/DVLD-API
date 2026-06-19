using DVDLDataAccessLayer;
using System;
using DVLD_API.DTOs;

namespace DVDLBusinussLayer
{
    public class clsUser
    {
        public enum _enMode { eAddNew = 0, eUpdate = 1 };
        public _enMode Mode = _enMode.eAddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public clsPerson PersonInfo { get; set; }

        // Constructor للـ Add New
        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;
            Mode = _enMode.eAddNew;
        }

        // Constructor للـ Update
        private clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            Mode = _enMode.eUpdate;
            this.PersonInfo = clsPerson.FindpID(PersonID);
        }

        // إضافة مستخدم جديد (Private)
        private bool _AddNewUser()
        {
            this.UserID = clsUserDataLayer.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (this.UserID != -1);
        }

        // تحديث بيانات المستخدم (Private)
        private bool _UpdateUser()
        {
            return clsUserDataLayer.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }

        // حفظ المستخدم (Add أو Update)
        public bool Save()
        {
            switch (Mode)
            {
                case _enMode.eAddNew:
                    if (_AddNewUser())
                    {
                        Mode = _enMode.eUpdate;
                        return true;
                    }
                    else
                        return false;

                case _enMode.eUpdate:
                    return _UpdateUser();
            }
            return false;
        }

        // البحث عن مستخدم بالمعرف
        public static clsUser FindByUserID(int UserID)
        {
            bool IsFound = false;
            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            IsFound = clsUserDataLayer.GetUserByID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive);

            if (IsFound)
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }

        // البحث عن مستخدم باسم المستخدم
        public static clsUser FindByUserName(string UserName)
        {
            bool IsFound = false;
            int UserID = -1, PersonID = -1;
            string Password = "";
            bool IsActive = false;

            IsFound = clsUserDataLayer.GetUserByUserName(UserName, ref UserID, ref PersonID, ref Password, ref IsActive);

            if (IsFound)
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }

        // حذف مستخدم
        public static bool DeleteUser(int UserID)
        {
            return clsUserDataLayer.DeleteUser(UserID);
        }
    }
}