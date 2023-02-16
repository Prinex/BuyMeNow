namespace BuyMeNow.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    public bool isBusy;

    [ObservableProperty]
    public string title;
}