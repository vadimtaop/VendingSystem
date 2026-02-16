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
using VendingSystemWeb.Ado;

namespace VendingSystemWeb.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Заполните логин!");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните пароль!");
                return;
            }

            var user = AppData.db.Users.FirstOrDefault(u => u.Login == login);

            if (user != null)
            {
                if (user.Password == password)
                {
                    string name = user.Name;
                    string role = user.Role;

                    GeneralWindow generalWindow = new GeneralWindow();
                    generalWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пароль неверный!");
                }
            }
            else
            {
                MessageBox.Show("Пользователь не найден!");
            }
        }
    }
}
