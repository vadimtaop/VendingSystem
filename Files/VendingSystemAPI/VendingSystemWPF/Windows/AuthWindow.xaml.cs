using Newtonsoft.Json;
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
using VendingSystemWPF.ADO;
using VendingSystemWPF.Services;

namespace VendingSystemWPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        //private ApiService _apiService; // Для API
        public AuthWindow()
        {
            InitializeComponent();

            //_apiService = new ApiService(); // Для API
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = LoginTextBox.Text.Trim();
                string password = PasswordBox.Password.Trim(); // В реале нужен хеш, но для старта так

                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Введите логин и пароль!");
                    return;
                }

                // ЗАПРОС НАПРЯМУЮ В БД ЧЕРЕЗ ADO (Entity Framework)
                var user = AppData.db.Users.FirstOrDefault(u => u.Login == login && u.PasswordHash == password);

                if (user != null)
                {
                    // Получаем роль (предполагаем, что Role - это внешний ключ или int)
                    // Проверь в своей модели, как называется свойство навигации (Roles или RoleNavigation)
                    string roleName = user.Roles != null ? user.Roles.Title : "Сотрудник";
                    string fio = $"{user.Surname} {user.Name}";

                    // Открываем главное окно
                    GeneralWindow generalWindow = new GeneralWindow(fio, roleName);
                    generalWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка БД: " + ex.Message);
            }
        }
    }
}
