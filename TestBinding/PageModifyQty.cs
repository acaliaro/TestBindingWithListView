using System;
using Xamarin.Forms;

namespace TestBinding
{
	public class PageModifyQty : ContentPage
	{
		public PageModifyQty( Model bindingContext)
		{
			Label l = new Label() {Text = "You are editing QTY because you have tapped on QTY" };
			Entry entry = new Entry() { Keyboard = Keyboard.Numeric };
			entry.BindingContext = bindingContext;
			entry.SetBinding(Entry.TextProperty, "Qty");

			StackLayout sl = new StackLayout { Children = {l, entry } };
			Content = sl;
		}
	}
}

