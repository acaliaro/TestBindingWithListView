using System;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace TestBinding
{
	public class MyPage : ContentPage
	{
		public MyPage ()
		{

			StackLayout sl = new StackLayout ();
			sl.Padding = new Thickness (20, 20, 20, 20);

			ListView lv = new ListView ();
			lv.ItemsSource = App.List;
			lv.HasUnevenRows = true;
			lv.ItemTemplate = new DataTemplate (typeof(MyTemplate));
			lv.ItemSelected += async(object sender, SelectedItemChangedEventArgs e) => {

				var ret = await DisplayActionSheet ("Select", "Cancel", "Destruction", new string[]{"Edit", "Delete"});

				if(ret == "Edit"){

					PromptConfig promptConfig = new PromptConfig();
					promptConfig.CancelText = "CANCEL";
					promptConfig.InputType = InputType.Number;
					promptConfig.Message = "Modify QTA";
					promptConfig.OkText = "OK";
					promptConfig.Title  = "UPDATE";
					PromptResult result= await UserDialogs.Instance.PromptAsync (promptConfig);
					if(result.Ok)
						((Model)lv.SelectedItem).Qty = int.Parse (result.Value);

				}
				else if(ret == "Delete"){

					App.List.Remove ((Model)lv.SelectedItem);

				}
				else{}

				//string s = ((Model)e.SelectedItem).Description;
			};



			sl.Children.Add (lv);

			Content = sl;
		}
	}
}

