namespace BuyMeNow.Views.Main;

public partial class AddUpdateShoppingListPage : ContentPage
{
	public AddUpdateShoppingListPage(AddUpdateShoppingListPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}