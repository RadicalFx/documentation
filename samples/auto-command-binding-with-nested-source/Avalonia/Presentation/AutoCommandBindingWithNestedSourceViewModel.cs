using Radical.Avalonia.Presentation;
using System;

namespace AutoCommandBindingWithNestedSourceSample.Presentation
{
	public class AutoCommandBindingWithNestedSourceViewModel : AbstractViewModel
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
