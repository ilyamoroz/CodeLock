using CodeLock.DataModel;
using System;
using System.Linq;
using System.Windows.Threading;
using System.Windows;
using CodeLock.State;
using System.Media;
using CodeLock.Migrations;
using NLog;
using Serilog;

namespace CodeLock
{
    public partial class MainWindow : Window
    {
        private int openTime = 0;
        private bool IsAdmin = false;
        private Logger logger;
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

            Log.Logger = new LoggerConfiguration().WriteTo.File(@"../../information.log").CreateLogger();

            ConfigureNlog();

            logger = LogManager.GetCurrentClassLogger();

            UserRadioBtn.IsChecked = true;
            AdminRadioBtn.IsChecked = false;
            UserRadioBtn.Visibility = Visibility.Hidden;

            AdminRadioBtn.Visibility = Visibility.Hidden;
            AdminPassChangeBox.Visibility = Visibility.Hidden;

            db.SetBaseAdminPassword();
            db.SetBasePassword();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (door.state == DoorState.Open)
            {
                openTime++;
            }
            if (openTime == 60)
            {
                openTime = 0;
                IsAdmin = false;
                CloseBTN.Visibility = Visibility.Hidden;
                OpenButton.Visibility = Visibility.Visible;
                UserRadioBtn.IsChecked = true;
                LockDoor();
            }
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4 && AdminPassChangeBox.Text.Length < 4)
            {
                if (UserRadioBtn.IsChecked == true)
                {
                    PasswordField.Text += Button1.Content.ToString();
                }
                else if(AdminRadioBtn.IsChecked == true)
                {
                    AdminPassChangeBox.Text += Button1.Content.ToString();
                }
            }
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4 && AdminPassChangeBox.Text.Length < 4)
            {
                if (UserRadioBtn.IsChecked == true)
                {
                    PasswordField.Text += Button2.Content.ToString();
                }
                else
                {
                    AdminPassChangeBox.Text += Button2.Content.ToString();
                }
            }
        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4 && AdminPassChangeBox.Text.Length < 4)
            {
                if (UserRadioBtn.IsChecked == true)
                {
                    PasswordField.Text += Button3.Content.ToString();
                }
                else
                {
                    AdminPassChangeBox.Text += Button3.Content.ToString();
                }
            }
        }
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4 && AdminPassChangeBox.Text.Length < 4)
            {
                if (UserRadioBtn.IsChecked == true)
                {
                    PasswordField.Text += Button4.Content.ToString();
                }
                else
                {
                    AdminPassChangeBox.Text += Button4.Content.ToString();
                }
            }
        }
        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4 && AdminPassChangeBox.Text.Length < 4)
            {
                if (UserRadioBtn.IsChecked == true)
                {
                    PasswordField.Text += Button5.Content.ToString();
                }
                else
                {
                    AdminPassChangeBox.Text += Button5.Content.ToString();
                }
            }
        }
        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4 && AdminPassChangeBox.Text.Length < 4)
            {
                if (UserRadioBtn.IsChecked == true)
                {
                    PasswordField.Text += Button6.Content.ToString();
                }
                else
                {
                    AdminPassChangeBox.Text += Button6.Content.ToString();
                }
            }
        }
        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4 && AdminPassChangeBox.Text.Length < 4)
            {
                if (UserRadioBtn.IsChecked == true)
                {
                    PasswordField.Text += Button7.Content.ToString();
                }
                else
                {
                    AdminPassChangeBox.Text += Button7.Content.ToString();
                }
            }
        }
        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4 && AdminPassChangeBox.Text.Length < 4)
            {
                if (UserRadioBtn.IsChecked == true)
                {
                    PasswordField.Text += Button8.Content.ToString();
                }
                else
                {
                    AdminPassChangeBox.Text += Button8.Content.ToString();
                }
            }
        }
        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4 && AdminPassChangeBox.Text.Length < 4)
            {
                if (UserRadioBtn.IsChecked == true)
                {
                    PasswordField.Text += Button9.Content.ToString();
                }
                else
                {
                    AdminPassChangeBox.Text += Button9.Content.ToString();
                }
            }
        }
        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordField.Text.Length < 4 && AdminPassChangeBox.Text.Length < 4)
            {
                if (UserRadioBtn.IsChecked == true)
                {
                    PasswordField.Text += Button0.Content.ToString();
                }
                else
                {
                    AdminPassChangeBox.Text += Button0.Content.ToString();
                }
            }
        }
        private void CallButton_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer sp = new SoundPlayer();
            sp.SoundLocation = "../../call.wav";
            sp.Play();
            Log.Information("The bell rang");
            logger.Info("The bell rang");
        }
        private void Control_Click(object sender, RoutedEventArgs e)
        {
            if (door.state == DoorState.Open)
            {
                if (IsAdmin)
                {
                    UserRadioBtn.Visibility = Visibility.Visible;

                    AdminRadioBtn.Visibility = Visibility.Visible;
                    AdminPassChangeBox.Visibility = Visibility.Visible;
                    if (UserRadioBtn.IsChecked == true)
                    {
                        db.ChangePassword(PasswordField.Text);
                        User_label.Content = "";
                        Log.Information("The password changed");
                        logger.Info("The password changed");
                    }

                    if (AdminRadioBtn.IsChecked == true)
                    {
                        db.ChangeAdminPass(AdminPassChangeBox.Text);
                        User_label.Content = "";
                        Log.Information("Entered by admin");
                        logger.Info("Entered by admin");
                    }
                    UserRadioBtn.Visibility = Visibility.Hidden;
                    AdminRadioBtn.Visibility = Visibility.Hidden;
                    AdminPassChangeBox.Visibility = Visibility.Hidden;
                    AdminPassChangeBox.Text = "";

                    UserRadioBtn.IsChecked = true;
                }


                if (db.GetPasswordHash(PasswordField.Text) == db.GetAdminPassword() && IsAdmin == false)
                {
                    if (UserRadioBtn.IsChecked == true)
                    {
                        User_label.Content = "Admin";
                        PasswordField.Text = "";
                        IsAdmin = true;
                        Log.Information("Admin password changed");
                        logger.Info("Admin password changed");
                    }

                    if (AdminRadioBtn.IsChecked == true)
                    {
                        if (db.GetPasswordHash(AdminPassChangeBox.Text) == db.GetAdminPassword() && IsAdmin == false)
                        {
                            User_label.Content = "Admin";
                            AdminPassChangeBox.Text = "";
                            IsAdmin = true;
                            Log.Information("Entered by admin");
                            logger.Info("Entered by admin");

                        }
                    }
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
                Log.Information("The door is open");
                logger.Info("The door is open");
            }
            else
            {
                MessageBox.Show("The password was entered incorrectly");

                Log.Information("Login attempt");
                logger.Info("Login attempt");
                db.SetLoginAttempt(PasswordField.Text);
            }
            PasswordField.Text = "";
        }
        private void LockDoor()
        {
            door.Close();
            StatusLabel.Content = "Status: " + CheckDoorStatus();
            PasswordField.Text = "";
            Log.Information("The door is close");
            logger.Info("The door is close");

        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserRadioBtn.IsChecked == true)
            {
                PasswordField.Text = "";
            }
            else if (AdminRadioBtn.IsChecked == true)
            {
                AdminPassChangeBox.Text = "";
            }
        }
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            string usr = PasswordField.Text;
            string adm = AdminPassChangeBox.Text;

            if (UserRadioBtn.IsChecked == true)
            {
                if (usr.Length >= 1)
                {
                    usr = usr.Substring(0, usr.Length - 1);
                }
                PasswordField.Text = usr;
            }
            else if (AdminRadioBtn.IsChecked == true)
            {
                if (adm.Length >= 1)
                {
                    adm = adm.Substring(0, adm.Length - 1);
                }
                PasswordField.Text = adm;
            }
        }
        private void CloseBTN_Click(object sender, RoutedEventArgs e)
        {
            LockDoor();
            OpenButton.Visibility = Visibility.Visible;
            IsAdmin = false;
            CloseBTN.Visibility = Visibility.Hidden;

            UserRadioBtn.IsChecked = true;
            AdminRadioBtn.IsChecked = false;
            UserRadioBtn.Visibility = Visibility.Hidden;

            AdminRadioBtn.Visibility = Visibility.Hidden;
            AdminPassChangeBox.Visibility = Visibility.Hidden;


        }
        private string CheckDoorStatus()
        {
            string status = string.Empty;
            if (door.state == DoorState.Open)
            {
                status = "Unlock";
            }
            else if(door.state == DoorState.Close)
            {
                status = "Lock";
            }
            return status;
        }
        private void ConfigureNlog()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: Console
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);

            // Apply config
            NLog.LogManager.Configuration = config;
        }
    }
}
