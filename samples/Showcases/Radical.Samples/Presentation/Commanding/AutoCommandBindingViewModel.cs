using Radical.ComponentModel.Windows.Input;
using Radical.Model;
using Radical.Observers;
using Radical.Samples.ComponentModel;
using Radical.Windows;
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

		Fact _canExecuteWithFact;
		public Fact CanExecuteWithFact
		{
			get
			{
				if( _canExecuteWithFact == null )
				{
					_canExecuteWithFact = Fact.Create( o =>
					{
						return IsActiveWithFact;
					} )
					.AddMonitor
					(
						PropertyObserver.For( this )
							.Observe( v => v.IsActiveWithFact )
					);
				}

				return _canExecuteWithFact;
			}
		}

		public void ExecuteWithFact()
		{
			ExecutedWithFact = DateTime.Now.ToLongTimeString();
		}

		public string ExecutedWithFact
		{
			get { return this.GetPropertyValue( () => ExecutedWithFact ); }
			set { this.SetPropertyValue( () => ExecutedWithFact, value ); }
		}

		private bool _isActiveWithFact = false;
		public bool IsActiveWithFact
		{
			get { return _isActiveWithFact; }
			set
			{
				if( value != IsActiveWithFact )
				{
					_isActiveWithFact = value;
					this.OnPropertyChanged( () => IsActiveWithFact );
				}
			}
		}

		public AutoCommandBindingViewModel()
		{
			this.GetPropertyMetadata( () => IsActiveWithBoolean )
				.AddCascadeChangeNotifications( () => CanExecuteWithBoolean );

			Items = new ObservableCollection<Item>() 
			{
				new Item(){ Name = "Foo"},
				new Item(){ Name = "Bar"},
			};
		}

		public bool CanExecuteWithBoolean
		{
			get { return IsActiveWithBoolean; }
		}

		[KeyBindingAttribute(System.Windows.Input.Key.B, Modifiers= System.Windows.Input.ModifierKeys.Control)]
		public void ExecuteWithBoolean()
		{
			ExecutedWithBoolean = DateTime.Now.ToLongTimeString();
		}

		public string ExecutedWithBoolean
		{
			get { return this.GetPropertyValue( () => ExecutedWithBoolean ); }
			set { this.SetPropertyValue( () => ExecutedWithBoolean, value ); }
		}

		public bool IsActiveWithBoolean
		{
			get { return this.GetPropertyValue( () => IsActiveWithBoolean ); }
			set { this.SetPropertyValue( () => IsActiveWithBoolean, value ); }
		}

		public ObservableCollection<Item> Items { get; private set; }
	}
}