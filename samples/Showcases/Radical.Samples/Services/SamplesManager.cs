using System;
using System.Collections.Generic;
using System.Linq;
using Radical.Reflection;
using Radical.Linq;
using Radical.Windows.Presentation.ComponentModel;
using Radical.Samples.ComponentModel;

namespace Radical.Samples.Services
{
	class SamplesManager
	{
		public class SampleCategory
		{
			public SampleCategory(string name, IEnumerable<SampleItem> samples )
			{
				Name = name;
				Samples = samples;
			}

			public string Name { get; private set; }

			public IEnumerable<SampleItem> Samples
			{
				get;
				private set;
			}
		}

		public class SampleItem
		{
			public SampleItem( Type viewModelType, Type viewType )
			{
				var attribute = viewModelType.GetAttribute<SampleAttribute>();
				ViewType = viewType;

				Title = attribute.Title;
				Description = attribute.Description;

				Order = attribute.Order;
			}

			public string Title { get; private set; }
			public string Description { get; private set; }
			public int Order { get; private set; }

			public Type ViewType { get; private set; }

			public Action<SampleItem> ViewSampleHandler { get; set; }

			public void ViewSample() 
			{
				ViewSampleHandler( this );
			}
		}

		public SamplesManager( IConventionsHandler conventions )
		{
			Categories = GetAssembly.ThatContains<SamplesManager>()
				.GetTypes()
				.Where( t => t.IsAttributeDefined<SampleAttribute>() )
				.GroupBy( t =>
				{
					var sa = t.GetAttribute<SampleAttribute>();
					return sa.Category;
				} )
				.Select( g =>
				{
					return new SampleCategory
					(
						g.Key,
						g.Select( t => new SampleItem( t, conventions.ResolveViewType( t ) ) )
							.AsReadOnly()
					);
				} )
				.AsReadOnly();
		}

		public IEnumerable<SampleCategory> Categories
		{
			get;
			private set;
		}
	}
}
