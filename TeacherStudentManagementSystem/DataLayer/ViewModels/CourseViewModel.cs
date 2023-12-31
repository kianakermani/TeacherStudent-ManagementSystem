﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class CourseViewModel
    {
        [Display(Name = "CID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseID { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Teacher")]
        public string Teacher { get; set; }
        [Display(Name = "Days")]
        public string Days { get; set; }
        [Display(Name = "Time")]
        public string Time { get; set; }
        [Display(Name = "StartDate")]
        public string StartDate { get; set; }
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        public List<CourseViewModel> course { get; set; }
    }
}
