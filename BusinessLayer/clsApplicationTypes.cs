using System;

namespace DVDLBusinussLayer
{
    public class clsApplicationTypes
    {
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFee { get; set; }

        public clsApplicationTypes()
        {
            ApplicationTypeID = -1;
            ApplicationTypeTitle = "";
            ApplicationFee = 0;
        }

        public clsApplicationTypes(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationFee)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFee = ApplicationFee;
        }

        // جلب نوع التطبيق بالمعرف
        public static clsApplicationTypes Find(int ApplicationTypeID)
        {
            // هذا يجب أن يكون من قاعدة البيانات
            // للآن نرجع قيم افتراضية
            switch (ApplicationTypeID)
            {
                case 1:
                    return new clsApplicationTypes(1, "Local License", 20);
                case 2:
                    return new clsApplicationTypes(2, "Renew License", 15);
                case 3:
                    return new clsApplicationTypes(3, "Replacement Lost", 10);
                case 4:
                    return new clsApplicationTypes(4, "Replacement Damaged", 10);
                case 5:
                    return new clsApplicationTypes(5, "Release", 5);
                case 6:
                    return new clsApplicationTypes(6, "New International", 50);
                case 7:
                    return new clsApplicationTypes(7, "Retake Test", 8);
                default:
                    return null;
            }
        }
    }
}