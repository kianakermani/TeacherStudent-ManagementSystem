using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "UserID")]
        public int UserID { get; set; }
        [Display(Name = "RoleID")]
        public int RoleID { get; set; }
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Family")]
        public string Family { get; set; }

        public List<UserViewModel> user { get; set; }

    }
}
