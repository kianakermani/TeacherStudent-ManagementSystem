using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class EmailViewModel
    {
        [Display(Name = "EmailAddress")]
        [Required(ErrorMessage = "Please fill {0} !")]
        [EmailAddress(ErrorMessage = "Email Adress is not valid !")]
        public string Email { get; set; }
    }
}
