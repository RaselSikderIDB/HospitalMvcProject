using System;

namespace HospitalMvcProject.Models.ViewModels
{
    internal class CustomExcludeCharacterAttribute : Attribute
    {
        private string v;

        public CustomExcludeCharacterAttribute(string v)
        {
            this.v = v;
        }
    }
}