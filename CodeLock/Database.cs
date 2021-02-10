using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeLock.DataModel;
using System.Security.Cryptography;
using System.Net.NetworkInformation;

namespace CodeLock
{
    public class Database
    {
        public void SetBasePassword()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                if (!context.passwords.Any(x => x.Available == "Available"))
                {
                    GeneratePassword("1111");
                }
            }
        }
        public void GeneratePassword(string str)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                Password pass = new Password();
                pass.Pass = GetPasswordHash(str);
                pass.Available = "Available";
                context.passwords.Add(pass);
                context.SaveChanges();
            }
        }
        public string GetPassword()
        {
            string str = "";
            using (DataBaseContext context = new DataBaseContext())
            {
                var s = context.passwords.Single(x => x.Available == "Available");
                str = s.Pass;
            }
            return str;
        }
        public string GetPasswordHash(string inputText)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(inputText));
            return Convert.ToBase64String(hash);
        }
        public void SetBaseAdminPassword()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                if (!context.admins.Any(x => x.AdminID == 1))
                {
                    Admin admin = new Admin();
                    admin.AdminPassword = GetPasswordHash("9999");
                    context.admins.Add(admin);
                    context.SaveChanges();
                }
            }

        }
        public void ChangePassword(string newPassword)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                Password pass = context.passwords.Single(x => x.Available == "Available");
                pass.Available = "Non-Available";
                context.SaveChanges();
            }
            GeneratePassword(newPassword);
        }
        private string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }
        public void SetLoginAttempt(string password)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                LoginAttempts attempt = new LoginAttempts();
                attempt.Passwords = GetPasswordHash(password);
                attempt.MacAddress = GetMACAddress();
                context.attempts.Add(attempt);
                context.SaveChanges();
            }
        }
    }
}
