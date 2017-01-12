using System;
using System.Collections.ObjectModel;
using PropertyChanged;
using Acr.UserDialogs;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;

namespace TestBinding
{

	[ImplementPropertyChanged]
	public class MyPageViewModel
	{
		bool _isLabelEmptyVisible { get; set; }
		Model _selectedItem { get; set; }
		bool _isTapped { get; set; }
		int _count { get; set; }

		int Count {
			get { return _count; }
			set { _count = value;
				IsListViewVisible = (_count != 0);
				IsLabelEmptyVisible = (_count == 0); }
		}

		public bool IsLabelEmptyVisible { get; set; }
		public bool IsListViewVisible { get; set; }
		public ObservableCollection<Model> List { get; set; } = new ObservableCollection<Model>();

		public MyPageViewModel()
		{
			List.Add(new Model { Description = "D1", Cost = 10.0, Qty = 1 });
			List.Add(new Model { Description = "D2", Cost = 20.0, Qty = 2 });
			List.Add(new Model { Description = "D3", Cost = 30.0, Qty = 3 });
			Count = List.Count;

			this.QtyCommand = new Command(async (object obj) => {

				try {

					if (_isTapped)
						return;
					_isTapped = true;
					await Application.Current.MainPage.Navigation.PushAsync(new PageModifyQty((Model)obj), false);
					_isTapped = false;
				}
				catch (Exception ex) { 
					_isTapped = false;
					await Application.Current.MainPage.DisplayAlert("Attention", ex.Message, "Ok");
				
				}

			});

			this.TrashCommand = new Command(async (object obj) => {

				try
				{
					if (_isTapped)
						return;

					if (obj != null)
						System.Diagnostics.Debug.WriteLine("Obj is not null");
					else
						System.Diagnostics.Debug.WriteLine("Obj IS null");


					_isTapped = true;
					var ret = await Application.Current.MainPage.DisplayAlert("Attention", "Delete this row?", "Yes", "No");

					if (ret)
					{

						int idx = List.IndexOf((Model)obj);

						List.Remove((Model)obj);
						Count = List.Count;
					}

					_isTapped = false;

				}
				catch (Exception ex) {
					_isTapped = false;
					await Application.Current.MainPage.DisplayAlert("Attention", ex.Message, "Ok");
				}
			});
		}

		public Model SelectedItem { 
			get { return _selectedItem; }
			set {
				_selectedItem = value;

				if (_selectedItem != null) {

					//Task.Run(async () =>
					//{
						Device.BeginInvokeOnMainThread(async() =>
						{
							var ret = await Application.Current.MainPage.DisplayActionSheet("Select", "Cancel", "Destruction", new string[] { "Edit", "Delete" });


							if (ret == "Edit")
							{

								PromptConfig promptConfig = new PromptConfig();
								promptConfig.CancelText = "CANCEL";
								promptConfig.InputType = InputType.Number;
								promptConfig.Message = "Modify QTA";
								promptConfig.OkText = "OK";
								promptConfig.Title = "UPDATE";
								PromptResult result = await UserDialogs.Instance.PromptAsync(promptConfig);
								if (result.Ok)
									SelectedItem.Qty = int.Parse(result.Value);

							}
							else if (ret == "Delete")
							{
								List.Remove(SelectedItem);
								Count = List.Count;
							}
							else { }
						});
					//});
				}
			}
		}

		public ICommand TrashCommand { get; protected set;}
		public ICommand QtyCommand { get; protected set;}
	}
}
