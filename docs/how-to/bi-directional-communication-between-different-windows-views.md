In a MVVM based application if you decide to delegate all the communication to the infrastructure using a broker, so to respect the single responsibility principle, you end up dealing with problems that in a much more coupled environment would never arise.

Imagine the following scenario: As a user when I create e new order I need to choose the customer that owns the order so to successfully associate the order with the given customer.

In “programming” language the above translates to: when the user creates a new order and want to choose a customer we need to **open** a search dialog to let the user search for the customer and then **return** the chosen customer to the calling view.

I have highlighted the keywords in **bold**, we have 2 different contexts that needs to communicate but we do not want to have a direct relation between the 2 contexts, in other words: we do not want to open the dialog from the “create new order view model” but we want to delegate all the communication to the broker.

There are several options to achieve the same behavior but we think that the most important thing to avoid is to try to achieve the same “blocking” behavior that a dialog gives us because this prevents to move to a different UX without changing the implementation.

### 2-way messaging

The first viable approach is to use 2 messages where the first one is to request the start of the selection process and the second one is delivered by the selection process to send back the selection result(s):

```csharp
class SelectCustomer
{
    public object RequestToken{ get; set; }
}
```

```csharp
class CustomerSelected
{
    public object RequestToken{ get; set; }
    public IEnumerable<Customer> Selection{ get; set; }
}
```

We need to introduce the concept of token so that the receiver when receives the response can determine if the response if for its own request or should be discarded because is someone else request:

```csharp
class OrderViewModel
{
    IMessageBroker broker
    object requestToken = null;

    public OrderViewModel( IMessageBroker broker )
    {
        this.broker = broker;
        this.broker.Subscribe<CustomerSelected>( this, ( s, m ) => 
        {
            if( m.RequestToken == this.requestToken )
            {
                //do something with the selection
            }
        } );
    }

    void Select()
    {
        this.requestToken = new object();
        this.broker.Broadcast( this, new SelectCustomer() );
    }
}
```

The benefit of this approach is that someone else can be interested in the selection and we do not have to do anything to plug “that” someone else into the process.

### Message callback

an easier approach is to use one single message:

```csharp
class SelectCustomer
{
    public object RequestToken{ get; set; }
    public Action<IEnumerable<Customer>> Callback{ get; set }
}
```

so that we do not have to deal with tokens to identify responses:

```chsarp
class OrderViewModel
{
    IMessageBroker broker

    public OrderViewModel( IMessageBroker broker )
    {
        this.broker = broker;
    }

    void Select()
    {
        var msg = new SelectCustomer()
        {
            Callback = results => 
            {
                //do something interesting with the results
            }
        };
        this.broker.Broadcast( this,  );
    }
}
```

in this case when the selection process is finished the view model that handles the search/selection simply invokes the callback with the selection results.

### Dedicated services

The last approach is to wrap the above logic into a custom component, such as an ISelectionService<T>, that can be something like:

```csharp
interface ISelectionService<T>
{
    Task<IEnumerable<T>> Search();
    Task<IEnumerable<T>> Search( String query );
}
```

where the fact that we return a Task can be handy because can be used in conjunction with the async/await keywords and perfectly fit the async nature of the message broker broadcasting engine.