using CommunityToolkit.Mvvm.ComponentModel;
using Messenger.ViewModel;
namespace Messenger;

public partial class ReceiptAdd : ContentPage
{
    string name, description, photo_url, author, cooking;

    Dictionary<string, string> ingredients;

    RecipeAddViewModel local_rvm;
    public ReceiptAdd(RecipeAddViewModel rvm)
    {
        InitializeComponent();  // Убедитесь, что эта строка присутствует
        BindingContext = rvm;
        local_rvm = rvm;

        ingredients = new Dictionary<string, string>();
    }
}
