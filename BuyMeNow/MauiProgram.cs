using Microsoft.Extensions.Logging;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<SigninPage>();
        builder.Services.AddSingleton<SigninPageViewModel>();

        return builder.Build();
	}
}
