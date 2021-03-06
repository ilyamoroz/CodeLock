using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeLock.DataModel;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using System.Windows.Media.Animation;

namespace CodeLock
{
    public class Database
    {
        public static int adminID = 1;
        private string userPass = "";

        //Задаёт стандартый пароль в базе данных(если она пустая)
        public void SetBasePassword()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                if (!context.passwords.Any(x => x.Available == "Available"))
                {
                    GeneratePassword("1111", 1);
                    GeneratePassword("2222", 2);
                }
            }
        }

        //Создаёт запись в базе данных для подальшого использования 
        public void GeneratePassword(string str, int _adminID)
        {
            if (str != "")
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    Password pass = new Password();
                    pass.Pass = GetPasswordHash(str);
                    pass.Available = "Available";
                    pass.Deleted = "Non-delete";
                    pass.AdminID = _adminID;
                    context.passwords.Add(pass);
                    context.SaveChanges();
                }
            }
        }

        //Получает пароль и хеширует его при записи в бд
        public string GetPassword(string password)
        {
            string str = "";
            string pass = GetPasswordHash(password);
            using (DataBaseContext context = new DataBaseContext())
            {
                var s = context.passwords.Where(x => x.Pass == pass).ToList();
                foreach (var item in s)
                {
                    if (item.Available == "Available")
                    {
                        str = item.Pass;

                        adminID = item.AdminID;

                        userPass = item.Pass;
                    }
                }
            }
            return str;
        }

        // Функция хеширования
        public string GetPasswordHash(string inputText)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(inputText));
            return Convert.ToBase64String(hash);
        }

        //Задаёт пароль админа, и хешируэт его
        public void SetBaseAdminPassword(int _AdminID, string password)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                if (!context.admins.Any(x => x.AdminID == _AdminID))
                {
                    Admin admin = new Admin();
                    admin.AdminPassword = GetPasswordHash(password);
                    context.admins.Add(admin);
                    context.SaveChanges();
                }
            }


        }

        //Изменяет пароль юзера и созраняет его в бд для ведения учёта паролей
        public void ChangePassword(string newPassword)
        {
            if (newPassword != "")
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    var pass = context.passwords.Where(x => x.Pass == userPass).ToList();
                    foreach (var item in pass)
                    {
                        if (item.Available == "Available")
                        {
                            item.Available = "Non-Available";
                        }
                    }
                    context.SaveChanges();
                }
                GeneratePassword(newPassword, adminID);
            }
            
        }

        //Изменяет пароль админа
        public void ChangeAdminPass(string newPassword)
        {
            using (DataBaseContext context = new DataBaseContext())
            {

                var pass = context.admins.Where(x => x.AdminID == adminID).ToList();
                foreach (var item in pass)
                {
                    if (item.AdminPassword == item.AdminPassword.Normalize())
                    {
                        item.AdminPassword = GetPasswordHash(newPassword);
                    }
                }
                context.SaveChanges();
            }
        }

        //Пяолучает мак адрес компютера и делает запись в бд (Срабатывает при неправельном вводе пароля юзера)
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

        //Задаёт информацию для записи в бд
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

        //Получает пароль админа для подальшой работы с ним 
        public string GetAdminPassword()
        {
            string str = "";
            using (DataBaseContext context = new DataBaseContext())
            {
                var s = context.admins.Single(x => x.AdminID == adminID);
                str = s.AdminPassword;
            }
            return str;
        }
    }

}
