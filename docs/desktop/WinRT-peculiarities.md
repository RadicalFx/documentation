The application bootstrapper included in the WinRT version is really similar, even if not so powerful as the desktop version.

The main differences are:

* the WinRT bootstrapper does not expose all the behaviors that full version exposes;
* has a difference in the way we utilize MEF;
* is built on top of async/await;
* exposes facilities to handle OS charms;
* exposes facilities to handle application suspension;

The daily usage is really similar to the one we have already seen:

```c#
sealed partial class App : Application
{
    ApplicationBootstrapper bootstrapper;
        
    public App()
    {
        this.InitializeComponent();

        this.Suspending += OnAppSuspending;

        this.bootstrapper = new PuzzleApplicationBootstrapper<Presentation.MainView>();
    }

    async void OnAppSuspending( object sender, SuspendingEventArgs e )
    {
        var deferral = e.SuspendingOperation.GetDeferral();

        await this.bootstrapper.OnSuspending();

        deferral.Complete();
    }
}
```

We simply need to add support for the suspend operation. The other key difference is in the MEF usage, the version for WinRT does not have any catalog, in order to setup MEF we have to do the following:

```c#
protected virtual async Task<CompositionHost> CreateCompositionHost( IServiceProvider serviceProvider )
{
    var all = await this.GetCompositionAssemblies();
    var config = new ContainerConfiguration()
        .WithAssemblies( all );

    var host = config.CreateContainer();

    return host;
}
```

so, there best way to integrate your assemblies in the assemblies loaded by MEF is to use the provided hook:

```c#
this.bootstrapper = new PuzzleApplicationBootstrapper<Presentation.MainView>()
    .OnGetCompositionAssemblies( assemblies => 
    {
        return Task.Factory.StartNew( () => 
        {
            //load assemblies here
            //fill the given IDictionary<String, Assembly>
        } );
    } );
```

We pass in a `IDictionary<String, Assembly>` where the key is the assembly name (`AssemblyName.Name` property value) so the user has the option to determine if an assembly is already queued for scanning and has the option to remove an assembly from the scan queue.

The other important key difference is what we do at boot time when the WinRT app is “launched” (with quotes because can be a resume or lots of different things):

```c#
protected virtual async Task OnBoot( ILaunchActivatedEventArgs args )
{
    await this.OnBoot( this.serviceProvider, args );

    if ( !String.IsNullOrWhiteSpace( args.Arguments ) )
    {
        /*
         * we have command line args (e.g. custom tile),
         * we delegate to someone who knows what to do, period.
         */
        await this.HandleLaunchArguments( args );
    }
    else if ( args.PreviousExecutionState == ApplicationExecutionState.Terminated )
    {
        /*
         *  the previous state was terminated, lets resume, we expect
         *  that the suspension manager will restore a view 
         */
        var service = this.serviceProvider.GetService<ISuspensionManager>();
        await service.ResumeAsync();
    }
    else if ( args.PreviousExecutionState != ApplicationExecutionState.Running )
    {
        /*
         * none of the previous interesting state 
         * and we were not running, start from scratch...lets go home.
         */
        this.NavigateHome( this.homeViewType, args );
    }

    await this.OnBootCompleted( this.serviceProvider );

    /*
     * since in all the previous cases a view has been created
     * we simply activate it in any case.
     */
    Window.Current.Activate();
}
```

As we can see all the details and decisions are completely held by the toolkit, leaving us the possibility to concentrate on the core business of the application.

Following the same philosophy the application bootstrapper exposes a really trivial way to handle charms:

```c#
this.bootstrapper = new PuzzleApplicationBootstrapper<Presentation.MainView>();
this.bootstrapper.SearchRequestHandler += e => 
{
    return Task.Factory.StartNew( () => 
    {
        //handle the search request here
    } );
};
```

We’ll discuss in details how to handle charms request in the related chapter.