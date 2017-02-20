## Property Metadata for the Change Tracking Service

A `MementoEntity`, being a Radical `Entity`, will benefit of the [Property System](/entities/property-system.md) and of the properties metadata the the property system adds. A `MementoEntity` enriches the property system basic metadata adding some behaviors directly related to the memento services.

`DisableChangesTracking()` and `EnableChangesTracking()` change tracking is enabled by default on all properties, Radical properties, of a tracked entity, it is possible to control on which properties disable or enable the tracking system via the property metadata or decorating the property with the `MementoPropertyMetadataAttribute` and setting the `TrackChanges` value.