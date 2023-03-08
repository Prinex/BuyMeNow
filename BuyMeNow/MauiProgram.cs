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
			});

        // singleton/transient pattern for services, views, and viewmodels
        // binding context for pages that do not have a view model?
        // Services 
        builder.Services.AddSingleton<IAccountService, AccountService>();
        builder.Services.AddSingleton<IItemService, ItemService>();
        builder.Services.AddSingleton<IItemInteractionHistoryService, ItemInteractionHistoryService>();
        builder.Services.AddSingleton<IShoppingListService, ShoppingListService>();
        builder.Services.AddSingleton<IStoreService, StoreService>();

        // views
        builder.Services.AddSingleton<SigninPage>();
		builder.Services.AddSingleton<SignupPage>();
		builder.Services.AddTransient<ResetPasswordPage>();
        builder.Services.AddTransient<LoadingPage>();
        builder.Services.AddSingleton<HomePage>();
		builder.Services.AddSingleton<TermsPage>();
        builder.Services.AddSingleton<PrivacyPage>();
        builder.Services.AddSingleton<AboutPage>();
		builder.Services.AddSingleton<AccountPage>();
		builder.Services.AddTransient<EditAccountPage>();
        builder.Services.AddTransient<ShoppingListsPage>();
        builder.Services.AddTransient<AddUpdateShoppingListPage>();
        builder.Services.AddTransient<ShoppingListPage>();
        builder.Services.AddTransient<AddItemPage>();
        builder.Services.AddTransient<AddItemInteractionHistoryPage>();
        builder.Services.AddTransient<ItemsHistoryPage>();


        // viewmodels
        builder.Services.AddSingleton<SigninPageViewModel>();
		builder.Services.AddSingleton<SignupPageViewModel>();
		builder.Services.AddTransient<ResetPasswordPageViewModel>();
        builder.Services.AddTransient<LoadingPageViewModel>();
        builder.Services.AddSingleton<HomePageViewModel>();
		builder.Services.AddSingleton<AccountPageViewModel>();
        builder.Services.AddTransient<EditAccountPageViewModel>();
        builder.Services.AddTransient<ShoppingListsPageViewModel>();
        builder.Services.AddTransient<AddUpdateShoppingListPageViewModel>();
        builder.Services.AddTransient<ShoppingListPageViewModel>();
        builder.Services.AddTransient<AddItemPageViewModel>();
        builder.Services.AddTransient<AddItemInteractionHistoryPageViewModel>();
        builder.Services.AddTransient<ItemsHistoryPageViewModel>();

        return builder.Build();
	}
}
