using System;
using System.Collections.Generic;
using System.Globalization;
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

			Label l = new Label() { Text = "You can TAP on Description, Trash or on QTY... there are two different answers because Description raise a SelectedItem, Trash and Qty raise a TapGestureRecognizer. If you delete all rows, a Label appears. If you longpress a row, a ContextAction is executed. If you tap on the Image, it changes" };
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

				Label lTrash = new Label { Text = "Trash", VerticalTextAlignment = TextAlignment.Center };
				lTrash.GestureRecognizers.Add(tgrTrash);

				// Image UP/DOWN 1
				TapGestureRecognizer tgrUpDown1 = new TapGestureRecognizer();
				tgrUpDown1.SetBinding(TapGestureRecognizer.CommandProperty, new Binding("BindingContext.UpDown1Command", source: this));
				tgrUpDown1.SetBinding(TapGestureRecognizer.CommandParameterProperty, ".");

				Image imageUpDown1 = new Image() { HorizontalOptions = LayoutOptions.EndAndExpand, VerticalOptions = LayoutOptions.Start, Scale = 0.5 };
				imageUpDown1.SetBinding(Image.SourceProperty, new Binding("Checked1", BindingMode.Default, new CheckedToSourceConverter()));
				imageUpDown1.GestureRecognizers.Add(tgrUpDown1);

				// Image UP/DOWN 2
				TapGestureRecognizer tgrUpDown2 = new TapGestureRecognizer();
				tgrUpDown2.SetBinding(TapGestureRecognizer.CommandProperty, new Binding("BindingContext.UpDown2Command", source: this));
				tgrUpDown2.SetBinding(TapGestureRecognizer.CommandParameterProperty, ".");

				Image imageUpDown2 = new Image() { HorizontalOptions = LayoutOptions.EndAndExpand, VerticalOptions = LayoutOptions.Start, Scale = 0.5 };
				imageUpDown2.SetBinding(Image.SourceProperty, new Binding("Checked2", BindingMode.Default, new CheckedToSourceConverter()));
				imageUpDown2.GestureRecognizers.Add(tgrUpDown2);

				// Stack layout last row
				StackLayout slLastRow = new StackLayout() { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand, Children = { lTrash, imageUpDown1, imageUpDown2 } };

				var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
				deleteAction.SetBinding(MenuItem.CommandProperty, new Binding("BindingContext.TrashCommand", source: this));
				deleteAction.SetBinding(MenuItem.CommandParameterProperty, ".");

				slView.Children.Add(lDesc);
				slView.Children.Add(lQty);
				slView.Children.Add(slLastRow);

				ViewCell vc = new ViewCell() { View = slView };
				vc.ContextActions.Add(deleteAction);

				return vc;
			});

			lv.SetBinding(ListView.ItemsSourceProperty, "List");
			lv.SetBinding(VisualElement.IsVisibleProperty, "IsListViewVisible");
			lv.SetBinding(ListView.SelectedItemProperty, "SelectedItem");

			sl.Children.Add(l);
			sl.Children.Add(labelEmpty);
			sl.Children.Add(lv);

			Content = sl;
		}

		class CheckedToSourceConverter : IValueConverter
		{
			public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			{
				if (value != null && value is bool)
				{
					if ((bool)value)
						return "up.png";
					else
						return "down.png";
				}

				return "down.png";
			}

			public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			{
				throw new NotImplementedException();
			}
		}
	}
}

