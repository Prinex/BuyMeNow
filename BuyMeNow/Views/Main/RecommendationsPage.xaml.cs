namespace BuyMeNow.Views.Main;

public partial class RecommendationsPage : ContentPage
{
	private RecommendationsPageViewModel _viewModel;
	public RecommendationsPage(RecommendationsPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		this.BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.GetRecommendations();
    }
}