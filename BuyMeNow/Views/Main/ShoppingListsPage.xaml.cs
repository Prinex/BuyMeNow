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

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.GetShoppingLists();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }
}   