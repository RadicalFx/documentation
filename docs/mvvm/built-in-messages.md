## Built in messages

Radical Presentation relies on the [`MessageBroker`](/messaging/message-broker.md) to broadcast messages that can be used by the application to easily manage a lot of stuff that otherwise can be a bit cumbersome.

The following is the list of the Radical Presentation built-in messages and their meaning/usage.

### Application Messages

These messages are broadcasted or dispatched by the application to notify application level state changes.

* `ApplicationBootCompleted`:

  * **broadcasted** *asynchronously* by application bootstrapper to notify that the boot process is completed.

* `ApplicationShutdownRequest`:

  * can be dispatched or broadcasted by anyone to request programmatically the application to shutdown. it is highly recommended that the message is broadcasted asynchronously_._ When the application shutdown is requested via the `ApplicationShutdownRequest` message, the following events might be dispatched:

     * `ApplicationShutdownRequested`:

        * **dispatched synchronously** by application bootstrapper to notify that the application has started the shutdown process, this event is dispatched synchronously to allow subscribers to easily cancel the shutdown process using a well known approach similar to the one exposed by the .net `CancelEventArgs`.

      * `ApplicationShutdownCanceled`:

        * **broadcasted** *asynchronously* by application bootstrapper to notify that the shutdown process has been canceled.

* `ApplicationShutdown`:

  * **broadcasted** *asynchronously* by application bootstrapper to notify that the shutdown process is in progress, from this point on the process is not cancellable any more.

*Note*:

All the “application shutdown” related events/messages brings with them an enumeration (`ApplicationShutdownReason`) that identifies why the application is shutting down.

### View/ViewModel Messages

The following messages are broadcasted or dispatched by the infrastructure when the state of a view changes or to request a change to the view status.

* `CloseViewRequest`:

  * can be dispatched or broadcasted by anyone to request programmatically to a view to close. it is highly recommended that the message is broadcasted *asynchronously*.
    The message is generally used to close the view of the view model that issues the message, but the shape of the message allows to close a view attached to any view model.

  ```csharp
  class SampleViewModel
  {
  	readonly IMessageBroker broker;

  	public SampleViewModel( IMessageBroker broker )
  	{
  		this.broker = broker;
  	}

  	public void Sample() 
  	{
  		this.broker.Broadcast( new CloseViewRequest( this ) );
  	}
  }
  ```

* `ViewModelClosed`:

  * **broadcasted** asynchronously by the infrastructure to notify that a view and an associated ViewModel has been closed.

* `ViewModelClosing`:

  * **dispatched synchronously** by the infrastructure to notify that the a view and an associated ViewModel is closing, this event is dispatched synchronously to allow subscribers to easily cancel the close process using a well known approach similar to the one exposed by the .net `CancelEventArgs`.

* `ViewLoaded`:

  * **broadcasted** *asynchronously* by the infrastructure to notify that a view has been loaded.

* `ViewModelLoaded`:

  * **broadcasted** *asynchronously* by the infrastructure to notify that a ViewModel has been loaded.

*Note*:

`ViewLoaded` and `ViewModelLoaded` messages are broadcasted only under certain circumstances, depending on the result of the `ShouldNotifyViewLoaded` and `ShouldNotifyViewModelLoaded` [conventions](runtime-conventions.md).

* `ViewModelShown`:

  * **broadcasted** *asynchronously* by the infrastructure to notify that a view and an associated ViewModel has been shown for the first time.
