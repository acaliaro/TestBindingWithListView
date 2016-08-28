using Xamarin.Forms;

namespace TestBinding
{
	public class MyTemplate : ViewCell
	{
		public MyTemplate(){
			StackLayout sl = new StackLayout ();

			Label l = new Label ();
			l.SetBinding (Label.TextProperty, "Description", stringFormat:"DESCRIPTION: {0}");

			TapGestureRecognizer tgrQty = new TapGestureRecognizer();
			tgrQty.Tapped += async (sender, e) => {
				await Application.Current.MainPage.Navigation.PushAsync(new PageModifyQty((Model)this.BindingContext), false);
			};
			Label lQty = new Label ();
			lQty.GestureRecognizers.Add(tgrQty);
			lQty.SetBinding (Label.TextProperty, "Qty", stringFormat:"QTY: {0}");

			Label lTrash = new Label { Text = "Trash"};
			TapGestureRecognizer tgrTrash = new TapGestureRecognizer();
			tgrTrash.Tapped +=async (sender, e) => {
			
				var ret = await Application.Current.MainPage.DisplayAlert("Attention", "Delete this row?", "Yes", "No");

				if (ret) {
					App.List.Remove((Model)this.BindingContext);
				}
			};
			lTrash.GestureRecognizers.Add(tgrTrash);

			sl.Children.Add (l);
			sl.Children.Add (lQty);
			sl.Children.Add(lTrash);
			View = sl;
		}

		protected override void OnBindingContextChanged ()
		{
			string s = "";
			if(this.BindingContext != null)
				s = ((Model)this.BindingContext).Description;
			System.Diagnostics.Debug.WriteLine (s);
			base.OnBindingContextChanged ();
		}
	}

}

