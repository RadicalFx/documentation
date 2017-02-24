using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Topics.Radical.Windows.Presentation.Regions;
using Topics.Radical.Conversions;

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
