using Radical.Model;
using Radical.Samples.ComponentModel;
using Radical.Windows.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace Radical.Samples.Presentation.Commanding
{
	[Sample( Title = "Auto Command Binding", Category = Categories.Commanding )]
	public class AutoCommandBindingViewModel : SampleViewModel
	{
		public class Item : Entity
		{
			public void Execute()
			{
				ExecutedOn = string.Format( " {0}: {1}", Name, DateTime.Now.ToLongTimeString() );
			}

			public string Name { get; set; }

			public string ExecutedOn
			{
				get { return GetPropertyValue( () => ExecutedOn ); }
				set { SetPropertyValue( () => ExecutedOn, value ); }
			}
		}

		public AutoCommandBindingViewModel()
		{
			this.GetPropertyMetadata( () => IsActive )
				.AddCascadeChangeNotifications( () => CanExecute );

			Items = new ObservableCollection<Item>() 
			{
				new Item(){ Name = "Foo"},
				new Item(){ Name = "Bar"},
			};
		}

		public bool CanExecute
		{
			get { return IsActive; }
		}

		[KeyBindingAttribute(System.Windows.Input.Key.B, Modifiers= System.Windows.Input.ModifierKeys.Control)]
		public void Execute()
		{
			Executed = DateTime.Now.ToLongTimeString();
		}

		public string Executed
		{
			get { return this.GetPropertyValue( () => Executed ); }
			set { this.SetPropertyValue( () => Executed, value ); }
		}

		public bool IsActive
		{
			get { return this.GetPropertyValue( () => IsActive ); }
			set { this.SetPropertyValue( () => IsActive, value ); }
		}

		public ObservableCollection<Item> Items { get; private set; }
	}
}