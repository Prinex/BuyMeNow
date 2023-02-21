namespace BuyMeNow;

/*
 * Some note for me:
 * Every route that is outside of the shell needs to be registered here and in mauiprogram.cs
 */
public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		this.BindingContext = new AppShellViewModel();

		Routing.RegisterRoute(nameof(SigninPage), typeof(SigninPage));
        Routing.RegisterRoute(nameof(SignupPage), typeof(SignupPage));
        Routing.RegisterRoute(nameof(ResetPasswordPage), typeof(ResetPasswordPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        
        //Routing.RegisterRoute(nameof(AccountPage), typeof(AccountPage));
        //Routing.RegisterRoute(nameof(ShoppingListsPage), typeof(ShoppingListsPage));
        //Routing.RegisterRoute(nameof(RecommendationsPage), typeof(RecommendationsPage));
        //Routing.RegisterRoute(nameof(ItemsHistoryPage), typeof(ItemsHistoryPage));
        //Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
        //Routing.RegisterRoute(nameof(TermsPage), typeof(TermsPage));
        //Routing.RegisterRoute(nameof(PrivacyPage), typeof(PrivacyPage));
    }
}
