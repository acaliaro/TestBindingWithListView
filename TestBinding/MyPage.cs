using Xamarin.Forms;

namespace TestBinding
{
	public class MyPage : ContentPage
	{
		public MyPage()
		{

			this.BindingContext = new MyPageViewModel();

			StackLayout sl = new StackLayout();
			sl.Padding = new Thickness(20, 20, 20, 20);

			Label l = new Label() { Text = "You can TAP on Description, Trash or on QTY... there are two different answers because Description raise a SelectedItem, Trash and Qty raise a TapGestureRecognizer. If you delete all rows, a Label appears. ATTENTION. NOW TRASH DOES NOT WORKS!!!" };
			Label labelEmpty = new Label { Text = "THE LIST IS EMPTY", VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand };
			labelEmpty.SetBinding(Label.IsVisibleProperty, "IsLabelEmptyVisible");

			ListView lv = new ListView { HasUnevenRows = true };
			lv.ItemTemplate = new DataTemplate(typeof(MyTemplate));
			lv.SetBinding(ListView.ItemsSourceProperty, "List");
			lv.SetBinding(ListView.IsVisibleProperty, "IsListViewVisible");
			lv.SetBinding(ListView.SelectedItemProperty, "SelectedItem");

			sl.Children.Add(l);
			sl.Children.Add(labelEmpty);
			sl.Children.Add(lv);

			Content = sl;
		}
	}
}

