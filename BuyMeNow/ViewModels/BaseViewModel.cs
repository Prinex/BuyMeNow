namespace BuyMeNow.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    public bool isBusy;

    [ObservableProperty]
    public string title;

    // isPass, img and Show password are used accross user functionalities for showing/hiding the password
    [ObservableProperty]
    public bool isPass = true;

    [ObservableProperty]
    public string img = "lock.png";

    // same case as above
    // command to show the password, security stuff
    [RelayCommand]
    public void ShowPassword()
    {
        if (Img == "lock.png")
        {
            Img = "openlock.png";
            IsPass = false;
        }
        else if (Img == "openlock.png")
        {
            Img = "lock.png";
            IsPass = true;
        }
    }
}