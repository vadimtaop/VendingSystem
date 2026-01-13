using VendingSystemMobile.Models;

namespace VendingSystemMobile.Pages;

public partial class TaskDetailsPage : ContentPage
{
    LocalServiceTask _task;

    public TaskDetailsPage(LocalServiceTask task)
    {
        InitializeComponent();
        _task = task;
        BindingContext = _task;

        AddressLabel.Text = _task.Address;
        DescLabel.Text = _task.Description;
        StatusLabel.Text = _task.Status;

        if (_task.Status == "В работе")
        {
            ChecklistLayout.IsVisible = true;
        }
    }

    private void OnAcceptClicked(object sender, EventArgs e)
    {
        _task.Status = "В работе";
        StatusLabel.Text = "В работе";
        ChecklistLayout.IsVisible = true;
        // Здесь по хорошему надо обновить в БД
    }

    private async void OnRejectClicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Отмена", "Причина отказа?");
        if (!string.IsNullOrEmpty(result))
        {
            _task.Status = "Отменена";
            StatusLabel.Text = "Отменена";
            await Navigation.PopAsync();
        }
    }

    private async void OnCompleteClicked(object sender, EventArgs e)
    {
        _task.Status = "Выполнена";
        await DisplayAlert("Успех", "Протокол сохранен локально", "OK");
        await Navigation.PopAsync();
    }
}