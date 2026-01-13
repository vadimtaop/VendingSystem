namespace VendingSystemMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // --- ВОТ ПРАВИЛЬНАЯ ЛОГИКА ---

            // 1. Проверяем, есть ли сохраненный токен
            string token = Preferences.Get("auth_token", string.Empty);

            // 2. Решаем, что показать
            if (string.IsNullOrEmpty(token))
            {
                // Если токена нет -> показываем страницу входа
                MainPage = new AuthPage();
            }
            else
            {
                // Если токен есть -> сразу показываем основное приложение
                MainPage = new AppShell();
            }
        }

    }
}