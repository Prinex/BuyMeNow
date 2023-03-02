namespace BuyMeNow.Views.Main;

public partial class EditAccountPage : ContentPage
{
	public EditAccountPage(EditAccountPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}