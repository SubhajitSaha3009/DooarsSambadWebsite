using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_DooarsBangla.Models
{
    public class AddFeedBackModel
    {
        [Display(Name = "Feedback ID")]
        //[Required(ErrorMessage = "*")]
        public int FeedbackID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "*")]
        [StringLength(30, ErrorMessage = "Max Charecter length exceeded")]
        public string Name { get; set; }

        [Display(Name = "Contact Number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Only 10 digits")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "invalid format")]
        [Required(ErrorMessage = "Please enter your contact number")]
        public string contactNumber { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "*")]
        [StringLength(140, ErrorMessage = "Max 140 chars")]
        public string Message { get; set; }

        public string feedbackStatus {get;set;}

    }
}