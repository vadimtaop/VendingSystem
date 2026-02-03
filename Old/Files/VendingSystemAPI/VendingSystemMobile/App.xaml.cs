using VendingSystemMobile.Pages; // 1. "Прописываем" папку с нашими страницами
using VendingSystemMobile.Services; // Добавляем, чтобы работать с базой данных

namespace VendingSystemMobile
{
    public partial class App : Application
    {
        // Создаем единую, "глобальную" точку доступа к нашей базе данных
        // чтобы все страницы могли с ней работать
        public static DatabaseService Database { get; private set; }
        public App()
        {
            InitializeComponent();

            // --- ВОТ ГЛАВНЫЕ СТРОКИ ---

            // 1. Создаем единственный экземпляр нашей БД при старте приложения
            Database = new DatabaseService();

            // 2. Указываем, что первая страница - это AuthPage, которая теперь лежит в папке Pages
            MainPage = new NavigationPage(new LoginPage());
        }

    }
}