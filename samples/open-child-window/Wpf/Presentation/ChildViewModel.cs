using Radical.Windows.Presentation;

namespace OpenChildWindowSample.Presentation
{
    class ChildViewModel : AbstractViewModel
    {
        public string IcomingData { get; set; }

        public string UserText
        {
            get { return GetPropertyValue( () => UserText ); }
            set { SetPropertyValue( () => UserText, value ); }
        }
    }
}
