namespace BuyMeNow.Views.Startup;

public partial class SignupPage : ContentPage
{
	public SignupPage(SignupPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}