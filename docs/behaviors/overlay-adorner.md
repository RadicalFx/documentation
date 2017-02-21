## Overlay adorner

Radical offers a rich set of adorners and also a generic purpose adorner to put an arbitrary content on top of another element:

```xml
<Calendar Height="180" 
            HorizontalAlignment="Left" 
            VerticalAlignment="Top" 
            Width="180">
    <i:Interaction.Behaviors>
        <behaviors:OverlayBehavior Background="#99FAFAFA" IsVisible="True" IsHitTestVisible="False">
            <behaviors:OverlayBehavior.Content>
                <Border BorderBrush="Red" BorderThickness="4" HorizontalAlignment="Stretch" VerticalAlignment="Stretch">
                    <TextBlock Text="I'm on top of the calendar" HorizontalAlignment="Center" VerticalAlignment="Center" />
                </Border>
            </behaviors:OverlayBehavior.Content>
        </behaviors:OverlayBehavior>
    </i:Interaction.Behaviors>            
</Calendar>
```

producing the following effect at runtime:

![Overlay adorner sample](/images/calendar-overlay-adorner.png)

The behavior is defined in the `http://schemas.topics.it/wpf/radical/windows/behaviors` xml namespace.