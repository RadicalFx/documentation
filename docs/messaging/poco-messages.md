## POCO Messages

The Radical [message broker](message-broker.md) supports also POCO messages, it is not required, anymore, that a class, in order to be a first class message, implements the `IMessage` interface.

>Side note: The `IMessage` interface, and all the `IMessageBroker` operations that depends on it, are now marked as obsolete. It is highly suggested that all the message broker dependent code will be migrated to the new POCO version even if the old one will be fully supported and can safely be used in a mixed environment.

All the features supported by the old version of the message broker are supported even by the new POCO version. The only difference is in the signature of the broadcast, dispatch and subscribe methods.

###Subscribe

the new available signatures are:

```c#
void Subscribe( object subscriber, object sender, Type messageType, Action<object, object> callback );
void Subscribe( object subscriber, object sender, Type messageType, InvocationModel invocationModel, Action<object, object> callback );
void Subscribe( object subscriber, Type messageType, Action<object, object> callback );
void Subscribe( object subscriber, Type messageType, InvocationModel invocationModel, Action<object, object> callback );
void Subscribe<T>( object subscriber, Action<object, T> callback );
void Subscribe<T>( object subscriber, object sender, Action<object, T> callback );
void Subscribe<T>( object subscriber, object sender, InvocationModel invocationModel, Action<object, T> callback );
void Subscribe<T>( object subscriber, InvocationModel invocationModel, Action<object, T> callback );
```

Where the main differences are:

* The generics constraints have been removed;
* The signature of the action callback delegate now has 2 parameters, instead of the single `IMessage` one, where the first object parameter is the sender of the message and the second is the message itself;

###Broadcast & Dispatch

there is only a new simplified signature for the broadcast method:

```c#
void Broadcast( Object sender, Object message );
```

And even for the dispatch method things get simpler:

```c#
void Dispatch( Object sender, Object message );
```