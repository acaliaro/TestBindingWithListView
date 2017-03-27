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

			Label l = new Label() { Text = "You can TAP on Description, Trash or on QTY... there are two different answers because Description raise a SelectedItem, Trash and Qty raise a TapGestureRecognizer. If you delete all rows, a Label appears. If you longpress a row, a ContextAction is executed" };
			Label labelEmpty = new Label { Text = "THE LIST IS EMPTY", VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand };
			labelEmpty.SetBinding(Label.IsVisibleProperty, "IsLabelEmptyVisible");

			// Moved here the Template so I can use Commands in ViewModel also in ViewCell with this binding
			ListView lv = new ListView { HasUnevenRows = true };
			lv.ItemTemplate = new DataTemplate(() =>
			{
				StackLayout slView = new StackLayout();

				Label lDesc = new Label();
				lDesc.SetBinding(Label.TextProperty, "Description", stringFormat: "DESCRIPTION: {0}");

				// LABEL QTY
				TapGestureRecognizer tgrQty = new TapGestureRecognizer();
				tgrQty.SetBinding(TapGestureRecognizer.CommandProperty, new Binding("BindingContext.QtyCommand", source: this));
				tgrQty.SetBinding(TapGestureRecognizer.CommandParameterProperty, ".");

				Label lQty = new Label();
				lQty.GestureRecognizers.Add(tgrQty);
				lQty.SetBinding(Label.TextProperty, "Qty", stringFormat: "QTY: {0}");

				// LABEL TRASH
				TapGestureRecognizer tgrTrash = new TapGestureRecognizer();
				tgrTrash.SetBinding(TapGestureRecognizer.CommandProperty, new Binding("BindingContext.TrashCommand", source: this));
				tgrTrash.SetBinding(TapGestureRecognizer.CommandParameterProperty, ".");

				Label lTrash = new Label { Text = "Trash" };
				lTrash.GestureRecognizers.Add(tgrTrash);

				var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
				deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("BindingContext.TrashCommand", source: this));

				slView.Children.Add(lDesc);
				slView.Children.Add(lQty);
				slView.Children.Add(lTrash);

				ViewCell vc = new ViewCell() {View = slView };
				vc.ContextActions.Add(deleteAction);

				return vc;
			});
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

