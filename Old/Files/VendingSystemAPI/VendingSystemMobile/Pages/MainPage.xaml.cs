using Microsoft.Extensions.Logging.Abstractions;
using VendingSystemMobile.Models;
using VendingSystemMobile.Services;

namespace VendingSystemMobile.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // Ётот метод вызываетс€ каждый раз, когда страница ѕќя¬Ћя≈“—я на экране
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // «агружаем задачи из локальной базы данных и показываем их в списке
        TasksList.ItemsSource = await App.Database.GetTasksAsync();
    }

    // Ётот метод срабатывает, когда ты нажимаешь на задачу в списке
    private async void OnTaskSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is LocalServiceTask task)
        {
            // ѕереходим на страницу деталей, передава€ ей выбранную задачу
            await Navigation.PushAsync(new DetailsPage(task));

            // —нимаем выделение, чтобы можно было нажать еще раз
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}