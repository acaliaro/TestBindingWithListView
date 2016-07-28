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

	}
}

