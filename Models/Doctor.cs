namespace HospitalMvcProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Doctor")]
    public partial class Doctor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Doctor()
        {
            Patients = new HashSet<Patient>();
        }

        public int DoctorID { get; set; }

        [StringLength(50)]
        public string DoctorName { get; set; }

        [StringLength(50)]
        public string PhoneNo { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public DateTime JoiningDate { get; set; }

        [StringLength(600)]
        public string ImageName { get; set; }

        [StringLength(600)]
        public string ImageUrl { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
