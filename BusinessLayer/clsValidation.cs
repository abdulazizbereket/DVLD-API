using System;

namespace DVDLBusinussLayer
{
    public class clsValidation
    {
        // التحقق من صحة البريد الإلكتروني
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // التحقق من صحة رقم الهاتف (بسيط - يجب تعديل حسب الحاجة)
        public static bool IsValidPhoneNumber(string phone)
        {
            return !string.IsNullOrEmpty(phone) && phone.Length >= 7;
        }

        // التحقق من أن النص ليس فارغ
        public static bool IsNotEmpty(string text)
        {
            return !string.IsNullOrEmpty(text) && text.Length > 0;
        }

        // التحقق من أن اسم الشخص صحيح
        public static bool IsValidName(string name)
        {
            return !string.IsNullOrEmpty(name) && name.Length >= 2 && name.Length <= 100;
        }

        // التحقق من صحة رقم الهوية
        public static bool IsValidNationalNo(string nationalNo)
        {
            return !string.IsNullOrEmpty(nationalNo) && nationalNo.Length >= 10 && nationalNo.Length <= 20;
        }

        // التحقق من أن تاريخ الميلاد معقول
        public static bool IsValidDateOfBirth(DateTime dateOfBirth)
        {
            return dateOfBirth < DateTime.Now && dateOfBirth.Year >= 1900;
        }
    }
}