using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_DooarsBangla.Models
{
    public class PhotoGalleryModel
    {

        [Display(Name = "Image ID")]
        [Required(ErrorMessage = "*")]
        public int imageID { get; set; }

        [Display(Name = "Image Title")]
        [Required(ErrorMessage = "*")]
        [StringLength(150, ErrorMessage = "Max 150 chars")]
        public String ImageTitle { get; set; }


        [Display(Name = "Choose An Image")]
        [Required(ErrorMessage = "*")]
        public HttpPostedFileBase file { get; set; }

        public String ImageAddress { get; set; }

        [Display(Name = "Select Position")]
        [Required(ErrorMessage = "*")]
        public int Position { get; set; }
    }
}