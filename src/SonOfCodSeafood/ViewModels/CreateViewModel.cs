using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SonOfCodSeafood.ViewModels
{
    public class CreateViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "RoleName")]
        public string RoleName { get; set; }
    }
}
