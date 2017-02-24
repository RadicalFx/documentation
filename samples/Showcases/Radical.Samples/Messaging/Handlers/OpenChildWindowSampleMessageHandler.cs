using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Topics.Radical.ComponentModel.Messaging;
using Topics.Radical.Presentation.OpenChildWindow;
using Topics.Radical.Windows.Presentation.ComponentModel;
using Topics.Radical.Windows.Presentation.Messaging;

namespace Topics.Radical.Messaging.Handlers
{
    class OpenChildWindowSampleMessageHandler : AbstractMessageHandler<OpenChildWindowSampleMessage>, INeedSafeSubscription
    {
        readonly IViewResolver viewResolver;
        readonly IConventionsHandler conventions;

        public OpenChildWindowSampleMessageHandler( IViewResolver viewResolver, IConventionsHandler conventions )
        {
            this.viewResolver = viewResolver;
            this.conventions = conventions;
        }

        public override void Handle( object sender, OpenChildWindowSampleMessage message )
        {
            var view = this.viewResolver.GetView<ChildView>();
            view.Owner = this.conventions.GetViewOfViewModel( sender ) as Window;
            if( message.AsDialog )
            {
                view.ShowDialog();
            }
            else
            {
                view.Show();
            }
        }
    }
}
