using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_DooarsBangla.Models
{
    public class NewsVideoModel
    {

        [Display(Name = "News ID")]
        //[Required(ErrorMessage = "*")]
        //[RegularExpression("([A-Za-z ]*)", ErrorMessage = "invalid format")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string NewsID { get; set; }


        [Display(Name = "Choose Section:")]
        [Required(ErrorMessage = "*")]
        public int Section { get; set; }



        [Display(Name = "Video Iframe Address:")]
        [Required(ErrorMessage = "*")]
        [StringLength(500, ErrorMessage = "Max 500 characters")]
        public string IFrameAddress { get; set; }


    }
}