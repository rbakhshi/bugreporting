namespace MauiAppNew;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp(Action<MauiAppBuilder> configurer = null)
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		configurer?.Invoke(builder);

		return builder.Build();
	}
}
