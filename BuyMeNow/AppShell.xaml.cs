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
        Routing.RegisterRoute(nameof(EditAccountPage), typeof(EditAccountPage));
        Routing.RegisterRoute(nameof(ShoppingListPage), typeof(ShoppingListPage));
        Routing.RegisterRoute(nameof(AddUpdateShoppingListPage), typeof(AddUpdateShoppingListPage));
        Routing.RegisterRoute(nameof(AddItemPage), typeof(AddItemPage));
        Routing.RegisterRoute(nameof(AddItemInteractionHistoryPage), typeof(AddItemInteractionHistoryPage));
    }
}
