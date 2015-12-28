It is possible to configure a `ViewModel` to notify, via a broker message, that the state of the associated `View` has changed. The `ViewModel` class can be decorated with one, or more, of the following attributes, depending of the notifications we need:

* `NotifyLoadedAttribute`
* `NotifyShownAttribute`
* `NotifyActivatedAttribute`
* `NotifyClosedAttribute`

All the notifications will be broadcasted asynchronously using the `MessageBroker`, such as in following sample:

```csharp
[NotifyLoaded, NotifyClosed]
class MySampleViewModel : AbstractViewModel
{
}
```