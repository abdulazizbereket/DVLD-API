using System;

namespace DVDLBusinussLayer
{
    public class clsTestTypes
    {
        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public int TestTypeFees { get; set; }

        public clsTestTypes()
        {
            TestTypeID = -1;
            TestTypeTitle = "";
            TestTypeDescription = "";
            TestTypeFees = 0;
        }

        public clsTestTypes(int TestTypeID, string TestTypeTitle, string TestTypeDescription, int TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
        }

        // جلب نوع الاختبار بالمعرف
        public static clsTestTypes Find(int TestTypeID)
        {
            // هذا يجب أن يكون من قاعدة البيانات
            // للآن نرجع قيم افتراضية
            switch (TestTypeID)
            {
                case 1:
                    return new clsTestTypes(1, "Vision Test", "Test your vision", 15);
                case 2:
                    return new clsTestTypes(2, "Written Test", "Written driving test", 10);
                case 3:
                    return new clsTestTypes(3, "Practical Test", "Practical driving test", 20);
                default:
                    return null;
            }
        }
    }
}