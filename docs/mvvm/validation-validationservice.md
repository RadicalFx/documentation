## Validation and Validation Services

One of the most common task during the development of a rich client application is the need to handle the validation of the data input by the user running the application. Radical fully supports WPF validation engine and does all what can be done to alleviate the need for the developer to write infrastructure code.

Let’s start from the end of the story, using a view model like the following:

```csharp
class SampleViewModel : AbstractViewModel, ICanBeValidated
{
    public SampleViewModel()
    {

    }

    protected override IValidationService GetValidationService()
    {
        return new DataAnnotationValidationService<SampleViewModel>( this );
    }

    [Required( AllowEmptyStrings = false )]
    public String Text
    {
        get { return this.GetPropertyValue( () => this.Text ); }
        set { this.SetPropertyValue( () => this.Text, value ); }
    }
}
```

NOTE: If your project is based on .NET framework 4.5, or greater, the `IRequireValidation` interface can be used instead of the `ICanBeValidated` enabling support for multiple errors per property.

and a view as:

```xml
<TextBox Text="{markup:EditorBinding Path=Text}" Grid.Row="0" Margin="33,47,220,0" Height="25" VerticalAlignment="Top" />
<ListBox Grid.Row="1" Grid.IsSharedSizeScope="True" ItemsSource="{Binding Path=ValidationErrors}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <Grid>
                <Grid.ColumnDefinitions>
                    <ColumnDefinition SharedSizeGroup="propertyName" Width="Auto" />
                    <ColumnDefinition SharedSizeGroup="errorText" Width="*" />
                </Grid.ColumnDefinitions>

                <TextBlock Text="{Binding Path=Key}" Margin="0,0,5,0" Grid.Column="0" Foreground="Red" />
                <TextBlock Text="{Binding}" Grid.Column="1" Foreground="Brown" />

            </Grid>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
```

we immediately get full validation support, even with error summary:

![Validation error and error summary](/images/cab-be-validated-1.png)

## WPF validation support

Obviously we are not reinventing the wheel, we are simply leveraging the power of the built-in validation support that WPF already has using the `IDataErrorInfo` or the `INotifyDataErrorInfo` interfaces; for a detailed explanation of the WPF validation capabilities take a look a the following MSDN Magazine detailed article: http://msdn.microsoft.com/en-us/magazine/ff714593.aspx

## How it works

### ICanBeValidated

The first important piece is the `ICanBeValidated` interface that inherits from the `IDataErrorInfo` interface, the `ICanBeValidated` is defined as follows:

```csharp
public interface ICanBeValidated : IDataErrorInfo
{
    Boolean IsValid { get; }

    ObservableCollection<ValidationError> ValidationErrors { get; }

    Boolean Validate();

    Boolean Validate( ValidationBehavior behavior );

    Boolean Validate( String ruleSet, ValidationBehavior behavior );

    event EventHandler Validated;

    void TriggerValidation();
}
```

All the interface methods and properties are already implemented by the base `AbstractViewModel`, the user is only required to inherit from the interface so to tell to the WPF infrastructure that the `DataContext` of the `View` is a class the implements `IDataErrorInfo`. Going deeper the `ICanBeValidated` interface exposes the following features:

* **IsValid**: determines if the current view model validation failed or is valid;
* **ValidationErrors**: Gives access to a list of validation errors occurred during the validation process;
* **Validate()**: the validate method, and its overloads, allows to manually trigger the validation process, by default the validation process is automatically triggered by WPF for each property set during a data binding operation;
  * **Validate( ValidationBehavior behavior )**;
  * **Validate( String ruleSet, ValidationBehavior behavior )**;
* **Validated**: the validated event is raised each time the validation process is completed;
* **TriggerValidation**: the TriggerValidation method allows to programmatically “ask” to WPF to trigger the error status even on properties, valid or invalid, that has never been involved in a binding write operation;
  The typical scenario is a form with a submit button, if the user never fills the form but simply press the submit button we want to show, visually show, invalid properties/fields to the user, the `TriggerValidation` method allows us to achieve this.

### IRequireValidation

As said before if you are using .NET Framework 4.5, or greater, you can use the new `IRequireValidation` interface that inherits from the `INotifyDataErrorInfo` interface, the `IRequireValidation` is defined as follows:

```csharp
public interface IRequireValidation : INotifyDataErrorInfo
{
	Boolean IsValid { get; }

	ObservableCollection<ValidationError> ValidationErrors { get; }

	Boolean Validate();

	Boolean Validate( ValidationBehavior behavior );

	Boolean Validate( String ruleSet, ValidationBehavior behavior );

	event EventHandler Validated;

	void TriggerValidation();

	void ResetValidation();
}
```

