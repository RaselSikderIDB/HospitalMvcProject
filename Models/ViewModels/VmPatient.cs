using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalMvcProject.Models.ViewModels
{
    public class VmPatient
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string Picture { get; set; }
        public string DepartmentName { get; set; }
        public string DoctorName { get; set; }
    }
}