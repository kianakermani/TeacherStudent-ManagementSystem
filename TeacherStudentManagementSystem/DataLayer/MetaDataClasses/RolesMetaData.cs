using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class RolesMetaData
    {
        [Key]
        public int RoleID { get; set; }
        [Display(Name = "RoleTitle")]
        [Required(ErrorMessage ="Please fill {0}")]
        public string RoleTitle { get; set; }
        [Display(Name = "RoleName (System Name)")]
        [Required(ErrorMessage = "Please fill {0}")]
        public string RoleName { get; set; }
    }

    [MetadataType(typeof(RolesMetaData))]
    public partial class Roles {
    }

}
