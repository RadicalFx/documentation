using Radical.ComponentModel.Messaging;
using Radical.Messaging;
using OpenChildWindowSample.Presentation;
using Radical.Windows.ComponentModel;
using System.Windows;

namespace OpenChildWindowSample.Messaging.Handlers
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
