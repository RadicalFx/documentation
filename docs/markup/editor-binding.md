## Editor binding

One of the most annoying thing when dealing with input forms in WPF is that in order to activate all the required feature to support the basic scenario a user expects you end with a binding defined as the following:

```xml
<TextBox Text="{Binding Path=MyText, 
                        UpdateSourceTrigger=PropertyChanged, 
                        NotifyOnValidationError=True, 
                        ValidatesOnDataErrors=True, 
                        ValidatesOnExceptions=True}" />
```

That leads to a waste of time and to a really hard-to-manage xaml markup. In order to eliminate both problems in Radical we introduced our own binding markup extension that has as default values all the values exposed in the previous sample. Using our EditorBinding markup extension the same behavior can be achieved in the following manner:

```xml
<TextBox Text="{markup:EditorBinding Path=MyText}" />
```

simpler and cleaner. The markup extension is defined in the `http://schemas.topics.it/wpf/radical/windows/markup` xml namespace.