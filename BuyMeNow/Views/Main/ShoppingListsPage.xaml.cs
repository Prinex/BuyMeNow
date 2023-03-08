namespace BuyMeNow.Views.Main;

public partial class ShoppingListsPage : ContentPage
{
	private ShoppingListsPageViewModel _viewModel;
    public ShoppingListsPage(ShoppingListsPageViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetShoppingLists();
    }
}   