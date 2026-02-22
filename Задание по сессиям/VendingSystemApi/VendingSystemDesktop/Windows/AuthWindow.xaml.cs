using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using VendingSystemDesktop.Ado;

namespace VendingSystemDesktop.Windows
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

        private string PasswordHash(string password)
        {
            var bytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();


            // Чтобы получить хеш пароля
            //Users users = new Users();

            //users.Password = PasswordHash(password);

            //AppData.db.Users.Add(users);
            //AppData.db.SaveChanges();



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
                if (user.Password == PasswordHash(password))
                {
                    string name = user.Name;
                    string role = user.Role;

                    GeneralWindow generalWindow = new GeneralWindow(name, role);
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
