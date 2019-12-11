using System;
using Radical.ComponentModel;
using Radical.Observers;
using Radical.Windows;
using System.Collections.Generic;
using Radical.Model;
using System.Threading.Tasks;
using System.Threading;
using Radical.Threading;
using Radical.Samples.ComponentModel;

namespace Radical.Samples.Presentation.Commanding
{
	[Sample( Title = "Auto Command Binding (with Templates)", Category = Categories.Commanding )]
	public class AutoCommandBindingWithTemplatesViewModel : SampleViewModel
	{
		public AutoCommandBindingWithTemplatesViewModel()
		{
			Children = new List<Child>()
			{
				new Child( this ){ Id = "foo" },
				new Child( this ){ Id = "bar" }
			};
		}

		public List<Child> Children { get; private set; }

		public string InvokedOn
		{
			get { return this.GetPropertyValue( () => InvokedOn ); }
			set { this.SetPropertyValue( () => InvokedOn, value ); }
		}
	}

	public class Child : Entity
	{
		AutoCommandBindingWithTemplatesViewModel owner;

		public Child( AutoCommandBindingWithTemplatesViewModel owner )
		{
			this.owner = owner;

			Status = "Idle";

			GetPropertyMetadata( () => IsDoActive )
				.AddCascadeChangeNotifications( () => CanDo );
		}

		public string Id { get; set; }

		public string Status
		{
			get { return GetPropertyValue( () => Status ); }
			set { SetPropertyValue( () => Status, value ); }
		}

		public bool IsDoActive
		{
			get { return GetPropertyValue( () => IsDoActive ); }
			set { SetPropertyValue( () => IsDoActive, value ); }
		}

		public bool CanDo
		{
			get { return IsDoActive; }
		}

		public void Do()
		{
			owner.InvokedOn = Id;
			Status = "Running: " + Id;

			//await Task.Factory.StartNew( () =>
			//{
			//    //something long running...
			//    Thread.Sleep( 2000 );
			//} );

			Status = "Completed: " + Id;
		}
	}
}