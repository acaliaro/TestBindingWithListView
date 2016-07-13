using System;
using Xamarin.Forms;

namespace TestBinding
{
	public class MyPage : ContentPage
	{
		public MyPage ()
		{

			StackLayout sl = new StackLayout ();

			ListView lv = new ListView ();
			lv.ItemsSource = App.List;
			lv.ItemTemplate = new DataTemplate (typeof(MyTemplate));
			lv.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {

				string s = ((Model)e.SelectedItem).Description;
			};

			sl.Children.Add (lv);

			Content = sl;
		}
	}
}

