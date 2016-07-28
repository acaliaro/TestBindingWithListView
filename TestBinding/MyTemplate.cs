using System;
using Xamarin.Forms;

namespace TestBinding
{
	public class MyTemplate : ViewCell
	{
		public MyTemplate(){
			StackLayout sl = new StackLayout ();

			Label l = new Label ();
			l.SetBinding (Label.TextProperty,"Description");

			Label lQty = new Label ();
			lQty.SetBinding (Label.TextProperty, "Qty");

			sl.Children.Add (l);
			sl.Children.Add (lQty);
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

