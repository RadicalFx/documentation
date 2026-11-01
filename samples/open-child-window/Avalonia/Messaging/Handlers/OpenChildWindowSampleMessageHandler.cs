using Avalonia.Controls;
using OpenChildWindowSample.Presentation;
using Radical.Avalonia.ComponentModel;
using Radical.ComponentModel.Messaging;
using Radical.Messaging;
using System.Threading.Tasks;

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

        public override Task HandleAsync( object sender, OpenChildWindowSampleMessage message )
        {
            var view = viewResolver.GetView<ChildView>();
            var owner = conventions.GetViewOfViewModel( sender ) as Window;
            if( message.AsDialog )
            {
                _ = view.ShowDialog( owner );
            }
            else
            {
                view.Show( owner );
            }

            return Task.CompletedTask;
        }
    }
}
