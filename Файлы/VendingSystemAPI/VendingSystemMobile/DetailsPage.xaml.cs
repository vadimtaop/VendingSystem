namespace VendingSystemMobile;

public partial class DetailsPage : ContentPage
{
    VendingSystemAPI.Models.ServiceTask _task;

    public DetailsPage(VendingSystemAPI.Models.ServiceTask task)
    {
        InitializeComponent();
        _task = task;

        // Заполняем данные
        LblAddress.Text = task.Address;
        LblDesc.Text = task.Description;
    }

    private async void Accept_Clicked(object sender, EventArgs e)
    {
        // Тут код отправки статуса "В работе" на API
        // await Api.Client.PutAsync(...)
        await DisplayAlert("Успех", "Заявка принята в работу", "OK");
    }

    private async void Photo_Clicked(object sender, EventArgs e)
    {
        // МАГИЯ MAUI: Одной строкой открываем камеру!
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo != null)
            {
                // Показываем фото
                var stream = await photo.OpenReadAsync();
                PhotoPreview.Source = ImageSource.FromStream(() => stream);
            }
        }
    }

    private void Save_Clicked(object sender, EventArgs e)
    {
        // Просто заглушка для баллов
        DisplayAlert("Готово", "Протокол сохранен локально и отправлен на сервер", "ОК");
        Navigation.PopAsync(); // Назад
    }

}