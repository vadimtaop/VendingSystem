using VendingSystemMobile.Models;

namespace VendingSystemMobile.Pages;

public partial class DetailsPage : ContentPage
{
    // Здесь мы будем хранить задачу, с которой работаем на этой странице
    private LocalServiceTask _currentTask;

    public DetailsPage(LocalServiceTask task)
    {
        InitializeComponent();

        // 1. Сохраняем переданную задачу в поле _currentTask
        _currentTask = task;

        // 2. "Вписываем" данные из задачи в нужные поля на экране
        LblAddress.Text = _currentTask.Address;
        LblDesc.Text = _currentTask.Description;
        LblStatus.Text = _currentTask.Status;
    }

    private async void Accept_Clicked(object sender, EventArgs e)
    {
        // Меняем статус и сохраняем в локальную БД
        _currentTask.Status = "В работе";
        await App.Database.SaveTaskAsync(_currentTask);

        // Обновляем текст на экране
        LblStatus.Text = _currentTask.Status;

        await DisplayAlert("Успех", "Заявка принята в работу!", "OK");
    }

    private async void Reject_Clicked(object sender, EventArgs e)
    {
        // Спрашиваем причину
        string reason = await DisplayPromptAsync("Отказ", "Укажите причину отказа:");
        if (!string.IsNullOrEmpty(reason))
        {
            _currentTask.Status = $"Отклонена ({reason})";
            await App.Database.SaveTaskAsync(_currentTask);
            // Возвращаемся на предыдущую страницу
            await Navigation.PopAsync();
        }
    }

    private async void Photo_Clicked(object sender, EventArgs e)
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo != null)
            {
                // Показываем фото на экране
                PhotoPreview.Source = ImageSource.FromFile(photo.FullPath);
                PhotoPreview.IsVisible = true;
            }
        }
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        _currentTask.Status = "Выполнена";
        await App.Database.SaveTaskAsync(_currentTask);
        await DisplayAlert("Готово", "Протокол сохранен", "ОК");
        // Возвращаемся на главный экран
        await Navigation.PopAsync();
    }
}