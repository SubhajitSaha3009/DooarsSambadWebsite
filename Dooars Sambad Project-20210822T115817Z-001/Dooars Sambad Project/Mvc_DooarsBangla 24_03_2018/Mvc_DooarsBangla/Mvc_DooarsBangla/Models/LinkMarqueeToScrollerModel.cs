using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_DooarsBangla.Models
{
    public class LinkMarqueeToScrollerModel
    {
        [Display(Name = "Marquee News ID")]
        [Required(ErrorMessage = "*")]
        public int MarqueeNewsID { get; set; }

        [Display(Name = "Scroller Image ID")]
        [Required(ErrorMessage = "*")]
        public int ScrImageID { get; set; }
    }
}