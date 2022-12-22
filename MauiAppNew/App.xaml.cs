using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace MauiAppNew;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new MainPage();
	}
}
