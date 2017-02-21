## Callback expectations

A view model that needs to intercept state changes can implement a interface that declares which are the required callback(s), the supported interfaces are:

* IExpectViewLoadedCallback;
* IExpectViewActivatedCallback;
* IExpectViewShownCallback;
* IExpectViewClosingCallback;
* IExpectViewClosedCallback;

All those interfaces are pretty trivial and does not require any further explanation other then the following sample:

```csharp
class SampleViewModel : IExpectViewLoadedCallback
{
    void IExpectViewLoadedCallback.OnViewLoaded()
    {
        //code to handle the View Loaded event
    }
}
```

The only “special” one is the `IExpectViewClosingCallback` that allows the `ViewModel` to ask to the `View` to stop the closing process:

```csharp
class ChildViewModel : AbstractViewModel, IExpectViewClosingCallback
{
    void IExpectViewClosingCallback.OnViewClosing( CancelEventArgs e )
    {
        //blocks the view closing process
        e.Cancel = true;
    }
}
```

Those interfaces are designed to let the `ViewModel` intercept the state changes of **its own** `View` not of other view, the default way to intercept state changes of other view is to use the `MessageBroker`.