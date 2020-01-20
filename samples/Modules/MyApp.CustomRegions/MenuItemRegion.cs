using Radical.Conversions;
using Radical.Windows.Presentation.Regions;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MyApp.CustomRegions
{
    public class MenuItemRegion : ElementsRegion<MenuItem>
    {
        public MenuItemRegion()
        {

        }

        public MenuItemRegion( String name )
        {
            this.Name = name;
        }

        protected override void OnAdd( DependencyObject view )
        {
            this.Element.Items.Add( ( MenuItem )view );
        }

        protected override void OnRemove( DependencyObject view, RemoveReason reason )
        {
            view.As<MenuItem>( e =>
            {
                if ( this.Element.Items.Contains( e ) )
                {
                    this.Element.Items.Remove( e );
                }
            } );
        }
    }
}
