using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tut10.DTOs.Requests;
using tut10.DTOs.Response;
using tut10.Models;

namespace tut10.Services
{
    public interface IClinicDbService
    {
        public List<Doctor> GetDoctors();
        public AddDoctorResponse AddDoctor(AddDoctorRequest request);
        public ModifyDoctorResponse ModifyDoctor(ModifyDoctorRequest request);
        public void DeleteDoctor(string id);
    }
}
