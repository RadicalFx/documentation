If you are building a Windows 8 app, based on WinRT, you can use our own built-in IoC container (Puzzle, originally built a couple of years ago to support Silverllight and Windows Phone).

In order to keep things simple we have tried to mimic the Windsor behavior, so if you are used to Windsor the Puzzle container will give the same familiar environment.

In order to setup your app using Puzzle it’s enough to add a reference to `Radical.Windows.Presentation.Puzzle` via [nuget](http://nuget.org/) and configure the application like in the following snippet:

	sealed partial class App : Application
	{
	    ApplicationBootstrapper bootstrapper;
	        
	    public App()
	    {
	        this.InitializeComponent();
	
	        this.bootstrapper = new PuzzleApplicationBootstrapper<Presentation.MainView>();
	    }
	}

for a detailed explanation of what’s going on take look at the WinRT Quick Start.
In the case you need to register your own components in Puzzle and the provided Bootstrap conventions does not satisfies your requirements you can leverage the power of Puzzle Descriptors and MEF: drop a class like the following in your assembly:

	[Export( typeof( IPuzzleSetupDescriptor ) )]
	public class DefaultDescriptor : IPuzzleSetupDescriptor
	{
	    public async Task Setup( IPuzzleContainer container, Func<IEnumerable<TypeInfo>> knownTypesProvider )
	    {
	        //register your components here.
	    }
	}

your installer will be automatically wired up at boot time by the infrastructure.

if, for some reason, in your components you need a dependency on the container you can add a dependency directly on `IPuzzleContainer` or on the lightweight `IServiceProvider`, they are both automatically registered as singleton at boot time.