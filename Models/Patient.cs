namespace HospitalMvcProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Patient")]
    public partial class Patient
    {
        public int PatientID { get; set; }

        [StringLength(50)]
        public string PatientName { get; set; }

        [StringLength(50)]
        public string Gender { get; set; }

        [StringLength(50)]
        public string Age { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(250)]
        public string Picture { get; set; }

        public int? DepartmentID { get; set; }

        public int? ReferDoctor { get; set; }

        public virtual Department Department { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
