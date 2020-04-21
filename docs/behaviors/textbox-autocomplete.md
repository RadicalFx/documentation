## TextBox Autocomplete

Radical from version 2.0 no longer supports autocomplete behavior.
However, an example is provided to support migration from the previous behavior to a third-party component that replaces for the most part the previously existing one.

The package for third-party component can be downloaded from nuget with the following command:

`Install-Package AutoCompleteTextBox`

The version used in the example is 1.1.0

An example of use is:

```xml
<editors1:AutoCompleteTextBox HorizontalAlignment="Left"
                              Delay="1000"
                              DisplayMember="FullName"
                              Provider="{StaticResource ResourceKey=PersonSuggestionProvider}">
    <editors1:AutoCompleteTextBox.SelectedItem>
        <Binding Path="Choosen" Mode="TwoWay" UpdateSourceTrigger="PropertyChanged" ValidatesOnDataErrors="True">
        </Binding>
    </editors1:AutoCompleteTextBox.SelectedItem>
    <editors1:AutoCompleteTextBox.LoadingContent>
        <TextBlock Text="Loading..." Margin="5" FontSize="14" />
    </editors1:AutoCompleteTextBox.LoadingContent>
    <editors1:AutoCompleteTextBox.ItemTemplate>
        <DataTemplate>
            <StackPanel>
                <TextBlock Text="{Binding Path=FirstName}" />
                <TextBlock Text="{Binding Path=LastName}" />
            </StackPanel>
        </DataTemplate>
    </editors1:AutoCompleteTextBox.ItemTemplate>
</editors1:AutoCompleteTextBox>
```
The details of the main component properties are:

* `$Delay`: the delay time in milliseconds before the component makes suggestions.;
* `$DisplayMember`: the name of the property you want to view after selecting from the suggestions;
* `$Provider`: the component responsible for loading suggestions;
* `$LoadingContent`: the template for the visual component to be shown during long waits;

Additional configurable properties are present in the proposed example.

For a complete list please refer to the official component repository:

`https://github.com/quicoli/WPF-AutoComplete-TextBox`