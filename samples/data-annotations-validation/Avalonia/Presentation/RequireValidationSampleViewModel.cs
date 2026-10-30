using Radical.Avalonia.Presentation;
using Radical.Avalonia.ComponentModel;
using Radical.Avalonia.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAnnotationsValidationSample.Presentation
{
    class RequireValidationSampleViewModel : AbstractViewModel,
        IRequireValidation
    {
        public RequireValidationSampleViewModel()
        {
            this.GetPropertyMetadata(() => Text)
                .AddCascadeChangeNotifications(() => Sample);

            this.SetInitialPropertyValue(() => MergeErrors, true)
                .OnChanged(pvc =>
               {
                   ValidationService.MergeValidationErrors = MergeErrors;
                   ResetValidation();
                   Validate(ValidationBehavior.TriggerValidationErrorsOnFailure);
               });

            ValidationService = new DataAnnotationValidationService<RequireValidationSampleViewModel>(this)
            {
                MergeValidationErrors = MergeErrors
            }.AddRule
            (
                property: o => o.Text,
                rule: ctx =>
                {
                    return ctx.Entity.Text == "foo"
                        ? ctx.Succeeded()
                        : ctx.Failed("must be equal to 'foo'");
                }
            ).AddRule
            (
                property: o => o.Sample,
                rule: ctx => ctx.Failed("This is fully custom, and works even on non-bound properties such as 'Sample'.")
            );
        }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Testo")]
        public string Text
        {
            get { return this.GetPropertyValue(() => Text); }
            set { this.SetPropertyValue(() => Text, value); }
        }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Altro Testo")]
        public string AnotherText
        {
            get { return this.GetPropertyValue(() => AnotherText); }
            set { this.SetPropertyValue(() => AnotherText, value); }
        }

        [DisplayName("Esempio")]
        public int Sample
        {
            get { return this.GetPropertyValue(() => Sample); }
            set { this.SetPropertyValue(() => Sample, value); }
        }

        public bool MergeErrors
        {
            get { return this.GetPropertyValue(() => MergeErrors); }
            set { this.SetPropertyValue(() => MergeErrors, value); }
        }

        public void RunValidation()
        {
            this.Validate(ValidationBehavior.TriggerValidationErrorsOnFailure);
        }
    }
}
