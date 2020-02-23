## Observers

In rich client applications user interaction is generally performed via many user interface elements, such as menus, buttons, hyperlinks and mouse clicks/double clicks on UI elements, most of the time multiple different interactions lead to the same action to be performed. In cases like these the way to go in WPF is to use the `ICommand` interface to build commands, for example using the [DelegateCommand](/mvvm/delegate-command.md).

There are scenarios, although, where the problem we need to face cannot be solved using commands, or using only commands, maybe we need to observe multiple conditions and react each time one of them changes. In this case generally we fall into 2 traps:

* We start polling for changes instead of waiting to be notified that something is changed;
* We distribute the polling code near what we want to observe instead of near to where we want to react to the change;

In both cases we are complicating things each time we need to update our logic that is spread all over the places.

Radical observers are there to fix the issue, they provide the same approach provided by the .NET Reactive Extensions in a simpler way. Observers are not better than reactive extensions, they are simply meant to provide an easy, and well integrated, solution to a well known issue.

Observers are classes the implements the `IMonitor` interface that basically adds a `Changed` event to the observer allowing others to hook the event and be notified when something changes.
