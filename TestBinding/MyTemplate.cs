using Xamarin.Forms;

namespace TestBinding
{
	public class MyTemplate : ViewCell
	{
		public MyTemplate(){
			StackLayout sl = new StackLayout ();

			Label l = new Label ();
			l.SetBinding (Label.TextProperty, "Description", stringFormat:"DESCRIPTION: {0}");

			// LABEL QTY
			TapGestureRecognizer tgrQty = new TapGestureRecognizer();
			tgrQty.SetBinding(TapGestureRecognizer.CommandProperty, new Binding("BindingContext.QtyCommand", source: new MyPage()));
			tgrQty.SetBinding(TapGestureRecognizer.CommandParameterProperty, ".");

			Label lQty = new Label ();
			lQty.GestureRecognizers.Add(tgrQty);
			lQty.SetBinding (Label.TextProperty, "Qty", stringFormat:"QTY: {0}");

			// LABEL TRASH
			TapGestureRecognizer tgrTrash = new TapGestureRecognizer();
			tgrTrash.SetBinding(TapGestureRecognizer.CommandProperty, new Binding("BindingContext.TrashCommand", source: new MyPage()));
			tgrTrash.SetBinding(TapGestureRecognizer.CommandParameterProperty, ".");

			Label lTrash = new Label { Text = "Trash"};
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

