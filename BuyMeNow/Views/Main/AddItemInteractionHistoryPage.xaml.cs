namespace BuyMeNow.Views.Main;

public partial class AddItemInteractionHistoryPage : ContentPage
{
	public AddItemInteractionHistoryPage(AddItemInteractionHistoryPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}