namespace MauiAppNew;

public partial class SubContent : Grid
{
	public SubContent()
	{
		InitializeComponent();
	}

    private int count = 0;

    private void Button_OnClicked(object sender, EventArgs e)
    {
        count++;
        this.StyleClass ??= new List<string>();
        if (count % 2 == 1)
        {
            this.StyleClass = new List<string> { "red" };
            // VisualStateManager.GoToState(this, VisualStateManager.CommonStates.Selected);
        }
        else
        {
            // VisualStateManager.GoToState(this, VisualStateManager.CommonStates.Normal);
            this.StyleClass = new List<string> { "blue" };

            /*this.StyleClass.Remove("red");
            this.StyleClass.Add("blue");*/

        }
    }
}