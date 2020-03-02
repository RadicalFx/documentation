## Busy status manager

One of the built-in [overlay adorners](overlay-adorner.md) that Radical offers is the `BusyStatusManager` that allows us to put some blocking content on top of another xaml element and control its visibility via the IsBusy attached property:

```xml
<Grid Grid.Row="1" Grid.Column="0" 
        behaviors:BusyStatusManager.Content="Searching..."
        behaviors:BusyStatusManager.Status="{Binding Path=IsBusy, Converter={converters:BooleanBusyStatusConverter}}">
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition Height="*" />
    </Grid.RowDefinitions>

    <TextBox Text="{markup:EditorBinding Path=Query}" 
            Grid.Row="0"
            Margin="0"
            Height="23"
            VerticalAlignment="Top"
            HorizontalAlignment="Stretch"
            behaviors:CueBannerService.CueBanner="Search..."
            behaviors:TextBoxManager.Command="{markup:AutoCommandBinding Path=Search}">
    </TextBox>
    <ListView Grid.Row="1" ItemsSource="{Binding Path=Persons}" SelectedItem="{Binding Path=SelectedPerson}">
        <ListView.ItemTemplate>
            <DataTemplate>
                <StackPanel Orientation="Horizontal">
                    <TextBlock Text="{Binding Path=FirstName}" />
                    <TextBlock Margin="5,0,0,0" Text="{Binding Path=LastName}" />
                </StackPanel>
            </DataTemplate>
        </ListView.ItemTemplate>
    </ListView>
</Grid>
```

In the previous example we are using the BusyStatusManager to put the “searching…” banner on top of a UI element whose role is to provide search capabilities, whenever from the view model we change the value of the IsBusy property to true the content of the Content property is displayed, the underlying element IsEnabled property is set to false and a semi-transparent grey background is drawn.

The important thing is that the Content property type is System.Object, this, inline with the WPF default behavior, allows us to put any content as the busy content of the BusyStatusManager.

The attached property is defined in the `http://schemas.radicalframework.com/windows/behaviors` xml namespace, and the converter is defined in the `http://schemas.radicalframework.com/windows/converters` xml namespace.