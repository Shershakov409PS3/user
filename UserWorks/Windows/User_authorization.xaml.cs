using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UserWorks.Classes;

namespace UserWorks.Windows
{
    /// <summary>
    /// Логика взаимодействия для User_authorization.xaml
    /// </summary>
    public partial class User_authorization : Window
    {
        public User_authorization()
        {
            InitializeComponent();
        }

        private void windowActive(Window nextWindow)
        {
            nextWindow.Show();
            this.Close();
        }

        private void btn_authorization_auth_Click(object sender, RoutedEventArgs e)
        {
            var users = Bd.Context.User.ToList();

            string login = txb_authorization_login.Text.Trim();
            string password = txb_authorization_password.Text.Trim();

            bool foundUsers = false;

            foreach (var user in users) 
            { 
                if(user.Login == login && user.Password == password)
                {
                    foundUsers = true;

                    CurrentUser.currentUser = user;

                    switch (user.IdRole)
                    {
                        case 1:
                            windowActive(new UsersList());
                            break;
                        case 2:
                            windowActive(new UsersList());
                            break;
                        case 3:
                            windowActive(new UserAddEditWindow(user));
                            break;
                        default:
                            MessageBox.Show("Неизвестная роль пользователя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                    break;
                }
            }
            if (!foundUsers)
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_authorization_cancel_Click(object sender, RoutedEventArgs e)
        {
            windowActive(new MainWindow());
            this.Close();
        }

        private void txb_authorization_login_GotFocus(object sender, RoutedEventArgs e)
        {
            txb_authorization_login.Text = string.Empty;
        }

        private void txb_authorization_login_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txb_authorization_login.Text == "")
            {
                txb_authorization_login.Text = "Логин";
            }
        }

        private void txb_authorization_password_GotFocus(object sender, RoutedEventArgs e)
        {
            txb_authorization_password.Text = string.Empty;
        }

        private void txb_authorization_password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txb_authorization_password.Text == "")
            {
                txb_authorization_password.Text = "Пароль";
            }
        }
    }
}
