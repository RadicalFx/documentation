using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Radical.Windows.Presentation;

namespace Topics.Radical.Presentation.OpenChildWindow
{
    class ChildViewModel : AbstractViewModel
    {
        public string IcomingData { get; set; }

        public String UserText
        {
            get { return this.GetPropertyValue( () => this.UserText ); }
            set { this.SetPropertyValue( () => this.UserText, value ); }
        }
    }
}
