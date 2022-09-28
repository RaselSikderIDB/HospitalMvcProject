using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HospitalMvcProject.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }
        public virtual DbSet<TblUserRole> TblUserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.DoctorName)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.PhoneNo)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.Patients)
                .WithOptional(e => e.Doctor)
                .HasForeignKey(e => e.ReferDoctor);

            modelBuilder.Entity<Patient>()
                .Property(e => e.PatientName)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.Age)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.Picture)
                .IsUnicode(false);

            modelBuilder.Entity<TblUser>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<TblUser>()
                .Property(e => e.UserPass)
                .IsUnicode(false);

            modelBuilder.Entity<TblUser>()
                .Property(e => e.UserType)
                .IsUnicode(false);

            modelBuilder.Entity<TblUserRole>()
                .Property(e => e.PageName)
                .IsUnicode(false);
        }
    }
}
