namespace BuyMeNow.Views.Startup;

public partial class SigninPage : ContentPage
{
	public SigninPage(SigninPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}