All the interface methods and properties are already implemented by the base `AbstractViewModel`, the user is only required to inherit from the interface so to tell to the WPF infrastructure that the `DataContext` of the `View` is a class the implements `INotifyDataErrorInfo`. Going deeper the `IRequireValidation` interface exposes the following features:

* **IsValid**: determines if the current view model validation failed or is valid;
* **ValidationErrors**: Gives access to a list of validation errors occurred during the validation process;
* **Validate()**: the validate method, and its overloads, allows to manually trigger the validation process, by default the validation process is automatically triggered by WPF for each property set during a data binding operation;
  * **Validate( ValidationBehavior behavior )**;
  * **Validate( String ruleSet, ValidationBehavior behavior )**;
* **Validated**: the validated event is raised each time the validation process is completed;
* **TriggerValidation**: the `TriggerValidation` method allows to programmatically “ask” to WPF to trigger the error status even on properties, valid or invalid, that has never been involved in a binding write operation;
  The typical scenario is a form with a submit button, if the user never fills the form but simply press the submit button we want to show, visually show, invalid properties/fields to the user, the `TriggerValidation` method allows us to achieve this.
* **ResetValidation**: Resets the staus of the validation infrastructure to its default value.

### Validation Services

The other step that must be accomplished by the user is to define the engine used to run the validation process, in order to achieve that is enough to override the protected method `GetValidationService()` that is called, by the infrastructure, once and only once the first time the validation process gets executed.

In the above sample we are using the most powerful validation service provided built-in in Radical, we are using the `DataAnnotationValidationService<TViewModel>` that, as the name implies, works against the [Data Annotation services](http://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.aspx), and add support for custom inline validation rules.

### Editor bindings

As we have seen WPF requires that the view model inherits from the `IDataErrorInfo` interface, or the new `INotifyDataErrorInfo`, in order to run the validation process, the second requirement is that each binding is configured to enable validation, since there are several properties to set to true this operation tends to be tedious and prone to errors; in order drastically simplify the validation setup [Radical](https://github.com/RadicalFx/radical) offers its [[own binding extension|Editor Binding]] with everything setup as expected:

```xml
<TextBox Text="{markup:EditorBinding Path=Text}" />
```

The `EditorBinding` is a standard binding with everything already setup for validation, the `EditorBinding` markup extension can be found in the `http://schemas.topics.it/wpf/radical/windows/markup` xml namespace.

### First time property validation

Another issue of the built-in WPF validation that the Radical validation system solves is the first time validation that WPF runs when a binding operation is performed for the first time, at the property get.

In a scenario where we have a form bound to a view model we do not want to display the form the first time as already invalid, since the user cannot understand why the form is invalid since there has not been any interaction.

In order to solve this scenario the Radical validation system discard the first validation request for each bound property, in order to change this behavior it is enough to override the protected method `ValidationCalledOnceFor( String propertyName )` that the infrastructure calls in order to understand if the given property has been validated at least once.

## Custom validation

In order to build your own validation logic is not necessary to create a custom validation service, even if it possible, because we have already added support for custom validation rules and custom advanced validation in the built-in `DataAnnotationValidationService`.

###Custom rules

There are scenarios in which validation attributes are not enough and we do not want to build a new validation attribute from scratch maybe because we already know that it will be used only in that specific scenario, in this case the best approach is to add a custom validation rule on the fly:

```csharp
protected override IValidationService GetValidationService()
{
    return new DataAnnotationValidationService<SampleViewModel>( this )
        .AddRule
        (
            property: () => this.Text,
            error: ctx => "must be equal to 'foo'",
            rule: ctx => ctx.Entity.Text == "foo"
        );
}
```

We can add as much rule as we want for each property, the context (ctx parameter) passed to the rule evaluation lambda and the error generator lambda has the following shape:

```csharp
public class ValidationContext<TViewModel>
{
    public TViewModel Entity { get; private set; }

    public String RuleSet { get; set; }

    public String PropertyName { get; set; }

    public IValidator<TViewModel> Validator { get; private set; }

    public ValidationResults Results { get; private set; }
}
```

and we can use it to access the whole entity to do a broader validation not specifically scoped to the property we are validating.

###Advanced validation

If none of the above options fit our needs we can integrate into the validation process a fully custom validation piece of code just implementing, in our view model, the `IRequireValidationCallback<TViewModel>` interface:

```csharp
class ValidationSampleViewModel : AbstractViewModel,
        ICanBeValidated,
        IRequireValidationCallback<ValidationSampleViewModel>
{
    public Int32 Sample
    {
        get;
        set;
    }

    public void OnValidate( ValidationContext<ValidationSampleViewModel> context )
    {
        context.Results.AddError( () => this.Sample, "This is fully custom." );
    }
}
```

Each time the validation process run, if the validated view model implements the `IRequireValidationCallback<TViewModel>`, the `OnValidate` method is called allowing us to perform a fully custom validation process.