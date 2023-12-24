namespace Messenger;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(Feed), typeof(Feed));
        Routing.RegisterRoute(nameof(ReceiptAdd), typeof(ReceiptAdd));
        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
    }
}
