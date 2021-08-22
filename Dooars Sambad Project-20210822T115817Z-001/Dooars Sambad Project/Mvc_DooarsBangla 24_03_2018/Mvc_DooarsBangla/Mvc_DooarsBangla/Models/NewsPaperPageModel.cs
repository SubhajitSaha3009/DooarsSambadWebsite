using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_DooarsBangla.Models
{
    public class NewsPaperPageModel
    {
        [Display(Name = "News Paper Image ID")]
        [Required(ErrorMessage = "*")]
        public int PaperImageID { get; set; }

        [Display(Name = "Choose An Image")]
        [Required(ErrorMessage = "*")]
        public HttpPostedFileBase file { get; set; }

        public String ImageAddress { get; set; }

        [Display(Name = "Select page number")]
        [Required(ErrorMessage = "*")]
        public int PageNumber { get; set; }

    }
}