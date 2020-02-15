using Radical.Conversions;
using Radical.Windows.Regions;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MyApp.CustomRegions
{
    public class MenuRegion : ElementsRegion<Menu>
    {
        public MenuRegion()
        {

        }

        public MenuRegion(string name )
        {
            Name = name;
        }

        protected override void OnAdd( DependencyObject view )
        {
            Element.Items.Add( ( MenuItem )view );
        }

        protected override void OnRemove( DependencyObject view, RemoveReason reason )
        {
            view.As<MenuItem>( e =>
            {
                if ( Element.Items.Contains( e ) )
                {
                    Element.Items.Remove( e );
                }
            } );
        }
    }
}
