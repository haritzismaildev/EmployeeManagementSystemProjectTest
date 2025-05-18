using Microsoft.AspNetCore.DataProtection;
using System;

namespace EmployeeManagementSystem.Helpers
{
    public static class EncryptionHelper
    {
        private static readonly IDataProtector _protector;

        static EncryptionHelper()
        {
            // Inisialisasi provider enkripsi untuk aplikasi ini
            var provider = DataProtectionProvider.Create("EmployeeManagementSystem");
            _protector = provider.CreateProtector("DateOfBirthProtector");
        }

        public static string EncryptDate(DateTime date)
        {
            // Format ISO 8601 untuk kemudahan parsing
            string plainText = date.ToString("O");
            return _protector.Protect(plainText);
        }

        public static DateTime DecryptDate(string cipherText)
        {
            string plainText = _protector.Unprotect(cipherText);
            return DateTime.Parse(plainText, null, System.Globalization.DateTimeStyles.RoundtripKind);
        }
    }
}
