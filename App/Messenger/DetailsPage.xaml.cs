using Messenger.ViewModel;

namespace Messenger;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsViewModel vm)
	{
		InitializeComponent();	
		BindingContext = vm;
	}
}