using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimpleScada.Screens
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        private Timer _timer;

        List<Users> items = new List<Users>();
        public AdminPanel()
        {
            InitializeComponent();
            _timer = new Timer(250); //Updates every quarter second.
            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer.Enabled = true;



        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            items.Clear();

            using (var db = new SimpleScadaContext())
            {
                // Write Users from database to viewList on Admin Panel
                var users = db.Users.OrderBy(p => p.Id);
                foreach (var user in users)
                {
                    items.Add(new SimpleScada.Users() { Id = user.Id, User = user.User, Password = user.Password, Level = user.Level });
                }
            }
            Dispatcher.Invoke(new Action(() => { userList.ItemsSource = items; ; }));

        }

        private void adminPanelClose_Click(object sender, RoutedEventArgs e)
        {
            _timer.Enabled = false;
            MainScreen.adminPanel.Close();
        }

        private void userList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedStockObject = userList.SelectedItem as Users;
            usernameTxt.Text = selectedStockObject.User;
            passwordTxt.Text = selectedStockObject.Password;
            levelTxt.Text = selectedStockObject.Level.ToString();
        }

        // CRUD

        // Add new user
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new SimpleScadaContext())
            {

                db.Users.Add(new Users() { User = usernameTxt.Text, Password = passwordTxt.Text, Level = Int32.Parse(levelTxt.Text) });
                db.SaveChanges();

            };
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedStockObject = userList.SelectedItem as Users;
            using(var db = new SimpleScadaContext())
            {
                db.Users.First(p => p.Id == selectedStockObject.Id).User = usernameTxt.Text;
                db.Users.First(p => p.Id == selectedStockObject.Id).Password = passwordTxt.Text;
                db.Users.First(p => p.Id == selectedStockObject.Id).Level = Int32.Parse(levelTxt.Text);
                db.SaveChanges();
            };
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedStockObject = userList.SelectedItem as Users;
            using (var db = new SimpleScadaContext())
            {
                var newUser = db.Users.First(p => p.Id == selectedStockObject.Id);
                db.Users.Remove(newUser);
                db.SaveChanges();
            };
        }
    }


    }
