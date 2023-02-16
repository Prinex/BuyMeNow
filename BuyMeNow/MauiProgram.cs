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

        // singleton/transient for views and viewmodels
        builder.Services.AddSingleton<SigninPage>();
		builder.Services.AddTransient<SignupPage>();
		builder.Services.AddSingleton<HomePage>();
		builder.Services.AddTransient<LoadingPage>();
		// viewmodels
        builder.Services.AddSingleton<SigninPageViewModel>();
		builder.Services.AddTransient<SignupPageViewModel>();
		builder.Services.AddSingleton<HomePageViewModel>();
        builder.Services.AddTransient<LoadingPageViewModel>();

        return builder.Build();
	}
}
