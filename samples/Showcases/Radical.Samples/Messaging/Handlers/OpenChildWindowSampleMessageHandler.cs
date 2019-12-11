using System.Windows;
using Radical.ComponentModel.Messaging;
using Radical.Messaging;
using Radical.Samples.Presentation.OpenChildWindow;
using Radical.Windows.Presentation.ComponentModel;

namespace Radical.Samples.Messaging.Handlers
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
            var view = viewResolver.GetView<ChildView>();
            view.Owner = conventions.GetViewOfViewModel( sender ) as Window;
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
