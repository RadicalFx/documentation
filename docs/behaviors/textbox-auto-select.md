## TextBox Auto select

One of the default annoying behavior of the WPF TextBox is that when it gets focused if there is some text it is not automatically selected, we can use the TextBoxManager AutoSelectText attached property:

```xml
<TextBox Text="bla bla..." behaviors:TextBoxManager.AutoSelectText="True" />
```

the attached property is defined in the `http://schemas.topics.it/wpf/radical/windows/behaviors` xml namespace.