using CodeLock.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodeLock
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if(PasswordField.Text.Length < 4)
                PasswordField.Text += Button1.Content.ToString();
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4)
                PasswordField.Text += Button2.Content.ToString();
        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4)
                PasswordField.Text += Button3.Content.ToString();
        }
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4)
                PasswordField.Text += Button4.Content.ToString();
        }
        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4)
                PasswordField.Text += Button5.Content.ToString();
        }
        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4)
                PasswordField.Text += Button6.Content.ToString();
        }
        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4)
                PasswordField.Text += Button7.Content.ToString();
        }
        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4)
                PasswordField.Text += Button8.Content.ToString();
        }
        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4)
                PasswordField.Text += Button9.Content.ToString();
        }
        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4)
                PasswordField.Text += Button0.Content.ToString();
        }
        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            string str = "";
            using (PasswordContext context = new PasswordContext())
            {
                var s = context.passwords.Single(x => x.Avaliable == "Avaliable");
                str = s.Pass;    
            }
            if (GetPasswordHash(PasswordField.Text) == str)
            {
                StatusLabel.Content = "Status: Unlock";
            }
            LockDoor();
        }
        private void LockDoor()
        {
            MessageBox.Show("Door is open");
            StatusLabel.Content = "Status: Lock";


            PasswordField.Text = "";

            /*int lockDoor = 0;
            DispatcherTimer dispatcherTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            dispatcherTimer.Start();

            dispatcherTimer.Tick += new EventHandler((object c, EventArgs eventArgs) =>
            {
                lockDoor++;
                if (lockDoor == 1000)
                {
                    
                    StatusLabel.Content = "Status: Lock";
                    ((DispatcherTimer)c).Stop();
                }
            });*/
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordField.Text = "";
        }
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            string s = PasswordField.Text;

            if (s.Length > 1)
            {
                s = s.Substring(0, s.Length - 1);
            }
            else
            {
                s = "0";
            }

            PasswordField.Text = s;
        }
        private string GetPasswordHash(string inputText)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(inputText));
            return Convert.ToBase64String(hash);
        }
        private void GenerateNewPassword()
        {
            using (PasswordContext context = new PasswordContext())
            {
                string str = PasswordField.Text;
                Password pass = new Password();
                pass.Pass = GetPasswordHash(str);
                pass.Avaliable = "Avaliable";
                context.passwords.Add(pass);
                context.SaveChanges();
            }
        }
    }
}
