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

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.GetShoppingList();
    }
}