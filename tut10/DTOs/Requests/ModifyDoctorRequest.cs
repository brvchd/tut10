using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tut10.DTOs.Requests
{
    public class ModifyDoctorRequest
    {
        [Required]
        public int IdDoctor { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
    }
}
