namespace BuyMeNow.Views.Main;

public partial class ItemsHistoryPage : ContentPage
{
	private ItemsHistoryPageViewModel _viewModel;
    public ItemsHistoryPage(ItemsHistoryPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		this.BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await _viewModel.GetItemInteractionHistory();
    }
}