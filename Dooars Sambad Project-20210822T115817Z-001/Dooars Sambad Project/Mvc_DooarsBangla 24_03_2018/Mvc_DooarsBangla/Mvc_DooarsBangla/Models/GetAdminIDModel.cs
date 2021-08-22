using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_DooarsBangla.Models
{
    public class GetAdminIDModel
    {
        [Display(Name = "Admin ID")]
        [Required(ErrorMessage = "Enter ID")]
        public int adminID { get; set; }
    }
}