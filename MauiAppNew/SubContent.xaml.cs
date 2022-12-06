using System.Linq;
namespace MauiAppNew;

public partial class SubContent : Grid
{
	public SubContent()
	{
		InitializeComponent();
        Loaded += (sender, args) => StyleClass = new List<string> { "blue" };
    }

    private int count = 0;

    private IList<T> FindAll<T>()
    where T:IVisualTreeElement
    {
        return this.GetVisualTreeDescendants()
            .Where(e => e is T)
            .Cast<T>()
            .ToList();
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        count++;
        var findAll = FindAll<VisualElement>();

        if (count % 2 == 1)
        {
            // this.StyleClass = new List<string> { "red" };
            VisualStateManager.GoToState(this, VisualStateManager.CommonStates.Selected);
            foreach (var label in findAll)
            {
                VisualStateManager.GoToState(label, VisualStateManager.CommonStates.Selected);
            }
        }
        else
        {
            VisualStateManager.GoToState(this, VisualStateManager.CommonStates.Normal);
            foreach (var label in findAll)
            {
                VisualStateManager.GoToState(label, VisualStateManager.CommonStates.Normal);
            }
            //this.StyleClass = new List<string> { "blue" };

            /*this.StyleClass.Remove("red");
            this.StyleClass.Add("blue");*/

        }
    }
}