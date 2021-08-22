using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_DooarsBangla.Models
{
    public class AddScrollerImageModel
    {

        [Display(Name = "Scroller Image ID")]
        [Required(ErrorMessage = "*")]
        public int ScrlImageID { get; set; }

        [Display(Name = "Scroller Image Header")]
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Max 100 chars")]
        public String ImageHeader { get; set; }

        [Display(Name = "Scroller Image Description")]
        [Required(ErrorMessage = "*")]
        [StringLength(180, ErrorMessage = "Max 180 chars")]
        public String ImageDesc { get; set; }

        [Display(Name = "Choose An Image")]
        [Required(ErrorMessage = "*")]
        public HttpPostedFileBase file { get; set; }

       
       
        public String ImageAddress { get; set; }

        [Display(Name = "Select Position")]
        [Required(ErrorMessage = "*")]
        public int Position { get; set; }

    }
}