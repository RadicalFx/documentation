using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Radical.Windows.Presentation;

namespace Radical.Samples.Presentation.OpenChildWindow
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
