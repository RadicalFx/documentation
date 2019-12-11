using System;

namespace Radical.Samples.ComponentModel
{
	[AttributeUsage( AttributeTargets.Class, Inherited = false, AllowMultiple = false )]
	public sealed class SampleAttribute : Attribute
	{
		public SampleAttribute()
		{
			Category = Categories.Default;
			Description = "<-- not set -->";
		}

		public string PageUri { get; set; }
		public string Title { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }
		public int Order { get; set; }
		//public IEnumerable<String> Tags { get; set; }
	}
}
