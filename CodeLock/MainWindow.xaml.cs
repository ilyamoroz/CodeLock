﻿using CodeLock.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Windows;
using CodeLock.State;
using System.Media;

namespace CodeLock
{
    public partial class MainWindow : Window
    {
        private int openTime = 0;
        private bool IsAdmin = false;
        private int adminID = 0;

        Door door;
        Database db = new Database();
        
        public MainWindow()
        {
            InitializeComponent();
            door = new Door(new CloseDoorState());

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            db.SetBasePassword();
            db.SetBaseAdminPassword();
        }
        
        
        private void timer_Tick(object sender, EventArgs e)
        {
            if (door.currentState.GetType().Name == nameof(OpenDoorState))
            {
                openTime++;
            }
            if (openTime == 60)
            {
                openTime = 0;
                IsAdmin = false;
                CloseBTN.Visibility = Visibility.Hidden;
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
            if (door.currentState.GetType().Name == nameof(OpenDoorState))
            {
                if (IsAdmin)
                {
                    db.ChangePassword(PasswordField.Text);
                    User_label.Content = "";
                }

                if (db.GetPasswordHash(PasswordField.Text) == db.GetAdminPassword(adminID) && IsAdmin == false)
                {   
                    User_label.Content = "Admin";
                    PasswordField.Text = "";
                    IsAdmin = true;
                }
                PasswordField.Text = "";
            }
        }
        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            if (db.GetPasswordHash(PasswordField.Text) == db.GetPassword(PasswordField.Text))
            {
                door.Open();
                StatusLabel.Content = "Status: " + CheckDoorStatus();

                OpenButton.Visibility = Visibility.Hidden;
                CloseBTN.Visibility = Visibility.Visible;

                adminID = db.adminID;
            }
            else
            {
                MessageBox.Show("The password was entered incorrectly");
                db.SetLoginAttempt(PasswordField.Text);
            }
            PasswordField.Text = "";
        }
        private void LockDoor()
        {
            door.Close();
            StatusLabel.Content = "Status: " + CheckDoorStatus();
            PasswordField.Text = "";
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
        private void CloseBTN_Click(object sender, RoutedEventArgs e)
        {
            LockDoor();
            OpenButton.Visibility = Visibility.Visible;
            IsAdmin = false;
            CloseBTN.Visibility = Visibility.Hidden;

        }
        private string CheckDoorStatus()
        {
            string status = string.Empty;
            if (door.currentState.GetType().Name == nameof(OpenDoorState))
            {
                status = "Unlock";
            }
            else if(door.currentState.GetType().Name == nameof(CloseDoorState))
            {
                status = "Lock";
            }
            return status;
        }
    }
}
