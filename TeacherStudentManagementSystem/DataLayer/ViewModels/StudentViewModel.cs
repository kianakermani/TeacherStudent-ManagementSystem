using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class StudentViewModel
    {
        [Display(Name = "SID")]
        public int StudentID { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "FName")]
        public string Family { get; set; }
        [Display(Name = "CodeMeli")]
        public string CodeMeli { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }

        public List<StudentViewModel> student { get; set; }
    }
}
