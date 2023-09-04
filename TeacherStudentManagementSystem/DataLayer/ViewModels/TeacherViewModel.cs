using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLayer.ViewModels
{
    public class TeacherViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Display(Name = "TID")]
        public int TeacherID { get; set; }
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

        public List<TeacherViewModel> teacher { get; set; }
    }
}
