using System;
using PropertyChanged;
namespace TestBinding
{
	[ImplementPropertyChanged]
	public class Model
	{
		public Model ()
		{
		}

		public string Description { get; set; }
		public double Cost { get; set; }
		public int Qty { get; set; }
		public bool Checked1 { get; set; }
		public bool Checked2 { get; set; }
		public string BackgroundColor { get; set; }
		public string TextColor { get; set; }

	}
}

