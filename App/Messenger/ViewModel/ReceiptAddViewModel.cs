using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace Messenger.ViewModel
{
    public partial class RecipeAddViewModel : ObservableObject
    {
        string url = "http://192.168.1.156:5000";
        string post = "/api/receipt";

        private string _recipeName;
        private string _recipeDescription;
        private string _recipePhotoUrl;
        private string _recipeCookingInstructions;

        private string _ingredientName;
        private string _ingredientQuantity;

        public ICommand AddIngredientCommand { get; }
        public ICommand DeleteIngredientCommand { get; }
        public ICommand AddRecipeCommand { get; }

        [ObservableProperty]
        public ObservableCollection<string> ingredients;

        Dictionary<string, string> _recipeTranslations = new Dictionary<string, string>();

        public string RecipeName
        {
            get => _recipeName;
            set => SetProperty(ref _recipeName, value);
        }

        public string RecipeDescription
        {
            get => _recipeDescription;
            set => SetProperty(ref _recipeDescription, value);
        }

        public string RecipePhotoUrl
        {
            get => _recipePhotoUrl;
            set => SetProperty(ref _recipePhotoUrl, value);
        }

        public string RecipeCookingInstructions
        {
            get => _recipeCookingInstructions;
            set => SetProperty(ref _recipeCookingInstructions, value);
        }

        public string IngredientName
        {
            get => _ingredientName;
            set => SetProperty(ref _ingredientName, value);
        }

        public string IngredientQuantity
        {
            get => _ingredientQuantity;
            set => SetProperty(ref _ingredientQuantity, value);
        }

        public RecipeAddViewModel()
        {
            Ingredients = new ObservableCollection<string>();
            AddIngredientCommand = new RelayCommand(AddIngredient);
            DeleteIngredientCommand = new RelayCommand<string>(DeleteIngredient);
            AddRecipeCommand = new RelayCommand(AddRecipe);
        }

        private void AddIngredient()
        {
            if (!string.IsNullOrEmpty(IngredientName) && !string.IsNullOrEmpty(IngredientQuantity))
            {
                if (!_recipeTranslations.ContainsKey(IngredientName))
                {
                    Ingredients.Add($"{IngredientName} - {IngredientQuantity}");

                    _recipeTranslations.Add(IngredientName, IngredientQuantity);

                    IngredientName = string.Empty;
                    IngredientQuantity = string.Empty;
                }
            }
        }

        private void DeleteIngredient(string ingredient)
        {
            Ingredients.Remove(ingredient);
        }

        private void AddRecipe()
        {
            if(!string.IsNullOrEmpty(RecipeName) && !string.IsNullOrEmpty(RecipeDescription) 
            && !string.IsNullOrEmpty(RecipePhotoUrl) && !string.IsNullOrEmpty(RecipeCookingInstructions) && _recipeTranslations.Count > 0)
            {
                Receipt receipt = new Receipt(RecipeName, RecipePhotoUrl, RecipeDescription, _recipeTranslations, RecipeCookingInstructions, User.Name);
            
                AddReceiptToServer(receipt);
            }
            RecipeName = string.Empty;
            RecipePhotoUrl = string.Empty;
            RecipeDescription = string.Empty;
            RecipeCookingInstructions = string.Empty;
            IngredientName = string.Empty;
            IngredientQuantity = string.Empty;
            Ingredients.Clear();
            _recipeTranslations.Clear();

        }
        async void AddReceiptToServer(Receipt receipt)
        {
            using (HttpClient client = new HttpClient())
            {
                var deviceType = DeviceInfo.Platform;
                if (deviceType == DevicePlatform.Android)
                    url = "http://10.0.2.2:5000";

                string request = url + post;

                string jsonContent = JsonConvert.SerializeObject(receipt);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    // Обработка ошибки
                    Console.WriteLine($"Ошибка при добавлении рецепта. Код ошибки: {response.StatusCode}");
                }
            }
        }
    }
}
