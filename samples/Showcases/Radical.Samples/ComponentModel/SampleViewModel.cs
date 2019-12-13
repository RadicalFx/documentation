using Radical.Reflection;
using Radical.Windows.Presentation;

namespace Radical.Samples.ComponentModel
{
	public abstract class SampleViewModel : AbstractViewModel
	{
		protected SampleViewModel()
		{
			var attribute = GetType().GetAttribute<SampleAttribute>();
			Title = attribute.Title;
			Description = attribute.Description;
		}

		public string Title { get; private set; }
		public string Description { get; private set; }
    }
}
