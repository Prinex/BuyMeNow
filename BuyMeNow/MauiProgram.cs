using BuyMeNow.Services;
using BuyMeNow.Views.Main;

namespace BuyMeNow;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Montserrat-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("Montserrat-Bold.ttf", "MontserratBold");
            })
            .ConfigureEssentials(essentials =>
            {
                essentials.UseMapServiceToken("AlIJtQ8qFdmEJKOKUOINwh--7niJ9xKzj1gJIdQwLZxWFQ0Dhj0tBZQJnjhz4a3d");
            });

        // singleton/transient pattern for services, views, and viewmodels
        // binding context for pages that do not have a view model?
        // Services 
        builder.Services.AddSingleton<IAccountService, AccountService>();
        builder.Services.AddSingleton<IItemService, ItemService>();
        builder.Services.AddSingleton<IItemInteractionHistoryService, ItemInteractionHistoryService>();
        builder.Services.AddSingleton<IShoppingListService, ShoppingListService>();
        builder.Services.AddSingleton<IStoreService, StoreService>();
        builder.Services.AddSingleton<IExistentItemService, ExistentItemService>();

        // API service for maps needs to have a singleton, i.e., only one instance needed
        // with a default implementeation of the API, i.e., Connectivity.Current
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Microsoft.Maui.Devices.Sensors.Geolocation.Default);
        builder.Services.AddSingleton<IMap>(Map.Default);

        // not sure about the ml model : to add dependency  injection or not?
        //builder.Services.AddSingleton<CollaborativeFilteringModel>();

        // views
        builder.Services.AddTransient<SigninPage>();
		builder.Services.AddTransient<SignupPage>();
		builder.Services.AddTransient<ResetPasswordPage>();
        builder.Services.AddTransient<LoadingPage>();
        builder.Services.AddTransient<HomePage>();
		builder.Services.AddSingleton<TermsPage>();
        builder.Services.AddSingleton<PrivacyPage>();
        builder.Services.AddSingleton<AboutPage>();
		builder.Services.AddTransient<AccountPage>();
		builder.Services.AddTransient<EditAccountPage>();
        builder.Services.AddTransient<ShoppingListsPage>();
        builder.Services.AddTransient<AddUpdateShoppingListPage>();
        builder.Services.AddTransient<ShoppingListPage>();
        builder.Services.AddTransient<AddItemPage>();
        builder.Services.AddTransient<AddItemInteractionHistoryPage>();
        builder.Services.AddTransient<ItemsHistoryPage>();
        builder.Services.AddTransient<RecommendationsPage>();


        // viewmodels
        builder.Services.AddTransient<SigninPageViewModel>();
		builder.Services.AddTransient<SignupPageViewModel>();
		builder.Services.AddTransient<ResetPasswordPageViewModel>();
        builder.Services.AddTransient<LoadingPageViewModel>();
        builder.Services.AddTransient<HomePageViewModel>();
		builder.Services.AddSingleton<AccountPageViewModel>();
        builder.Services.AddTransient<EditAccountPageViewModel>();
        builder.Services.AddTransient<ShoppingListsPageViewModel>();
        builder.Services.AddTransient<AddUpdateShoppingListPageViewModel>();
        builder.Services.AddTransient<ShoppingListPageViewModel>();
        builder.Services.AddTransient<AddItemPageViewModel>();
        builder.Services.AddTransient<AddItemInteractionHistoryPageViewModel>();
        builder.Services.AddTransient<ItemsHistoryPageViewModel>();
        builder.Services.AddTransient<RecommendationsPageViewModel>();

        return builder.Build();
	}
}
