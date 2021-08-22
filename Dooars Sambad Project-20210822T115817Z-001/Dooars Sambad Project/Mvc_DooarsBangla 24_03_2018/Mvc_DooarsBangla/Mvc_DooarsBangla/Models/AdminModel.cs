using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_DooarsBangla.Models
{
    public class AdminModel
    {
        [Display(Name = "Admin ID")]
        [Required(ErrorMessage = "*")]
        public int adminID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "*")]
        [RegularExpression("([A-Za-z ]*)", ErrorMessage = "invalid format")]
        [StringLength(60, ErrorMessage = "Max 60 characters")]
        public string adminName { get; set; }


        [Display(Name = "Mobile Number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Only 10 digits")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "invalid format")]
        [Required(ErrorMessage = "*")]
        public string mobileNo { get; set; }



        [Display(Name = "Email ID")]
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "Invalid format")]
        [StringLength(30, ErrorMessage = "Max 30 chars")]
        public string adminEmailID { get; set; }


        public string adminStatus { get; set; }

    }
}