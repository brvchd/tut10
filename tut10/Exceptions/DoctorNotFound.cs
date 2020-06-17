using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tut10.Exceptions
{
    public class DoctorNotFound : Exception
    {
        public DoctorNotFound() : base("Doctor with specified ID was not found") { }
    }
}
