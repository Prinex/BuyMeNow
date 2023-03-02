namespace BuyMeNow.Views.Main;

public partial class AccountPage : ContentPage
{
	private AccountPageViewModel _viewModel;
	public AccountPage(AccountPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		this.BindingContext = viewModel;
	}
	// on appearing update the credentials with the current
	// ones from the database and bind them
    protected override void OnAppearing()
    {
        base.OnAppearing();
		_viewModel.UpdateAccountDetails();
    }
}