namespace BuyMeNow.Views.Startup;

public partial class ResetPasswordPage : ContentPage
{
	public ResetPasswordPage(ResetPasswordPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}