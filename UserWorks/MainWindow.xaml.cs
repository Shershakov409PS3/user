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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserWorks.Models;
using UserWorks.Windows;
using UserWorks.Classes;

namespace UserWorks
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (Bd.Context.User.Any())
            {
                btn_Registration.IsEnabled = false;
            }
            else 
            { 
                btn_Registration.IsEnabled = true;
            }
        }

        private void windowActive(Window nextWindow)
        {
            nextWindow.ShowDialog();
            this.Close();
        }

        private void btn_Authorization_Click(object sender, RoutedEventArgs e)
        {
            windowActive(new User_authorization());
        }

        private void btn_Registration_Click(object sender, RoutedEventArgs e)
        {
                windowActive(new UserAddEditWindow(null));
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
