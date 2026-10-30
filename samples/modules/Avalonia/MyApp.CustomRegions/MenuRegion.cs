using Avalonia;
using Avalonia.Controls;
using Radical.Avalonia.ComponentModel;
using Radical.Avalonia.Regions;

namespace MyApp.CustomRegions
{
    public class MenuRegion : ElementsRegion<Menu>
    {
        public MenuRegion()
        {
        }

        public MenuRegion( string name )
        {
            Name = name;
        }

        protected override void OnAdd( AvaloniaObject view )
        {
            Element.Items.Add( ( MenuItem )view );
        }

        protected override void OnRemove( AvaloniaObject view, RemoveReason reason )
        {
            if ( view is MenuItem e && Element.Items.Contains( e ) )
            {
                Element.Items.Remove( e );
            }
        }
    }
}
