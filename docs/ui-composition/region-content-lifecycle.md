## Region content lifecycle

A `region`, as every other contet in Radical, has a lifecycle. Depending on the type of the region the lifecycle can be different, but the general approach is the following:

* View and ViewModel will be released;
* If View or ViewModel implements `IDisposable` they will be disposed;
* If View or ViewModel implements `IExpectViewClosedCallback` they will receive a callback notification;

Every region can be `Shutdown`, not explicitely, but by shutting down the `RegionManager` that manages the region. A `RegionManager` shutdown can occour, for example, at application shutdown or when the hosting `Window` is closed. At shutdown time every region managed by the shutdown `RegionManager` will be notified and the region content, in our case the `ViewModel`, has the opportunity to intercept and react to this process.

When ever is region content is removed the entire logical tree of the removed content is inspected to ensure that is it contains any other regions their lifecycle is managed as expected sutting down all the nested regions.

### IContentRegion

An `IContentRegion` notifies its own content `ViewModel`, if any, right before removing the content and once it has been removed. The content `ViewModel` has the opportunity, via the `IExpectViewClosingCallback`, to cancel the removal process and to be notified once the removal is comleted via the `IExpectViewClosedCallback`.

### IElementsRegion

An `IElementsRegion` can host multiple contents at a time, it is designed to notify the `ViewModel`, if any, of the content that will be removed right before removing it and once it has been removed. The content `ViewModel` has the opportunity, via the `IExpectViewClosingCallback`, to cancel the removal process and to be notified once the removal is completed via the `IExpectViewClosedCallback`.

### ISwitchingElementsRegion

An `ISwitchingElementsRegion` can host multiple contents at a time as the `IElementsRegion` and add the concept of an active content that can change over time. It is designed to notify the `ViewModel`, if any, of the content that will be removed right before removing it and once it has been removed. The content `ViewModel` has the opportunity, via the `IExpectViewClosingCallback`, to cancel the removal process and to be notified once the removal is completed via the `IExpectViewClosedCallback`.
Other than behaving as a `IElementsRegion` the `ISwitchingElementsRegion` notifies each content `ViewModel` whenever is activated if it implements the `IExpectViewActivatedCallback`.