namespace BuyMeNow;

public partial class App : Application
{
	public static Account UserDetails;
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
