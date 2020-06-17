using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tut10.DTOs.Requests;
using tut10.DTOs.Response;
using tut10.Exceptions;
using tut10.Models;

namespace tut10.Services
{
    public class ClinicDbService : IClinicDbService
    {
        private readonly PrescriptionDbContext _context;
        public ClinicDbService(PrescriptionDbContext context)
        {
            _context = context;
        }

        public AddDoctorResponse AddDoctor(AddDoctorRequest request)
        {
            var exists = _context.Doctors.Any(d => d.IdDoctor == request.IdDoctor);
            if (exists) throw new DoctorAlreadyExists();
            var newId = GenerateDoctorId();

            _context.Doctors.Add(
                new Doctor() { IdDoctor = newId, FirstName = request.FirstName, LastName = request.LastName, Email = request.Email}
                );
            _context.SaveChanges();

            return new AddDoctorResponse() { FirstName = request.FirstName, LastName = request.LastName };
        }

        public void DeleteDoctor(string id)
        {
            var doctor = _context.Doctors.FirstOrDefault(d => d.IdDoctor == Convert.ToInt32(id));
            if (doctor == null) throw new DoctorNotFound();

            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
        }

        public List<Doctor> GetDoctors()
        {
            return _context.Doctors.ToList();
        }

        public ModifyDoctorResponse ModifyDoctor(ModifyDoctorRequest request)
        {
            var doctor = _context.Doctors.FirstOrDefault(d => d.IdDoctor == request.IdDoctor);
            if (doctor == null) throw new DoctorNotFound();

            if(!string.IsNullOrEmpty(request.FirstName)) doctor.FirstName = request.FirstName;
            if(!string.IsNullOrEmpty(request.LastName)) doctor.LastName = request.LastName;         
            if(!string.IsNullOrEmpty(request.Email)) doctor.Email = request.Email;
            _context.SaveChanges();

            return new ModifyDoctorResponse() { FirstName = doctor.FirstName, LastName = doctor.LastName, Email = doctor.Email };

        }

        private int GenerateDoctorId()
        {
            return _context.Doctors.Max(d => d.IdDoctor) + 1;
        }
    }
}
