using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radical.Samples.Presentation.DragAndDrop
{
	class Person
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public override string ToString()
		{
			return FirstName + " " + LastName;
		}
	}
}
