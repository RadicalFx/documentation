using MyApp.Contracts.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Services
{
    class SampleSharedService: ISampleSharedService
    {
        public String MyProperty { get; set; }
    }
}
