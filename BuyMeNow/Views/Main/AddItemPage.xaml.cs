namespace BuyMeNow.Views.Main;

public partial class AddItemPage : ContentPage
{
	public AddItemPage(AddItemPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}