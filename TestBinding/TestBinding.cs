using System;

using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace TestBinding
{
	public class App : Application
	{

		public static ObservableCollection<Model> List = new ObservableCollection<Model>();

		public App ()
		{
			List.Add (new Model{Description = "D1", Cost = 10.0, Qty = 1 });
			List.Add (new Model{Description = "D2", Cost = 20.0, Qty = 2 });
			List.Add (new Model{Description = "D3", Cost = 30.0, Qty = 3 });

			// The root page of your application
			MainPage = new NavigationPage(new MyPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

