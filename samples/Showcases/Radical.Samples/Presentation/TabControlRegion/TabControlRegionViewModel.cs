using System.Linq;
using System.Windows;
using Radical.ComponentModel.Windows.Input;
using Radical.Samples.ComponentModel;
using Radical.Windows.Input;
using Radical.Windows.Presentation;
using Radical.Windows.Presentation.ComponentModel;

namespace Radical.Samples.Presentation.TabControlRegion
{
    [Sample(Title = "Tab Control Region", Category = Categories.UIComposition)]
    public class TabControlRegionViewModel : AbstractViewModel
    {
        public IDelegateCommand AddNewGammaViewCommand { get; set; }
        public IDelegateCommand ActivateGammaViewCommand { get; set; }
        public IDelegateCommand RemoveLastViewCommand { get; set; }

        public TabControlRegionViewModel(IViewResolver viewResolver, IRegionService regionService)
        {
            AddNewGammaViewCommand =
                DelegateCommand.Create()
                               .OnExecute(x =>
                               {
                                   var view = viewResolver.GetView<GammaView>();
                                   if (view != null)
                                   {
                                       regionService.GetKnownRegionManager<TabControlRegionView>()
                                                    .GetRegion<ISwitchingElementsRegion>("MainTabRegion")
                                                    .Add(view);
                                   }
                               });

            ActivateGammaViewCommand =
                DelegateCommand.Create()
                               .OnExecute(x =>
                               {
                                   var region = regionService.GetKnownRegionManager<TabControlRegionView>()
                                                             .GetRegion<ISwitchingElementsRegion>("MainTabRegion");
                                   
                                   var elements = region.GetElements<DependencyObject>()
                                                        .OfType<GammaView>()
                                                        .Cast<DependencyObject>()
                                                        .ToList();

                                   if (elements.Count > 0)
                                   {
                                       var index = elements.IndexOf(region.ActiveContent);
                                       if (index >= 0)
                                       {
                                           DependencyObject viewToActivate = index + 1 < elements.Count
                                                                                 ? elements[index + 1]
                                                                                 : elements[0];

                                           region.Activate(viewToActivate);
                                       }
                                       else
                                       {
                                           region.Activate(elements[0]);
                                       }
                                   }
                               });

            RemoveLastViewCommand =
                DelegateCommand.Create()
                               .OnExecute(x =>
                               {
                                   var region = regionService.GetKnownRegionManager<TabControlRegionView>()
                                                             .GetRegion<ISwitchingElementsRegion>("MainTabRegion");

                                   var lastElement = region.GetElements<DependencyObject>().LastOrDefault();
                                   if (lastElement != null)
                                   {
                                       region.Remove(lastElement);
                                   }
                               });
        }
    }
}