using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_DooarsBangla.Models
{
    public class MarqueeModel
    {
        [Display(Name = "Admin ID")]
        [Required(ErrorMessage = "*")]
        public int MarqueeNewsID { get; set; }


        [Display(Name = "Marquee News :")]
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Max 100 characters")]
        public string MarqueeDescription { get; set; }


        [Display(Name = "Marquee Status :")]
        //[Required(ErrorMessage = "*")]
        [StringLength(10, ErrorMessage = "Max 10 characters")]
        public string MarqueeStatus { get; set; }
    }
}