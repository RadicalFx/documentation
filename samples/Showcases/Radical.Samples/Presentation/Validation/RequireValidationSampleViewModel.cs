using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Radical.ComponentModel;
using Topics.Radical.ComponentModel.Validation;
using Topics.Radical.Validation;
using Topics.Radical.Windows.Presentation.ComponentModel;
using Topics.Radical.Windows.Presentation.Services.Validation;

namespace Topics.Radical.Presentation.Validation
{
    [Sample( Title = "DataAnnotation (IRequireValidation)", Category = Categories.Validation )]
	class RequireValidationSampleViewModel :
		AbstractValidationSampleViewModel,
		IRequireValidationCallback<RequireValidationSampleViewModel>,
		IRequireValidation
	{
		public RequireValidationSampleViewModel()
		{
			this.GetPropertyMetadata( () => this.Text )
				.AddCascadeChangeNotifications( () => this.Sample );

			this.SetInitialPropertyValue( () => this.MergeErrors, true )
                .OnChanged( pvc =>
                {
                    var invalid = this.ValidationService.GetInvalidProperties();
                    this.ValidationService.MergeValidationErrors = this.MergeErrors;
                    foreach( var item in invalid )
                    {
                        this.OnPropertyChanged( item );
                        this.OnErrorsChanged( item );
                    }
                } );
		}

		protected override IValidationService GetValidationService()
		{
			var svc = new DataAnnotationValidationService<RequireValidationSampleViewModel>( this )
			{
				MergeValidationErrors = this.MergeErrors
			}.AddRule
			(
				property: () => this.Text,
				error: ctx => "must be equal to 'foo'",
				rule: ctx => ctx.Entity.Text == "foo"
			);

			return svc;
		}

		[DisplayName( "Esempio" )]
		public Int32 Sample
		{
			get { return this.GetPropertyValue( () => this.Sample ); }
			set { this.SetPropertyValue( () => this.Sample, value ); }
		}

		public Boolean MergeErrors
		{
			get { return this.GetPropertyValue( () => this.MergeErrors ); }
			set { this.SetPropertyValue( () => this.MergeErrors, value ); }
		}

		public void OnValidate( Radical.Validation.ValidationContext<RequireValidationSampleViewModel> context )
		{
			var displayname = this.ValidationService.GetPropertyDisplayName( this, o => o.Sample );

			context.Results.AddError( () => this.Sample, displayname, new[] { "This is fully custom, and works even on non-bound properties such as 'Sample'." } );
		}

		public void RunValidation()
		{
			this.Validate( ValidationBehavior.TriggerValidationErrorsOnFailure );
		}
	}
}
