namespace BuyMeNow;

public partial class App : Application
{
	// only one instance of the account
	private static Account _userDetails;
	
	public static Account UserDetails { get => _userDetails; set => _userDetails = value; } 

	public App()
	{
		InitializeComponent();
		// use this only one time to import data into the database
		//Constants.ImportFilledDB();
        MainPage = new AppShell();
	}
}
