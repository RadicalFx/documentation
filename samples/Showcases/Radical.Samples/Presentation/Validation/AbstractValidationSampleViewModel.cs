using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Radical.ComponentModel;

namespace Topics.Radical.Presentation.Validation
{
    class AbstractValidationSampleViewModel : SampleViewModel
    {
        [Required( AllowEmptyStrings = false )]
        [DisplayName( "Testo" )]
        public String Text
        {
            get { return this.GetPropertyValue( () => this.Text ); }
            set { this.SetPropertyValue( () => this.Text, value ); }
        }

        [Required( AllowEmptyStrings = false )]
        [DisplayName( "Altro Testo" )]
        public String AnotherText
        {
            get { return this.GetPropertyValue( () => this.AnotherText ); }
            set { this.SetPropertyValue( () => this.AnotherText, value ); }
        }
    }
}
