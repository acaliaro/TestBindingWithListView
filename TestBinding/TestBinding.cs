using System;

using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace TestBinding
{
	public class App : Application
	{


		public App ()
		{
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

