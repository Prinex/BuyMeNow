namespace BuyMeNow.Views.Main;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext= viewModel;
	}
}