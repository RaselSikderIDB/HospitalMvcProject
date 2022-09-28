using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalMvcProject.Models.ViewModels
{
    public class DoctorCreateViewModel
    {
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Doctor Name Is Required")]
        [DataType(DataType.Text)]
        [Display(Name = "Doctor Name")]
        [StringLength(50, ErrorMessage = "Doctor Name Must be within 50 Charecter")]
        [CustomExcludeCharacter("/.,!@#$%")]
        public string DoctorName { get; set; }

        public string PhoneNo { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email Is Required")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [DisplayName("Joining Date")]
        [CustomJoiningDate(ErrorMessage = "Joining Date must be less than or equal to Today's Date")]
        public System.DateTime JoiningDate { get; set; }
        [DisplayName("Image Name")]
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}