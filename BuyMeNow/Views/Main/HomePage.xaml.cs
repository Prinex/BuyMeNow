namespace BuyMeNow.Views.Main;

public partial class HomePage : ContentPage
{
	private HomePageViewModel _viewModel;
	public HomePage(HomePageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		this.BindingContext= viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.UpdateUsername();
    }
}