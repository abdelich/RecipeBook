using Messenger.ViewModel;

namespace Messenger;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<Feed>();
		builder.Services.AddSingleton<ReceiptsViewModel>();

		builder.Services.AddSingleton<ReceiptAdd>();
		builder.Services.AddSingleton<RecipeAddViewModel>();

		builder.Services.AddTransient<DetailsPage>();
		builder.Services.AddTransient<DetailsViewModel>();

		return builder.Build();
	}
}
