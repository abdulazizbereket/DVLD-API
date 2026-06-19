using DVDLDataAccessLayer;
using System;
using System.Collections.Generic;
using DVLD_API.DTOs;

namespace DVDLBusinussLayer
{
    public class clsPerson
    {
        public enum _enMode { eAddNew = 0, eUpdate = 1 };
        public _enMode Mode = _enMode.eAddNew;

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
        public clsCountry CountryInfo;

        // Constructor للـ Add New
        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.NationalNo = "";
            this.DateOfBirth = new DateTime();
            this.Gendor = 0;
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.NationalityCountryID = -1;
            Mode = _enMode.eAddNew;
        }

        // Constructor للـ Update
        private clsPerson(int PersonID, string FirstName, string SecondName, string ThirdName, string LastName,
                         string NationalNo, DateTime DateOfBirth, short Gendor, string Address, string Phone,
                         string Email, int NationalityCountryID)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNo = NationalNo;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.NationalityCountryID = NationalityCountryID;
            Mode = _enMode.eUpdate;
            this.CountryInfo = clsCountry.GetCountry(NationalityCountryID);
        }

        public string FullName()
        {
            return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
        }

        // جلب جميع الأشخاص
        public static List<PersonDTO> GetAllPersons()
        {
            return clsPersonDataLayer.GetAllPersons();
        }

        // التحقق من وجود شخص برقم الهوية
        public static bool IsPersonExist(string NationalNo)
        {
            return clsPersonDataLayer.IsPersonIsExist(NationalNo);
        }

        // التحقق من وجود شخص بالمعرف
        public static bool IsPersonExist(int PersonID)
        {
            return clsPersonDataLayer.IsPersonIsExist(PersonID);
        }

        // إضافة شخص جديد (Private)
        private bool _Addperson()
        {
            this.PersonID = clsPersonDataLayer.AddPerson(this.FirstName, this.SecondName, this.ThirdName,
                                                        this.LastName, this.NationalNo, this.DateOfBirth,
                                                        this.Gendor, this.Address, this.Phone, this.Email,
                                                        this.NationalityCountryID);
            return (this.PersonID != -1);
        }

        // تحديث بيانات الشخص (Private)
        private bool _UpdatePerson()
        {
            return clsPersonDataLayer.UpdatePerson(this.PersonID, this.FirstName, this.SecondName, this.ThirdName,
                                                  this.LastName, this.NationalNo, this.DateOfBirth, this.Gendor,
                                                  this.Address, this.Phone, this.Email, this.NationalityCountryID);
        }

        // حذف شخص
        public static bool Deleteperson(int PersonID)
        {
            return clsPersonDataLayer.DeletePersonByPersonID(PersonID);
        }

        // البحث عن شخص برقم الهوية
        public static clsPerson Find(string NationalNo)
        {
            bool IsFound = false;
            int PersonID = -1;
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "",
                   Phone = "", Address = "";
            int NationalityCountryID = -1;
            DateTime DateOfBirth = DateTime.Now;
            short Gendor = 0;

            IsFound = clsPersonDataLayer.GetPersonByNationalNo(NationalNo, ref PersonID, ref FirstName, ref SecondName,
                                                              ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor,
                                                              ref Email, ref Phone, ref Address, ref NationalityCountryID);

            if (IsFound)
            {
                return new clsPerson(PersonID, FirstName, SecondName, ThirdName, LastName, NationalNo,
                                    DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID);
            }
            else
                return null;
        }

        // البحث عن شخص بالمعرف
        public static clsPerson FindpID(int PersonID)
        {
            bool IsFound = false;
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "",
                   Email = "", Phone = "", Address = "";
            int NationalityCountryID = -1;
            DateTime DateOfBirth = DateTime.Now;
            short Gendor = 0;

            IsFound = clsPersonDataLayer.GetPersonByID(PersonID, ref FirstName, ref SecondName, ref ThirdName,
                                                      ref LastName, ref NationalNo, ref DateOfBirth, ref Gendor,
                                                      ref Address, ref Phone, ref Email, ref NationalityCountryID);

            if (IsFound)
            {
                return new clsPerson(PersonID, FirstName, SecondName, ThirdName, LastName, NationalNo,
                                    DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID);
            }
            else
                return null;
        }

        // حفظ الشخص (Add أو Update)
        public bool Save()
        {
            switch (Mode)
            {
                case _enMode.eAddNew:
                    if (_Addperson())
                    {
                        Mode = _enMode.eUpdate;
                        return true;
                    }
                    else
                        return false;

                case _enMode.eUpdate:
                    return _UpdatePerson();
            }
            return false;
        }
    }
}