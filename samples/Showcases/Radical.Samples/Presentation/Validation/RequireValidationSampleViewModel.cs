﻿using Radical.ComponentModel.Validation;
using Radical.Samples.ComponentModel;
using Radical.Windows.ComponentModel;
using Radical.Windows.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Radical.Samples.Presentation.Validation
{
    [Sample(Title = "DataAnnotation (IRequireValidation)", Category = Categories.Validation)]
    class RequireValidationSampleViewModel :
        SampleViewModel,
        IRequireValidationCallback<RequireValidationSampleViewModel>,
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

        public void OnValidate(Radical.Validation.ValidationContext<RequireValidationSampleViewModel> context)
        {
            if (context.PropertyName == null)
            {
                var displayname = GetPropertyDisplayName(nameof(Sample));

                context.Results.AddError(() => Sample, displayname, new[] { "This is fully custom, and works even on non-bound properties such as 'Sample'." });
            }
        }

        public void RunValidation()
        {
            this.Validate(ValidationBehavior.TriggerValidationErrorsOnFailure);
        }
    }
}
