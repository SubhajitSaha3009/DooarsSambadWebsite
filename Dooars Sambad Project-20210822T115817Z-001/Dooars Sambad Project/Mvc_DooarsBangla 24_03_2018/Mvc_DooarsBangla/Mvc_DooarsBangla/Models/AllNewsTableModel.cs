using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Mvc_DooarsBangla.Models
{
    public class AllNewsTableModel
    {
        [Display(Name = "News ID")]
        [Required(ErrorMessage = "*")]
        //[RegularExpression("([A-Za-z ]*)", ErrorMessage = "invalid format")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string NewsID { get; set; }

        [Display(Name = "News Title")]
        [Required(ErrorMessage = "*")]
        //[RegularExpression("([A-Za-z ]*)", ErrorMessage = "invalid format")]
        [StringLength(100, ErrorMessage = "Max 100 characters")]
        public string NewsTitle { get; set; }


        [Display(Name = "Category")]
        [Required(ErrorMessage = "*")]
        //[RegularExpression("([A-Za-z -]*)", ErrorMessage = "invalid format")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string category { get; set; }


        [Display(Name = "Section 1 Description")]
        [Required(ErrorMessage = "*")]
        [StringLength(4000, ErrorMessage = "Max 4000 characters")]
        public string Section1Description { get; set; }

        [Display(Name = "Section 1 Image")]
        [Required(ErrorMessage = "*")]
        public HttpPostedFileBase Section1ImageFile { get; set; }
        public string Section1ImageString { get; set; }


        [Display(Name = "Section 2 Description")]
        //[Required(ErrorMessage = "*")]
        [StringLength(3500, ErrorMessage = "Max 3500 characters")]
        public string Section2Description { get; set; }

        [Display(Name = "Section 2 Image")]
        //[Required(ErrorMessage = "*")]
        public HttpPostedFileBase Section2ImageFile { get; set; }
        public string Section2ImageString { get; set; }


        [Display(Name = "Section 3 Description")]
        //[Required(ErrorMessage = "*")]
        [StringLength(3000, ErrorMessage = "Max 3000 characters")]
        public string Section3Description { get; set; }

        [Display(Name = "Section 3 Image")]
        public HttpPostedFileBase Section3ImageFile { get; set; }
        public string Section3ImageString { get; set; }

        [Display(Name = "Is Latest?")]
        //[Required(ErrorMessage = "*")]
        [RegularExpression("([A-Za-z ]*)", ErrorMessage = "invalid format")]
        [StringLength(3, ErrorMessage = "Max 3 characters")]
        public string IsLatest { get; set; }


        [Display(Name = "Is Popular?")]
        [Required(ErrorMessage = "*")]
        [RegularExpression("([A-Za-z ]*)", ErrorMessage = "invalid format")]
        [StringLength(3, ErrorMessage = "Max 3 characters")]
        public string IsPopular { get; set; }


        [Display(Name = "Priority Of News?")]
        [Required(ErrorMessage = "*")]
        [RegularExpression("([A-Za-z ]*)", ErrorMessage = "invalid format")]
        [StringLength(3, ErrorMessage = "Max 3 characters")]
        public string PriorityOfNews { get; set; }



        [Display(Name = "Marquee News ID")]
        //[Required(ErrorMessage = "*")]
        public int MarqueeNewsID { get; set; }



        [Display(Name = "Date of News")]
        [Required(ErrorMessage = "*")]
        public string NewsDate { get; set; }


        [Display(Name = "Scroller Image ID")]
        //[Required(ErrorMessage = "*")]
        public int ScrImageID { get; set; }


        public string IFrameAddress { get; set; }
        public int Section { get; set; }
    }
}