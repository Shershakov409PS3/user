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
    /// Логика взаимодействия для UsersList.xaml
    /// </summary>
    public partial class UsersList : Window
    {
        List<User> users;
        List<string> fields;

        public UsersList()
        {
            InitializeComponent();
            loadUsers();
        }

        public void loadUsers()
        {
            if (CurrentUser.currentUser != null && CurrentUser.currentUser.IdRole == 2)
            {
                users = Bd.Context.User.Where(u => u.Id == CurrentUser.currentUser.Id || u.IdRole == 3).ToList();
            }
            else
            {
                users = Bd.Context.User.ToList();
            }

            diagramUsers.ItemsSource = users;
            fields = new List<string>() { "Логин", "Роль" };
            cmbFields.ItemsSource = fields;

            getCountUsers();
        }

        private void getCountUsers()
        {
            List<sp_CountUserToRole_Result> counts = Bd.Context.sp_CountUserToRole().ToList();
            string str = "Количество пользователей\n";
            foreach (var item in counts) 
            {
                str += item.Роль + " : " + item.Количество_пользователей + "\n";
            }
            lbl_countUsers.Content = str;
        }

        private void windowActive(object sender)
        {
            var button = sender as Button;
            var user = button.DataContext as User;

            UserAddEditWindow userAddEditWindow = new UserAddEditWindow(user);
            userAddEditWindow.ShowDialog();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = diagramUsers.SelectedItem as User;

            if (CurrentUser.currentUser != null && selectedUser.Id == CurrentUser.currentUser.Id)
            {
                MessageBox.Show("Вы не можете удалить сами себя!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var confDeletion = MessageBox.Show("Вы действительно хотите удалить пользователя из системы",
                "Подтвердите удаление пользователя", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confDeletion == MessageBoxResult.Yes)
            {
                try
                {
                    Bd.Context.User.Remove(selectedUser);
                    Bd.Context.SaveChanges();
                    loadUsers();
                    MessageBox.Show("Удаление выполнено", "Ура", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            windowActive(sender);
            loadUsers();
        }

        private void btnCreateUsers_Click(object sender, RoutedEventArgs e)
        {
            windowActive(sender);
            loadUsers();
        }

        private void btn_sort_Click(object sender, RoutedEventArgs e)
        {
            if (cmbFields.SelectedIndex == 0)
            {
                users = rbUp.IsChecked == true ?
                    users.OrderBy(u => u.Login).ToList() :
                    users.OrderByDescending(u => u.Login).ToList();
            }
            else if (cmbFields.SelectedIndex == 1)
            {
                users = rbUp.IsChecked == true ?
                    users.OrderBy(u => u.IdRole).ToList() :
                    users.OrderByDescending(u => u.IdRole).ToList();
            }
            diagramUsers.ItemsSource = users;
        }

        private void cb_Filter_Click(object sender, RoutedEventArgs e)
        {
            if (cb_Filter.IsChecked == true) 
            { 
                sp_Filter.Visibility = Visibility.Visible;
            } else
            {
                sp_Filter.Visibility=Visibility.Collapsed;
                loadUsers();
            }
        }

        private void txb_FilterWord_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filteredUsers = Bd.Context.User.ToList();

            if (CurrentUser.currentUser != null && CurrentUser.currentUser.IdRole == 2)
            {
                filteredUsers = filteredUsers.Where(u => u.Id == CurrentUser.currentUser.Id || u.IdRole == 3).ToList();
            }

            users = filteredUsers.Where(u => u.Login.Contains(txb_FilterWord.Text)).ToList();
            diagramUsers.ItemsSource = users;
        }
    }
}
