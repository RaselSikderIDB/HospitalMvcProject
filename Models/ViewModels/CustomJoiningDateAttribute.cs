using System;

namespace HospitalMvcProject.Models.ViewModels
{
    internal class CustomJoiningDateAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
    }
}