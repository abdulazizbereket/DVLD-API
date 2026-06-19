using DVDLDataAccessLayer;
using System;
using System.Collections.Generic;
using DVLD_API.DTOs;

namespace DVDLBusinussLayer
{
    public class clsCountry
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public clsCountry()
        {
            CountryID = -1;
            CountryName = "";
        }

        private clsCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }

        // جلب جميع الدول
        public static List<CountryDTO> GetAllCountries()
        {
            return clsCountryDataLayer.GetAllCountries();
        }

        // البحث عن دولة بالمعرف
        public static clsCountry Find(int CountryID)
        {
            bool IsFound = false;
            string CountryName = "";

            IsFound = clsCountryDataLayer.GetCountryByID(CountryID, ref CountryName);

            if (IsFound)
                return new clsCountry(CountryID, CountryName);
            else
                return null;
        }

        // البحث عن دولة بالاسم
        public static clsCountry Find(string CountryName)
        {
            bool IsFound = false;
            int CountryID = -1;

            IsFound = clsCountryDataLayer.GetCountryByName(CountryName, ref CountryID);

            if (IsFound)
                return new clsCountry(CountryID, CountryName);
            else
                return null;
        }

        // الحصول على دولة بالمعرف (للاستخدام الداخلي)
        public static clsCountry GetCountry(int CountryID)
        {
            return Find(CountryID);
        }
    }
}