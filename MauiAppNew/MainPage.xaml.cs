using ClassLibrary1;
using MauiAppNew.Library;

namespace MauiAppNew;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();

        var str = "Button";
        this.BindingContext = new ClickViewModel(action => new Command(action), () =>
        {
            str += '+';
            var contentHolder = new ContentHolder()
            {
                Header = new SubContent()
            };
            (contentHolder.Header as SubContent).BindingContext = new
            {
                Text = str,
                Good = count++ % 2 == 1
            };
            return contentHolder;
        });
    }
}