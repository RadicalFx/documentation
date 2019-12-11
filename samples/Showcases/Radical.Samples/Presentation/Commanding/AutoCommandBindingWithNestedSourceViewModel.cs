using System;
using Radical.Samples.ComponentModel;
using Radical.Windows.Presentation;

namespace Radical.Samples.Presentation.Commanding
{
	[Sample( Title = "Auto Command Binding (Nested source)", Category = Categories.Commanding )]
	public class AutoCommandBindingWithNestedSourceViewModel : SampleViewModel
	{
		public AutoCommandBindingWithNestedSourceViewModel()
		{
		
		}
	}

	public class NestedSource : AbstractViewModel
	{
		public long CalledOn
		{
			get { return GetPropertyValue( () => CalledOn ); }
			private set { SetPropertyValue( () => CalledOn, value ); }
		}

		public void Call()
		{
			CalledOn = DateTime.Now.Ticks;
		}
	}
}
