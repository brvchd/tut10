using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tut10.Exceptions
{
    public class DoctorAlreadyExists : Exception
    {
        public DoctorAlreadyExists() : base("Doctor with specified ID already exists") {  }
    }
}
