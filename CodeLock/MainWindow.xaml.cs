﻿using CodeLock.DataModel;
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
            SetBasePassword();
            SetBaseAdminPassword();
        }
        private void SetBaseAdminPassword()
        {
            using (PasswordContext context = new PasswordContext())
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
        private void SetBasePassword()
        {
            using (PasswordContext context = new PasswordContext())
            {
                if (!context.passwords.Any(x => x.Available == "Available"))
                {
                    GeneratePassword("1111");
                }
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (isOpen)
            {
                openTime++;
            }
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
                var s = context.passwords.Single(x => x.Available == "Available");
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
        private void GeneratePassword(string str)
        {
            using (PasswordContext context = new PasswordContext())
            {
                Password pass = new Password();
                pass.Pass = GetPasswordHash(str);
                pass.Available = "Available";
                context.passwords.Add(pass);
                context.SaveChanges();
            }
        }
    }
}
