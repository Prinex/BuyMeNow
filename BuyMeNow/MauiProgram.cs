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

		// Services 
		builder.Services.AddSingleton<IAccountService, AccountService>();

        // singleton/transient for services, views, and viewmodels
        builder.Services.AddSingleton<SigninPage>();
		builder.Services.AddTransient<SignupPage>();
		builder.Services.AddTransient<ResetPasswordPage>();
        builder.Services.AddTransient<LoadingPage>();
        builder.Services.AddSingleton<HomePage>();
		builder.Services.AddSingleton<TermsPage>();
        builder.Services.AddSingleton<PrivacyPage>();
        builder.Services.AddSingleton<AboutPage>();
        // viewmodels
        builder.Services.AddSingleton<SigninPageViewModel>();
		builder.Services.AddTransient<SignupPageViewModel>();
		builder.Services.AddTransient<ResetPasswordPageViewModel>();

        builder.Services.AddTransient<LoadingPageViewModel>();
        builder.Services.AddSingleton<HomePageViewModel>();

        return builder.Build();
	}
}
