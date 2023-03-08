namespace BuyMeNow.Views.Main;

public partial class ShoppingListPage : ContentPage
{
	private ShoppingListPageViewModel _viewModel;
    public ShoppingListPage(ShoppingListPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		this.BindingContext = viewModel; 
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetShoppingList();
    }
}