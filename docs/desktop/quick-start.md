### Important note:

*Radical (for Windows and Silverlight) has a dependency on System.Windows.Interactivity that belongs to Expression Blend SDK.
In order to fully support all the Radical features the Blend SDK is required (on the developer machine):*

* *Silverlight: http://www.microsoft.com/en-us/download/details.aspx?id=3062*
* *WPF: http://www.microsoft.com/en-us/download/details.aspx?id=10801*

*The Radical.Windows NuGet Packages contains the required System.Windows.Interactivity. If you find any issue during the daily usage:*

* *manually remove the reference to System.Windows.Interactivity;*
* *install the correct SDK;*
* *add a reference to System.Windows.Interactivity from the GAC;*

## Steps to bootstrap your project in 3 minutes

* Create a new Visual Studio solution with a WPF application project;
* Add, using nuget, a reference to: Radical.Windows.Presentation.CastleWindsor;
	* this will give us the full Radical Presentation stack with the default Castle Windsor support as IoC/DI container;
* Delete the default MainWindow.xaml;
* Edit the app.xaml file to remove the StartupUri attribute;
* Add a “Presentation” folder to the project;
	* Presentation is the default location, based on a convention, where Radical Presentation looks for views and view models;
* Create 2 new items:
	* A WPF window named Main**View**.xaml (*View is important for the default conventions);
	* A class Main**ViewModel** (<*ViewName*>ViewModel is important for the default conventions);
* In the app.xaml.cs add a single line of code:

	![image](images/quickstart.png)

**Press F5 and you are up & running**: the MainView window will be shown.

The application boots, all the default and required services (for MVVM and UI Composition) are wired into Castle Windsor, the MainView is designed as the main window, at boot time the MainView is resolved and using the conventions engine the MainViewModel is setup and set as the DataContext of the MainView, in the end the MainView is shown.

## What’s next

The best topic to read now is basic concepts about the ViewModel.