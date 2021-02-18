using CodeLock.DataModel;
using System;
using System.Linq;
using System.Windows.Threading;
using System.Windows;
using CodeLock.State;
using System.Media;
using  Serilog;

namespace CodeLock
{
    public partial class MainWindow : Window
    {
        private int openTime = 0;
        private bool IsAdmin = false;

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

            db.SetBasePassword();
            db.SetBaseAdminPassword();
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
            Log.Information("The bell rang");
        }
        private void Control_Click(object sender, RoutedEventArgs e)
        {
            if (door.state == DoorState.Open)
            {
                if (IsAdmin)
                {
                    db.ChangePassword(PasswordField.Text);

                    User_label.Content = "";
                    Log.Information("The password changed");
                }

                if (db.GetPasswordHash(PasswordField.Text) == GetAdminPassword() && IsAdmin == false)
                {
                    User_label.Content = "Admin";
                    PasswordField.Text = "";
                    IsAdmin = true;
                    Log.Information("Entered by admin");
                }
                PasswordField.Text = "";
            }
        }
        private string GetAdminPassword()
        {
            string str = "";
            using (DataBaseContext context = new DataBaseContext())
            {
                var s = context.admins.Single(x => x.AdminID == 1);
                str = s.AdminPassword;
            }
            return str;
        }
        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            if (db.GetPasswordHash(PasswordField.Text) == db.GetPassword())
            {
                door.Open();
                StatusLabel.Content = "Status: " + CheckDoorStatus();

                OpenButton.Visibility = Visibility.Hidden;
                CloseBTN.Visibility = Visibility.Visible;
                Log.Information("The door is open");
            }
            else
            {
                MessageBox.Show("The password was entered incorrectly");

                Log.Information("Login attempt");
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
            CloseBTN.Visibility = Visibility.Hidden;
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
    }
}
