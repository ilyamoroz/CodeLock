using CodeLock.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Threading;
using System.Windows;
using CodeLock.State;
using System.Media;

namespace CodeLock
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isOpen = false;
        private int openTime = 0;
        Door door;
        public MainWindow()
        {
            InitializeComponent();
            door = new Door(new CloseDoorState());

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (isOpen)
            {
                openTime++;
            }
            timerLabel.Content = openTime.ToString();
            if (openTime == 60)
            {
                openTime = 0;
                LockDoor();
            }
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
        private void CallButton_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer sp = new SoundPlayer();
            sp.SoundLocation = "../../call.wav";
            sp.Play();
        }
        private void Control_Click(object sender, RoutedEventArgs e)
        {

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
                StatusLabel.Content = "Status: " + door.OpenDoor();
                isOpen = true;
            }
            
        }
        private void LockDoor()
        {
            StatusLabel.Content = "Status: " + door.CloseDoor();
            PasswordField.Text = "";
            isOpen = false;
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordField.Text = "";
        }
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            string s = PasswordField.Text;

            if (s.Length >= 1)
            {
                s = s.Substring(0, s.Length - 1);
            }

            PasswordField.Text = s;
        }
        private string GetPasswordHash(string inputText)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(inputText));
            return Convert.ToBase64String(hash);
        }
        private void GeneratePassword()
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
