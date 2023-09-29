using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class HomeWorkViewModel
    {
        [Display(Name = "HID")]
        [Key]
        public int ID { get; set; }
        [Display(Name = "TitleLesson")]
        public string Title { get; set; }
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [Display(Name = "TeacherName")]
        public string TeacherName { get; set; }
        [Display(Name = "TeacherID")]
        public string TeacherID { get; set; }
        [Display(Name = "DeliveryDate")]
        public string DeliveryDate { get; set; }
        [Display(Name = "StudentID")]
        public string StudentID { get; set; }

        public List<HomeWorkViewModel> homework { get; set; }
    }
}
