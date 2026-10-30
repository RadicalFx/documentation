using Radical.Avalonia.ComponentModel;
using Radical.Avalonia.Input;
using Radical.Avalonia.Presentation;
using System.Linq;
using Avalonia;

namespace TabControlRegionSample.Presentation
{
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
                                   
                                   var elements = region.GetElements<AvaloniaObject>()
                                                        .OfType<GammaView>()
                                                        .Cast<AvaloniaObject>()
                                                        .ToList();

                                   if (elements.Count > 0)
                                   {
                                       var index = elements.IndexOf(region.ActiveContent);
                                       if (index >= 0)
                                       {
                                           AvaloniaObject viewToActivate = index + 1 < elements.Count
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

                                   var lastElement = region.GetElements<AvaloniaObject>().LastOrDefault();
                                   if (lastElement != null)
                                   {
                                       region.Remove(lastElement);
                                   }
                               });
        }
    }
}