Radical provides an [overlay adorner](../behaviors/overlay-adorner.md) to handle busy/long running operations, that for simple scenarios just works as expected. Sometimes there are cases when we need to handle a much more complex scenario such as the following:

As a user I want to be able to start a long running operation, and if, after a certain amount of time, the operation is not completed I want to be able to cancel the operation itself.

Let us start drawing the UI for the above requirements:

```xml
<AdornerDecorator>
    <Grid behaviors:BusyStatusManager.Status="{Binding Path=IsBusy, Converter={converters:BooleanBusyStatusConverter}}">
        <behaviors:BusyStatusManager.Content>
            <Grid>
                <Grid.RowDefinitions>
                    <RowDefinition Height="*" />
                    <RowDefinition Height="*" />
                </Grid.RowDefinitions>
                <Ellipse x:Name="ellipse" StrokeThickness="6" Width="30" Height="30" RenderTransformOrigin="0.5,0.5">
                    <Ellipse.Resources>
                        <Storyboard x:Key="SpinAnimation" RepeatBehavior="Forever">
                            <DoubleAnimation To="359"
                                        Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)" />
                        </Storyboard>
                    </Ellipse.Resources>
                    <Ellipse.Triggers>
                        <EventTrigger RoutedEvent="FrameworkElement.Loaded">
                            <BeginStoryboard Storyboard="{StaticResource SpinAnimation}"/>
                        </EventTrigger>
                    </Ellipse.Triggers>
                    <Ellipse.RenderTransform>
                        <TransformGroup>
                            <ScaleTransform/>
                            <SkewTransform/>
                            <RotateTransform/>
                            <TranslateTransform/>
                        </TransformGroup>
                    </Ellipse.RenderTransform>
                    <Ellipse.Stroke>
                        <LinearGradientBrush EndPoint="0.5,1" StartPoint="0.5,0">
                            <GradientStop Color="Red" Offset="0"/>
                            <GradientStop Color="#FF1442BF" Offset="1"/>
                        </LinearGradientBrush>
                    </Ellipse.Stroke>
                </Ellipse>
                <Button IsEnabled="{Binding Path=ThresholdElapsed}" HorizontalAlignment="Center" VerticalAlignment="Top" Grid.Row="1" Content="Waited tooooo long, cancel..." Command="{markup:AutoCommandBinding Path=CancelWork}" />
            </Grid>
        </behaviors:BusyStatusManager.Content>
        <Button Content="Click me!" Command="{markup:AutoCommandBinding Path=WorkAsync}" HorizontalAlignment="Center" VerticalAlignment="Center" Width="70" Height="23"/>
    </Grid>
</AdornerDecorator>
```

Seems complex but it is not, we are using an ellipse element to create an animated spinning icon, pretty standard WPF stuff, the ellipse element, and a button is enclosed in a grid that is enclosed in the content of the BusyStatusManager, the button enabled status is bound to a property in the ViewModel. And then we have a simple button that triggers the long running operation in the ViewModel.

Q: Why the outer grid is wrapped in an AdornerDecorator element?

This a pretty complex WPF adorner requirement, the above code is contained in the Radical samples that are structured in the following manner:

* The top most element is a Window;
* Inside the Window there is a grid:
   * the left column contains the sample navigation menu;
   * the right column is a region (a Radical UI Composition region) that hosts the selected sample;
* Attached to the Window there is a MainViewModel;
* The MainViewModel exposes a property, SelectedSample, that is the currently selected sample view model that will be bound to the region content;

So by default the WPF visual tree will be something similar to:

* Window <–> MainViewModel
  * AdornerDecorator (automatically added by WPF)
    * Grid
      * region container
        * Sample UserControl <–> SampleViewModel

At runtime the Radical adorner decorator engine looks for an AdornerDecorator to attach the adorner to, and finds the one child of the Window, now if you look at the ViewModel positions you can immediately notice that the data binding engine will look for properties on the wrong ViewModel, in this case, so we need to constraint the position of the adding another AdornerDecorator:

* Window <–> MainViewModel
  * AdornerDecorator (automatically added by WPF)
    * Grid
      * region container
        * **AdornerDecorator**
          * Sample UserControl <–> SampleViewModel

From the view model point of view things are not so complicated as they appear in at the first look:

```csharp
class BusyBehaviorSampleViewModel : SampleViewModel
{
    readonly IDispatcher dispatcher;

    public BusyBehaviorSampleViewModel( IDispatcher dispatcher )
    {
        this.dispatcher = dispatcher;
    }

    public Boolean ThresholdElapsed
    {
        get { return this.GetPropertyValue( () => this.ThresholdElapsed ); }
        private set { this.SetPropertyValue( () => this.ThresholdElapsed, value ); }
    }

    public Boolean IsBusy
    {
        get { return this.GetPropertyValue( () => this.IsBusy ); }
        private set { this.SetPropertyValue( () => this.IsBusy, value ); }
    }

    public String Status
    {
        get { return this.GetPropertyValue( () => this.Status ); }
        private set { this.SetPropertyValue( () => this.Status, value ); }
    }

    Worker w = null;

    public void CancelWork()
    {
        if ( this.w != null )
        {
            lock ( this )
            {
                if ( this.w != null )
                {
                    this.w.CancelWork();
                }
            }
        }
    }

    public async void WorkAsync()
    {
        this.IsBusy = true;
        this.Status = "running...";

        this.w = new Worker()
        {
            OnThresholdElapsed = () => this.dispatcher.Dispatch( () => this.ThresholdElapsed = true )
        };

        var r = await this.w.Execute( token =>
        {
            var count = 0;
            while ( count < 15 && !token.IsCancellationRequested )
            {
                ++count;
                Thread.Sleep( 1000 );
            }
        } );

        lock ( this )
        {
            this.w = null;
        }

        this.Status = r.Cancelled
            ? "cancelled."
            : "completed.";

        this.IsBusy = false;
    }
}
```

We have a bunch of properties to control the status of the UI and 2 methods to control the async work, the Worker class is just a wrapper around the Task API to simplify the threshold elapsed management that is done using a Timer.

What happens, when the user pushes the “button”, is that:

* A long running job is started (15”);
* The “please wait…” UI adorner is displayed;
* After 5” the “Cancel” button is activated because the threshold to wait for the task to complete is exhausted;
* The long running job continues;
* if the user presses the “cancel” button a cancel request is injected into the worker;
* otherwise the long running task is allowed to complete;

The worker class:

```csharp
class Worker
{
    public class Result
    {
        public Boolean Cancelled { get; set; }
    }

    CancellationTokenSource cs = null;

    public Worker()
    {
        this.OnThresholdElapsed = () => { };
    }

    public Action OnThresholdElapsed { get; set; }

    public async Task<Result> Execute( Action<CancellationToken> action )
    {
        this.cs = new CancellationTokenSource();
        var token = this.cs.Token;

        var r = await Task.Factory.StartNew( () =>
        {
            var threshold = new System.Timers.Timer( 5000 );
            threshold.AutoReset = false;
            threshold.Elapsed += ( s, e ) => this.OnThresholdElapsed();
            threshold.Start();

            action( token );

            threshold.Stop();

            return new Result() { Cancelled = token.IsCancellationRequested };
        }, cs.Token );

        lock ( this )
        {
            this.cs = null;
        }

        return r;
    }

    public void CancelWork()
    {
        if ( this.cs != null )
        {
            lock ( this )
            {
                if ( this.cs != null )
                {
                    cs.Cancel();
                }
            }
        }
    }
}
```

The above worker is for sample purpose and is not intended to be used in production.
