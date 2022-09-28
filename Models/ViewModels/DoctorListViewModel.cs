using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalMvcProject.Models.ViewModels
{
    public class DoctorListViewModel
    {
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public System.DateTime JoiningDate { get; set; }

        public string ImageName { get; set; }

        public string ImageUrl { get; set; }
    }
}