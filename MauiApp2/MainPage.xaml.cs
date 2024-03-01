using System.ComponentModel;
using System.Diagnostics;

namespace MauiApp2;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        var w = Stopwatch.StartNew();
        BCrypt.Net.BCrypt.Verify("111111", "$2a$12$luf9xtzcPijRzyMnb1PxsuqFUsBba0ve.R.5k00XOsGf2awcHwj8a");
        w.Stop();
        Debug.WriteLine(w.Elapsed);
        
        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time {w.Elapsed}";
        else
            CounterBtn.Text = $"Clicked {count} times {w.Elapsed}";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}