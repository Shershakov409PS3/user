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
using UserWorks.Models;

namespace UserWorks.Windows
{
    /// <summary>
    /// Логика взаимодействия для UserAddEditWindow.xaml
    /// </summary>
    public partial class UserAddEditWindow : Window
    {
        bool isAdd = false;
        User user;

        public UserAddEditWindow(User user)
        {
            InitializeComponent();
            comboxRole.ItemsSource = Roles.All;

            if (user == null)
            {
                txb_Title_UserAddEdit.Text = "Добавить";
                user = new User();
                isAdd = true;
            }
            else
            {
                txb_Title_UserAddEdit.Text = "Изменить";

                txb_login.Text = user.Login;
                txb_password.Text = user.Password;
                txb_passwordVerifyde.Text = user.Password;
                comboxRole.SelectedIndex = (int)user.IdRole - 1;

                if (CurrentUser.currentUser != null && CurrentUser.currentUser.Id == user.Id)
                {
                    comboxRole.IsEnabled = false;
                    txb_login.IsEnabled = false;
                }
            }
            this.user = user;
        }

        public void windowActive(Window nextWindow)
        {
            nextWindow.Show();
            this.Close();
        }

        private void btn_SaveUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txb_password.Text != txb_passwordVerifyde.Text)
                {
                    MessageBox.Show("Пароли должны совпадать", "Предупреждение",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int selectedRoleId = comboxRole.SelectedIndex + 1;

                if (CurrentUser.currentUser != null && CurrentUser.currentUser.IdRole == 2)
                {
                    if (selectedRoleId == 1 || selectedRoleId == 2)
                    {
                        MessageBox.Show("У вас нет прав для создания администраторов и модераторов", "Ошибка доступа",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                this.user.Login = txb_login.Text;
                this.user.Password = txb_password.Text;

                if (isAdd || CurrentUser.currentUser == null || CurrentUser.currentUser.Id!= user.Id)
                {
                    this.user.IdRole = comboxRole.SelectedIndex + 1;
                }

                if (isAdd)
                {
                    Bd.Context.User.Add(this.user);
                }

                Bd.Context.SaveChanges();
                MessageBox.Show("Сохранение выполнено", "Ура",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetShortError(ex), "Ошибка (короткая версия)",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.Close();

        }

        private string GetShortError(Exception ex)
        {
            string fullMessage = ex.ToString();
            string marker = "Выполнение" +
                " данной инструкции было прервано.";

            string[] error = fullMessage.Split(new string[] {marker}, StringSplitOptions.None);

            if (error.Length > 1) 
            {
                return error[0] + marker;
            } else
            {
                return fullMessage;
            }
        }

        private void btn_cancelSaveUser_Click(object sender, RoutedEventArgs e)
        {
            windowActive(new MainWindow());
        }

    }
}
