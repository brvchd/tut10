using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tut10.Models
{
    public class PrescriptionDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments{ get; set; }


        public PrescriptionDbContext() 
        {
        
        }

        public PrescriptionDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>(opt =>
            {
                opt.HasKey(p => p.IdDoctor);
                opt.Property(p => p.IdDoctor);


                opt.Property(p => p.FirstName)
                    .HasMaxLength(100)
                    .IsRequired();
                opt.Property(p => p.LastName)
                    .HasMaxLength(100)
                    .IsRequired();
                opt.Property(p => p.Email)
                    .HasMaxLength(100)
                    .IsRequired();
                
            });


            modelBuilder.Entity<Patient>(opt =>
            {
                opt.HasKey(p => p.IdPatient);
                opt.Property(p => p.IdPatient);

                opt.Property(p => p.FirstName)
                    .HasMaxLength(100)
                    .IsRequired();
                opt.Property(p => p.LastName)
                    .HasMaxLength(100)
                    .IsRequired();
                opt.Property(p => p.BirthDate)
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Prescription>(opt =>
            {
                opt.HasKey(p => p.IdPrescription);
                opt.Property(p => p.IdPrescription);

                opt.Property(p => p.Date)
                    .HasColumnType("date")
                    .IsRequired();

                opt.Property(p => p.DueDate)
                    .HasColumnType("date")
                    .IsRequired();

                opt.HasOne(p => p.Doctor)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(p => p.IdDoctor)
                    .IsRequired();

                opt.HasOne(p => p.Patient)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(p => p.IdPatient)
                    .IsRequired();
            });

            modelBuilder.Entity<Medicament>(opt =>
            {
                opt.HasKey(p => p.IdMedicament);
                opt.Property(p => p.IdMedicament);

                opt.Property(p => p.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                opt.Property(p => p.Description)
                    .HasMaxLength(100)
                    .IsRequired();
                opt.Property(p => p.Type)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            modelBuilder.Entity<PrescriptionMedicament>(opt =>
            {
                opt.HasKey(p => new { p.IdMedicament, p.IdPrescription });
                     
                opt.Property(p => p.Details)
                    .HasMaxLength(100)
                    .IsRequired();

                opt.HasOne(p => p.Prescription)
                    .WithMany(p => p.PrescriptionMedicaments)
                    .HasForeignKey(p => p.IdPrescription);

                opt.HasOne(p => p.Medicament)
                    .WithMany(p => p.PrescrptionMedicaments)
                    .HasForeignKey(p => p.IdMedicament);

            });

            SeedData(modelBuilder);
        }

        protected void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasData(
                new Patient() { IdPatient = 1, FirstName = "Alex", LastName = "Miller", BirthDate = Convert.ToDateTime("2001-02-01") },
                new Patient() { IdPatient = 2, FirstName = "Vladyslav", LastName = "Kutsenko", BirthDate = Convert.ToDateTime("2010-13-11") },
                new Patient() { IdPatient = 3, FirstName = "Radyslav", LastName = "Burylko", BirthDate = Convert.ToDateTime("2000-01-01") }
                );

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor() { IdDoctor = 1, FirstName = "Martin", LastName = "Smith", Email = "martin.smith@mail.com" },
                new Doctor() { IdDoctor = 2, FirstName = "John", LastName = "Mercer", Email = "john.mercer@mail.com" },
                new Doctor() { IdDoctor = 3, FirstName = "Anna", LastName = "Walter", Email = "anna.walter@mail.com" }
                );

            modelBuilder.Entity<Medicament>().HasData(
                new Medicament() { IdMedicament = 1, Name = "Demidrol", Description = "desc_1", Type = "type_1" },
                new Medicament() { IdMedicament = 2, Name = "Senade", Description = "desc_2", Type = "type_2" },
                new Medicament() { IdMedicament = 3, Name = "Bisacodil", Description = "desc_3", Type = "type_3" }
                );

            modelBuilder.Entity<Prescription>().HasData(
                new Prescription() { IdPrescription = 1, Date = DateTime.Now, DueDate = Convert.ToDateTime("2022-01-10"), IdDoctor = 1, IdPatient = 1 },
                new Prescription() { IdPrescription = 2, Date = DateTime.Now, DueDate = Convert.ToDateTime("2021-01-01"), IdDoctor = 2, IdPatient = 1 },
                new Prescription() { IdPrescription = 3, Date = DateTime.Now, DueDate = Convert.ToDateTime("2020-05-05"), IdDoctor = 3, IdPatient = 3 }
                );

            modelBuilder.Entity<PrescriptionMedicament>().HasData(
                new PrescriptionMedicament() { IdMedicament = 1, IdPrescription = 2, Dose = 1, Details = "detail_1" },
                new PrescriptionMedicament() { IdMedicament = 2, IdPrescription = 2, Dose = 2, Details = "detail_2" },
                new PrescriptionMedicament() { IdMedicament = 3 , IdPrescription = 3, Dose = null, Details = "detail_3"}
                );
        }
    }
}
