## Customer Improvement Program

From the perspective of an application producer it is really important to know what our end users do with the application they use:

* we plan a feature;
* we invest money in building a feature;
* we deploy a feature;

we do not know nothing about how the user utilizes the feature we invested on, we do not even know if the user utilizes it at all.

```csharp
AnalyticsServices.UserActionTrackingHandler = evt =>
{
    //every user action will be dispatched here asynchronously    
};

AnalyticsServices.IsEnabled = true;
```

Writing the above code at the application startup enables the Radical AnalyticsServices, what happens is that all the code that in some way invokes a DelegateCommand, in a WPF MVVM based application, will be tracked and we have the opportunity to “save” what the user is doing in order to analyze it later.

What the UserActionTrackingHandler receives is an AnalyticsEvent with the following shape:

```csharp
public class AnalyticsEvent
{
    public AnalyticsEvent()
    {
        this.ExecutedOn = DateTimeOffset.Now;
        this.Identity = Thread.CurrentPrincipal.Identity;
    }

    public DateTimeOffset ExecutedOn { get; set; }

    public String Name { get; set; }

    public Object Data { get; set; }

    public IIdentity Identity { get; set; }
}
```

If we need we can define our own events inheriting from the AnalyticsEvent class and in order to plugin our events we only need to declare a dependency on the IAnalyticsServices service:

```csharp
public interface IAnalyticsServices
{
    Boolean IsEnabled { get; set; }
    void TrackUserActionAsync( Analytics.AnalyticsEvent action );
}
```

And each time we call `TrackUserActionAsync` the `UserActionTrackingHandler` will be invoked.