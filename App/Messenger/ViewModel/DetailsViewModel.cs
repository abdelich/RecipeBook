using CommunityToolkit.Mvvm.ComponentModel;

namespace Messenger.ViewModel
{
    [QueryProperty("Receipt", "receipt")]
    public partial class DetailsViewModel:ObservableObject
    {
        [ObservableProperty]
        Receipt receipt;
    }
}
