using Mvc_DooarsBangla.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace Mvc_DooarsBangla.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        AdminDAL dal = new AdminDAL();
       
        public ActionResult Index()
        {
            try
            {
                AdminModel model = dal.getAdminProfile(Convert.ToInt32(User.Identity.Name));
                ViewBag.ps = TempData["updatedmsg"];
                return View(model);
            }
            catch(Exception)
            {
                return View("Error");
            }
           
        }


        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAdmin(AdminModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    string idpassword = dal.addAdmin(model);
                    {
                        if (idpassword != null)
                        {
                          
                            string[] arr = idpassword.Split(' ');


                            ViewBag.id = arr[0];
                            ViewBag.password = arr[1];

                            return View("AdminAdded");
                        }
                        else
                        {
                            ViewBag.Redirectionmsg = "Admin not added.";
                            ViewBag.Btnlbl = "Add Admin";
                            ViewBag.BtnAction = "AddAdmin";
                            return View("Fail");
                        }
                    }

                }
                else
                {
                    ViewBag.Redirectionmsg = "Admin not added.";
                    ViewBag.Btnlbl = "Add Admin";
                    ViewBag.BtnAction = "AddAdmin";
                    return View("Fail");
                }

            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        [HttpGet]
        public ActionResult AddMarqueeNews()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMarqueeNews(MarqueeModel model)
        {
            try 
            { 

            if(dal.addMarqueeNews(model))
            {
		ViewBag.Redirectionmsg = "Marquee added Successfully.";
                ViewBag.Btnlbl = "Add Another Marquee";
                ViewBag.BtnAction = "AddMarqueeNews";
                return View("Success");
            }
            else
            ViewBag.Redirectionmsg = "Marquee not added.";
            ViewBag.Btnlbl = "Try Again";
            ViewBag.BtnAction = "AddMarqueeNews";
            return View("Fail");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        [HttpGet]
        public ActionResult GetAllMarqueeNews()
        {
            try
            {
            List<MarqueeModel> list_marquees= dal.getAllMarqueeNews();
            return View(list_marquees);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult MakeMarqueeInvisible(int id)
        {
            try
            {
                if (dal.makeMarqueeInvisible(id))
                {
                    return RedirectToAction("GetAllMarqueeNews");
                }
                else
                {
                    ViewBag.Redirectionmsg = "News Not Updated.";
                    ViewBag.Btnlbl = "Try Again";
                    ViewBag.BtnAction = "GetAllMarqueeNews";
                    return View("Fail");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult MakeMarqueeVisible(int id)
        {
            try
            {
                if (dal.makeMarqueeVisible(id))
                {
                    return RedirectToAction("GetAllMarqueeNews");
                }
                else
                {
                    ViewBag.Redirectionmsg = "News Not Updated.";
                    ViewBag.Btnlbl = "Try Again";
                    ViewBag.BtnAction = "GetAllMarqueeNews";
                    return View("Fail");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }




        /////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public ActionResult UpdatePasswordAdmin()
        {
            try
            {

            List<SelectListItem> lst_securityQuestion = new List<SelectListItem>();
            lst_securityQuestion.Add(new SelectListItem { Text = "Select", Value = "" });
            lst_securityQuestion.Add(new SelectListItem { Text = "What is your's first school name?", Value = "What is your's first school name?" });
            lst_securityQuestion.Add(new SelectListItem { Text = "What is your pet’s name?", Value = "What is your pet’s name?" });
            lst_securityQuestion.Add(new SelectListItem { Text = "In what city or town does your nearest sibling live?", Value = "In what city or town does your nearest sibling live?" });
            lst_securityQuestion.Add(new SelectListItem { Text = "What is your favorite food?", Value = "What is your favorite food?" });

            ViewBag.securityQuestion1 = lst_securityQuestion;

            return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult UpdatePasswordAdmin(ChangePasswordAdminModel model)
        {
            try
            {
                List<SelectListItem> lst_securityQuestion = new List<SelectListItem>();
                lst_securityQuestion.Add(new SelectListItem { Text = "Select", Value = "" });
                lst_securityQuestion.Add(new SelectListItem { Text = "What is your's first school name?", Value = "What is your's first school name?" });
                lst_securityQuestion.Add(new SelectListItem { Text = "What is your pet’s name?", Value = "What is your pet’s name?" });
                lst_securityQuestion.Add(new SelectListItem { Text = "In what city or town does your nearest sibling live?", Value = "In what city or town does your nearest sibling live?" });
                lst_securityQuestion.Add(new SelectListItem { Text = "What is your favorite food?", Value = "What is your favorite food?" });

                ViewBag.securityQuestion1 = lst_securityQuestion;


                int adminID = Convert.ToInt32(User.Identity.Name);
                //LoginDAL dal = new LoginDAL();
                if (dal.updatePassword(adminID, model))
                {
                    TempData["updatedmsg"] = "Password is updated";
                    return RedirectToAction("Index");
                }
                ViewBag.Redirectionmsg = "Password not updated.";
                ViewBag.Btnlbl = "Try Again";
                ViewBag.BtnAction = "UpdatePasswordAdmin";
                return View("Fail");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginToPortalAdmin", "HeadOfDS");
        }


        /////////////////////////////Atanu 12//////////////////
        private bool isValidContentImage(String ContentType)
        {
            return ContentType.Equals("image/jpeg") || ContentType.Equals("image/jpg") || ContentType.Equals("image/png");
        }

        [HttpGet]
        public ActionResult AddScrollImages()
        {
            List<SelectListItem> list_Position = new List<SelectListItem>();
            list_Position.Add(new SelectListItem { Text = "Select", Value = "" });
            list_Position.Add(new SelectListItem { Text = "1st", Value = "1" });
            list_Position.Add(new SelectListItem { Text = "2nd", Value = "2" });
            list_Position.Add(new SelectListItem { Text = "3rd", Value = "3" });
            list_Position.Add(new SelectListItem { Text = "4th", Value = "4" });
            list_Position.Add(new SelectListItem { Text = "5th", Value = "5" });
            list_Position.Add(new SelectListItem { Text = "6th", Value = "6" });

            ViewBag.PositionSelection = list_Position;
            return View();
        }

        [HttpPost]
        public ActionResult AddScrollImages(AddScrollerImageModel model)
        {
            try
            {
            List<SelectListItem> list_Position = new List<SelectListItem>();
            list_Position.Add(new SelectListItem { Text = "Select", Value = "" });
            list_Position.Add(new SelectListItem { Text = "1st", Value = "1" });
            list_Position.Add(new SelectListItem { Text = "2nd", Value = "2" });
            list_Position.Add(new SelectListItem { Text = "3rd", Value = "3" });
            list_Position.Add(new SelectListItem { Text = "4th", Value = "4" });
            list_Position.Add(new SelectListItem { Text = "5th", Value = "5" });
            list_Position.Add(new SelectListItem { Text = "6th", Value = "6" });

            ViewBag.PositionSelection = list_Position;

            bool flag = isValidContentImage(model.file.ContentType);
            if (!flag)
            {

                ViewBag.Error = "Only jpg and png are allowed";
                return View();
            }
            if (model.file.ContentLength > 3000000)
            {
                ViewBag.Error = "File size must be less than 3MB.";
                return View();
            }

            string filename = dal.AddScrollerImage(model);
            if (filename == "false")
            {
                ViewBag.Redirectionmsg = "Scroller Image not added.";
                ViewBag.Btnlbl = "Try Again.";
                ViewBag.BtnAction = "AddScrollImages";
                return View("Fail");
            }
            var path = Path.Combine(Server.MapPath("~/ScrollerImages/"), filename);
            model.file.SaveAs(path);
            ViewBag.Redirectionmsg = "Scroller Image added Successfully.";
            ViewBag.Btnlbl = "Add Another Image";
            ViewBag.BtnAction = "AddScrollImages";
            return View("Success");
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        [HttpGet]
        public ActionResult NewsUpload()
        {
            try
            { 
            List<SelectListItem> list_cat = new List<SelectListItem>();
            list_cat.Add(new SelectListItem { Text = "Select", Value = "" });
            list_cat.Add(new SelectListItem { Text = "Falakata", Value = "Falakata" });
            list_cat.Add(new SelectListItem { Text = "Madarihat-Birpara", Value = "Madarihat-Birpara" });
            list_cat.Add(new SelectListItem { Text = "Kumargram", Value = "Kumargram" });
            list_cat.Add(new SelectListItem { Text = "Alipurduar-1", Value = "Alipurduar-1" });
            list_cat.Add(new SelectListItem { Text = "Alipurduar-2", Value = "Alipurduar-2" });
            list_cat.Add(new SelectListItem { Text = "Kalchini", Value = "Kalchini" });
            list_cat.Add(new SelectListItem { Text = "Aamar Dooars", Value = "AamarDooars" });
            list_cat.Add(new SelectListItem { Text = "Bibidha", Value = "Bibidha" });
            ViewBag.cat = list_cat;

            List<SelectListItem> list_yesNo = new List<SelectListItem>();
            list_yesNo.Add(new SelectListItem { Text = "Select", Value = "" });
            list_yesNo.Add(new SelectListItem { Text = "Yes", Value = "Yes" });
            list_yesNo.Add(new SelectListItem { Text = "No", Value = "No" });

            ViewBag.yesno = list_yesNo;
            ViewBag.mID = TempData["mid"];
            ViewBag.ScrID = TempData["ScrImgID"];

            return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());


        }


        [HttpPost]
        public ActionResult NewsUpload(AllNewsTableModel model)
        {
            try 
            { 
            model.NewsID = RandomString(20);
            List<SelectListItem> list_cat = new List<SelectListItem>();
            list_cat.Add(new SelectListItem { Text = "Select", Value = "" });
            list_cat.Add(new SelectListItem { Text = "Falakata", Value = "Falakata" });
            list_cat.Add(new SelectListItem { Text = "Madarihat-Birpara", Value = "Madarihat-Birpara" });
            list_cat.Add(new SelectListItem { Text = "Kumargram", Value = "Kumargram" });
            list_cat.Add(new SelectListItem { Text = "Alipurduar-1", Value = "Alipurduar-1" });
            list_cat.Add(new SelectListItem { Text = "Alipurduar-2", Value = "Alipurduar-2" });
            list_cat.Add(new SelectListItem { Text = "Kalchini", Value = "Kalchini" });
            list_cat.Add(new SelectListItem { Text = "Aamar Dooars", Value = "AamarDooars" });
            list_cat.Add(new SelectListItem { Text = "Bibidha", Value = "Bibidha" });
            ViewBag.cat = list_cat;

            List<SelectListItem> list_yesNo = new List<SelectListItem>();
            list_yesNo.Add(new SelectListItem { Text = "Select", Value = "" });
            list_yesNo.Add(new SelectListItem { Text = "Yes", Value = "Yes" });
            list_yesNo.Add(new SelectListItem { Text = "No", Value = "No" });

            ViewBag.yesno = list_yesNo;




            if (!isValidContentImage(model.Section1ImageFile.ContentType))
            {
                ViewBag.Error = "Only jpg and png are allowed";
                return View();
            }

            if (model.Section1ImageFile.ContentLength > 3000000)
            {
                ViewBag.Error = "File Size Must Be Less Than 3MB";
                return View();
            }
            string filename1 = model.NewsID + "_1" + Path.GetExtension(model.Section1ImageFile.FileName);
            var path = Path.Combine(Server.MapPath("~/NewsImages/"), filename1);
            model.Section1ImageFile.SaveAs(path);

            if (model.Section2ImageFile != null)
            {
                if (!isValidContentImage(model.Section2ImageFile.ContentType))
                {
                    ViewBag.Error = "Only jpg and png are allowed";
                    return View();
                }

                if (model.Section2ImageFile.ContentLength > 3000000)
                {
                    ViewBag.Error = "File Size Must Be Less Than 3MB";
                    return View();
                }
                string filename2 = model.NewsID + "_2" + Path.GetExtension(model.Section2ImageFile.FileName);
                var path2 = Path.Combine(Server.MapPath("~/NewsImages/"), filename2);
                model.Section2ImageFile.SaveAs(path2);
            }


            if (model.Section3ImageFile != null)
            {
                if (!isValidContentImage(model.Section3ImageFile.ContentType))
                {
                    ViewBag.Error = "Only jpg and png are allowed";
                    return View();
                }

                if (model.Section3ImageFile.ContentLength > 3000000)
                {
                    ViewBag.Error = "File Size Must Be Less Than 3MB";
                    return View();
                }
                string filename3 = model.NewsID + "_3" + Path.GetExtension(model.Section3ImageFile.FileName);
                var path3 = Path.Combine(Server.MapPath("~/NewsImages/"), filename3);
                model.Section3ImageFile.SaveAs(path3);
            }

            if(dal.UploadNews(model))
            {
                TempData["nid"] = model.NewsID;
                return RedirectToAction("NewesVideoUpload");
            }
            ViewBag.Redirectionmsg = "News not Uploaded.";
            ViewBag.Btnlbl = "Try Again.";
            ViewBag.BtnAction = "NewsUpload";
            return View("Fail");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        /// <summary>
        /// /////////////////////video
        /// </summary>
        /// <param name="marqueeID"></param>
        /// <returns></returns>


        [HttpGet]
        public ActionResult NewesVideoUpload()
        {
            try
            { 
            if (TempData["nid"] == null)
            {
                return RedirectToAction("NewsUpload");
            }

            ViewBag.NewsID = TempData["nid"];
            List<SelectListItem> list_Sec = new List<SelectListItem>();
            list_Sec.Add(new SelectListItem { Text = "Select", Value = "" });
            list_Sec.Add(new SelectListItem { Text = "1", Value = "1" });
            list_Sec.Add(new SelectListItem { Text = "2", Value = "2" });
            list_Sec.Add(new SelectListItem { Text = "3", Value = "3" });

            ViewBag.Sect = list_Sec;
            return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult NewesVideoUpload(NewsVideoModel model)
        {
            try
            { 
            if (dal.newesVideoUpload(model))
            {
                ViewBag.Redirectionmsg = "News Uploaded Successfully.";
                ViewBag.Btnlbl = "TUpload Another News.";
                ViewBag.BtnAction = "NewsUpload";
                return View("Success");
            }
            ViewBag.Redirectionmsg = "News not Uploaded.";
            ViewBag.Btnlbl = "Try Again.";
            ViewBag.BtnAction = "NewsUpload";
            return View("Fail");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        public ActionResult Skip()
        {
            ViewBag.Redirectionmsg = "News Uploaded Successfully.";
            ViewBag.Btnlbl = "Upload Another News.";
            ViewBag.BtnAction = "NewsUpload";
            return View("Success");
        }




        public ActionResult UploadMarqueeFullNews(int marqueeID)
        {
            try
            { 
            TempData["mid"] = marqueeID;
            return RedirectToAction("NewsUpload");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        [HttpGet]
        public ActionResult AllNews()
        {
            try
            { 
            List<AllNewsTableModel> obj = dal.getAllNewsforTable();
            if (obj.Count == 0)
            {
                ViewBag.Nullmsg = "No News Found.";
            }
            ViewBag.msg = TempData["Rmsg"];
            ViewBag.emsg = TempData["Remsg"];
            return View(obj);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult UpdateNews(string id)
        {

            try
            { 
            AllNewsTableModel model = dal.getBriefNews(id);
            ViewBag.NewsID = model.NewsID;
            ViewBag.ttle = model.NewsTitle;
            ViewBag.category = model.category;
            ViewBag.des1 = model.Section1Description;
            ViewBag.des2 = model.Section3Description;
            ViewBag.des3 = model.Section3Description;
            ViewBag.IsLatest = model.IsLatest;
            ViewBag.IsPopular = model.IsPopular;
            ViewBag.PriorityOfNews = model.PriorityOfNews;
            ViewBag.dt = model.NewsDate;



            List<SelectListItem> list_cat = new List<SelectListItem>();
            list_cat.Add(new SelectListItem { Text = "Select", Value = "" });
            list_cat.Add(new SelectListItem { Text = "Falakata", Value = "Falakata" });
            list_cat.Add(new SelectListItem { Text = "Madarihat-Birpara", Value = "Madarihat-Birpara" });
            list_cat.Add(new SelectListItem { Text = "Kumargram", Value = "Kumargram" });
            list_cat.Add(new SelectListItem { Text = "Alipurduar-1", Value = "Alipurduar-1" });
            list_cat.Add(new SelectListItem { Text = "Alipurduar-2", Value = "Alipurduar-2" });
            list_cat.Add(new SelectListItem { Text = "Kalchini", Value = "Kalchini" });
            list_cat.Add(new SelectListItem { Text = "Aamar Dooars", Value = "AamarDooars" });
            list_cat.Add(new SelectListItem { Text = "Bibidha", Value = "Bibidha" });
            ViewBag.cat = list_cat;

            List<SelectListItem> list_yesNo = new List<SelectListItem>();
            list_yesNo.Add(new SelectListItem { Text = "Select", Value = "" });
            list_yesNo.Add(new SelectListItem { Text = "Yes", Value = "Yes" });
            list_yesNo.Add(new SelectListItem { Text = "No", Value = "No" });

            ViewBag.yesno = list_yesNo;


            return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult UpdateNews(AllNewsTableModel model)
        {
            try
            {
                List<SelectListItem> list_cat = new List<SelectListItem>();
                list_cat.Add(new SelectListItem { Text = "Select", Value = "" });
                list_cat.Add(new SelectListItem { Text = "Falakata", Value = "Falakata" });
                list_cat.Add(new SelectListItem { Text = "Madarihat-Birpara", Value = "Madarihat-Birpara" });
                list_cat.Add(new SelectListItem { Text = "Kumargram", Value = "Kumargram" });
                list_cat.Add(new SelectListItem { Text = "Alipurduar-1", Value = "Alipurduar-1" });
                list_cat.Add(new SelectListItem { Text = "Alipurduar-2", Value = "Alipurduar-2" });
                list_cat.Add(new SelectListItem { Text = "Kalchini", Value = "Kalchini" });
                list_cat.Add(new SelectListItem { Text = "Aamar Dooars", Value = "AamarDooars" });
                list_cat.Add(new SelectListItem { Text = "Bibidha", Value = "Bibidha" });
                ViewBag.cat = list_cat;

                List<SelectListItem> list_yesNo = new List<SelectListItem>();
                list_yesNo.Add(new SelectListItem { Text = "Select", Value = "" });
                list_yesNo.Add(new SelectListItem { Text = "Yes", Value = "Yes" });
                list_yesNo.Add(new SelectListItem { Text = "No", Value = "No" });

                ViewBag.yesno = list_yesNo;




                if (!isValidContentImage(model.Section1ImageFile.ContentType))
                {
                    ViewBag.Error = "Only jpg and png are allowed";
                    return View();
                }

                if (model.Section1ImageFile.ContentLength > 3000000)
                {
                    ViewBag.Error = "File Size Must Be Less Than 3MB";
                    return View();
                }
                string filename1 = model.NewsID + "_1" + Path.GetExtension(model.Section1ImageFile.FileName);
                var path = Path.Combine(Server.MapPath("~/NewsImages/"), filename1);
                model.Section1ImageFile.SaveAs(path);

                if (model.Section2ImageFile != null)
                {
                    if (!isValidContentImage(model.Section2ImageFile.ContentType))
                    {
                        ViewBag.Error = "Only jpg and png are allowed";
                        return View();
                    }

                    if (model.Section2ImageFile.ContentLength > 3000000)
                    {
                        ViewBag.Error = "File Size Must Be Less Than 3MB";
                        return View();
                    }
                    string filename2 = model.NewsID + "_2" + Path.GetExtension(model.Section2ImageFile.FileName);
                    var path2 = Path.Combine(Server.MapPath("~/NewsImages/"), filename2);
                    model.Section2ImageFile.SaveAs(path2);
                }
                else
                {
                    //delete code
                }


                if (model.Section3ImageFile != null)
                {
                    if (!isValidContentImage(model.Section3ImageFile.ContentType))
                    {
                        ViewBag.Error = "Only jpg and png are allowed";
                        return View();
                    }

                    if (model.Section3ImageFile.ContentLength > 3000000)
                    {
                        ViewBag.Error = "File Size Must Be Less Than 3MB";
                        return View();
                    }
                    string filename3 = model.NewsID + "_3" + Path.GetExtension(model.Section3ImageFile.FileName);
                    var path3 = Path.Combine(Server.MapPath("~/NewsImages/"), filename3);
                    model.Section3ImageFile.SaveAs(path3);
                }
                else
                {
                    //delete 
                }


                if (dal.UpdateNews(model))
                {
                    ViewBag.Redirectionmsg = "News Updated Successfully.";
                    ViewBag.Btnlbl = "Update Another News.";
                    ViewBag.BtnAction = "AllNews";
                    return View("Success");
                }
                ViewBag.Redirectionmsg = "News not Updated.";
                ViewBag.Btnlbl = "Try Again";
                ViewBag.BtnAction = "AllNews";
                return View("Fail");
            }
            catch (Exception)
            {
                return View("Error");
            }

        }



        public ActionResult GetAllScrollerImage()
        {
            try
            { 
            List<AddScrollerImageModel> obj = dal.GetScrollerImage();
            return View(obj);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult UploadScrImgFullNews(int ScrID)
        {
            try
            { 
            TempData["ScrImgID"] = ScrID;
            return RedirectToAction("NewsUpload");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }









        ///Add Gallery
        [HttpGet]
        public ActionResult AddGalleryImages()
        {
            List<SelectListItem> list_Position = new List<SelectListItem>();
            list_Position.Add(new SelectListItem { Text = "Select", Value = "" });
            list_Position.Add(new SelectListItem { Text = "1st", Value = "1" });
            list_Position.Add(new SelectListItem { Text = "2nd", Value = "2" });
            list_Position.Add(new SelectListItem { Text = "3rd", Value = "3" });
            list_Position.Add(new SelectListItem { Text = "4th", Value = "4" });
            list_Position.Add(new SelectListItem { Text = "5th", Value = "5" });
            list_Position.Add(new SelectListItem { Text = "6th", Value = "6" });
            
            ViewBag.PositionSelection = list_Position;
            return View();
        }

        [HttpPost]
        public ActionResult AddGalleryImages(PhotoGalleryModel model)
        {
            try
            { 
            List<SelectListItem> list_Position = new List<SelectListItem>();
            list_Position.Add(new SelectListItem { Text = "Select", Value = "" });
            list_Position.Add(new SelectListItem { Text = "1st", Value = "1" });
            list_Position.Add(new SelectListItem { Text = "2nd", Value = "2" });
            list_Position.Add(new SelectListItem { Text = "3rd", Value = "3" });
            list_Position.Add(new SelectListItem { Text = "4th", Value = "4" });
            list_Position.Add(new SelectListItem { Text = "5th", Value = "5" });
            list_Position.Add(new SelectListItem { Text = "6th", Value = "6" });
           
            ViewBag.PositionSelection = list_Position;

            bool flag = isValidContentImage(model.file.ContentType);
            if (!flag)
            {

                ViewBag.Error = "Only jpg and png are allowed";
                return View();
            }
            if (model.file.ContentLength > 3000000)
            {
                ViewBag.Error = "File size must be less than 3MB.";
                return View();
            }

            string filename = dal.AddGalleryImage(model);
            if (filename == "false")
            {
                ViewBag.Redirectionmsg = "Image not Uploaded.";
                ViewBag.Btnlbl = "Try Again.";
                ViewBag.BtnAction = "AddGalleryImages";
                return View("Fail");
            }
            var path = Path.Combine(Server.MapPath("~/Gallery/"), filename);
            model.file.SaveAs(path);
            ViewBag.Redirectionmsg = "Image Uploaded Successfully.";
            ViewBag.Btnlbl = "Upload Another Image.";
            ViewBag.BtnAction = "AddGalleryImages";
            return View("Success");
            }
            catch (Exception)
            {
                return View("Error");
            }

        }



        ////////////////////////////////souvik 16/12////////////////////////////////////////////
        [HttpGet]
        public ActionResult ShowAllFeedback()
        {
            try
            {
                List<AddFeedBackModel> list_fdbk = dal.showAllFeedback();
                ViewBag.delerrmsg = TempData["FeedErrMsg"];
                ViewBag.delmsg = TempData["FeedMsg"];
                return View(list_fdbk);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        public ActionResult DeleteNews(string id)
        {
            try
            {
                //int scrlID = dal.GetScrlID(id);

                

            if (dal.DeeleteNewsByID(id))
            {

                if (System.IO.File.Exists(Request.MapPath("~/NewsImages/" + id + "_1.jpg")))
                {
                    System.IO.File.Delete(Request.MapPath("~/NewsImages/" + id + "_1.jpg"));
                }
                else if (System.IO.File.Exists(Request.MapPath("~/NewsImages/" + id + "_1.png")))
                {
                    System.IO.File.Delete(Request.MapPath("~/NewsImages/"  + id + "_1.png"));
                }
                if (System.IO.File.Exists(Request.MapPath("~/NewsImages/" + id + "_2.jpg")))
                {
                    System.IO.File.Delete(Request.MapPath("~/NewsImages/" + id + "_2.jpg"));
                }
                else if (System.IO.File.Exists(Request.MapPath("~/NewsImages/" + id + "_2.png")))
                {
                    System.IO.File.Delete(Request.MapPath("~/NewsImages/" + id + "_2.png"));
                }
                if (System.IO.File.Exists(Request.MapPath("~/NewsImages/" + id + "_3.jpg")))
                {
                    System.IO.File.Delete(Request.MapPath("~/NewsImages/" + id + "_3.jpg"));
                }
                else if (System.IO.File.Exists(Request.MapPath("~/NewsImages/" + id + "_3.png")))
                {
                    System.IO.File.Delete(Request.MapPath("~/NewsImages/" + id + "_3.png"));
                }


                //if (System.IO.File.Exists("~/ScrollerImages/" + scrlID + ".jpg"))
                //{
                //    System.IO.File.Delete("~/ScrollerImages/" + scrlID + ".jpg");
                //}
                //else if (System.IO.File.Exists("~/ScrollerImages/" + scrlID + ".png"))
                //{
                //    System.IO.File.Delete("~/ScrollerImages/" + scrlID + ".png");
                //}

                TempData["Rmsg"] = "News Deleted Succesfully.";
                return RedirectToAction("AllNews");
            }
            TempData["Remsg"] = "News not Deleted, Try Again";
            return RedirectToAction("AllNews");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        /////////////////////////Delete feedback//////////////////
        public ActionResult DeleteFeedback(int id)
        {
            try
            { 
            if (dal.DeleteFeedbackById(id))
            {
                TempData["FeedMsg"] = "Feedback Deleted Successfully.";
                return RedirectToAction("ShowAllFeedback");
            }
            else
            {
                TempData["FeedErrMsg"] = "Feedback not Deleted.";
                return RedirectToAction("ShowAllFeedback");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        public ActionResult InvisibleFeedback(int id)
        {
            try
            {
                if (dal.invisibleFeedback(id))
                {
                    return RedirectToAction("ShowAllFeedback");
                }
                else
                {
                    ViewBag.Redirectionmsg = "Feedback Not Updated.";
                    ViewBag.Btnlbl = "Try Again";
                    ViewBag.BtnAction = "ShowAllFeedback";
                    return View("Fail");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult VisibleFeedback(int id)
        {
            try
            {
                if (dal.visibleFeedback(id))
                {
                    return RedirectToAction("ShowAllFeedback");
                }
                else
                {
                    ViewBag.Redirectionmsg = "Feedback Not Updated.";
                    ViewBag.Btnlbl = "Try Again";
                    ViewBag.BtnAction = "ShowAllFeedback";
                    return View("Fail");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        [HttpGet]
        public ActionResult LinkMarqueeToScroller(int marqueeID)
        {
            try
            { 
            ViewBag.id = marqueeID;
            return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult LinkMarqueeToScroller(LinkMarqueeToScrollerModel model)
        {
            try
            { 
            if(model.MarqueeNewsID==0)
            {
                return RedirectToAction("GetAllMarqueeNews");
            }

            if(dal.linkMarqueeToScroller(model.MarqueeNewsID, model.ScrImageID))
            {
                ViewBag.Redirectionmsg = "Linked Successfully.";
                ViewBag.Btnlbl = "Link Another";
                ViewBag.BtnAction = "GetAllMarqueeNews";
                return View("Success");
            }
           else
            {
                ViewBag.Redirectionmsg = "Failed to Link.";
                ViewBag.Btnlbl = "Try Again.";
                ViewBag.BtnAction = "GetAllMarqueeNews";
                return View("Fail");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        public ActionResult GetAllNewsPaperPage()
        {
            try
            {
                List<NewsPaperPageModel> obj = dal.getAllNewsPaperPage();


                ViewBag.delerrmsg = TempData["FeedErrMsg"];
                ViewBag.delmsg = TempData["FeedMsg"];
                return View(obj);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        public ActionResult DeleteNewsPage(int pageNo)
        {
            try
            {
                if (dal.deleteNewsPage(pageNo))
                {
                    TempData["FeedMsg"] = "News Image Deleted Successfully.";
                    return RedirectToAction("GetAllNewsPaperPage");
                }
                else
                {
                    TempData["FeedErrMsg"] = "News Image not Deleted.";
                    return RedirectToAction("GetAllNewsPaperPage");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }



        [HttpGet]
        public ActionResult AddNewPaperPage()
        {
            List<SelectListItem> list_Pageno = new List<SelectListItem>();
            list_Pageno.Add(new SelectListItem { Text = "Select", Value = "" });
            list_Pageno.Add(new SelectListItem { Text = "1st", Value = "1" });
            list_Pageno.Add(new SelectListItem { Text = "2nd", Value = "2" });
            list_Pageno.Add(new SelectListItem { Text = "3rd", Value = "3" });
            list_Pageno.Add(new SelectListItem { Text = "4th", Value = "4" });
            list_Pageno.Add(new SelectListItem { Text = "5th", Value = "5" });
            list_Pageno.Add(new SelectListItem { Text = "6th", Value = "6" });
            list_Pageno.Add(new SelectListItem { Text = "7th", Value = "7" });
            list_Pageno.Add(new SelectListItem { Text = "8th", Value = "8" });
            list_Pageno.Add(new SelectListItem { Text = "9th", Value = "9" });
            list_Pageno.Add(new SelectListItem { Text = "10th", Value = "10" });
            list_Pageno.Add(new SelectListItem { Text = "11th", Value = "11" });
            list_Pageno.Add(new SelectListItem { Text = "12th", Value = "12" });
            list_Pageno.Add(new SelectListItem { Text = "13th", Value = "13" });
            list_Pageno.Add(new SelectListItem { Text = "14th", Value = "14" });
            list_Pageno.Add(new SelectListItem { Text = "15th", Value = "15" });

            ViewBag.PageNumberSelection = list_Pageno;
            return View();
        }

        [HttpPost]
        public ActionResult AddNewPaperPage(NewsPaperPageModel model)
        {
            try
            { 
            List<SelectListItem> list_Pageno = new List<SelectListItem>();
            list_Pageno.Add(new SelectListItem { Text = "Select", Value = "" });
            list_Pageno.Add(new SelectListItem { Text = "1st", Value = "1" });
            list_Pageno.Add(new SelectListItem { Text = "2nd", Value = "2" });
            list_Pageno.Add(new SelectListItem { Text = "3rd", Value = "3" });
            list_Pageno.Add(new SelectListItem { Text = "4th", Value = "4" });
            list_Pageno.Add(new SelectListItem { Text = "5th", Value = "5" });
            list_Pageno.Add(new SelectListItem { Text = "6th", Value = "6" });
            list_Pageno.Add(new SelectListItem { Text = "7th", Value = "7" });
            list_Pageno.Add(new SelectListItem { Text = "8th", Value = "8" });
            list_Pageno.Add(new SelectListItem { Text = "9th", Value = "9" });
            list_Pageno.Add(new SelectListItem { Text = "10th", Value = "10" });
            list_Pageno.Add(new SelectListItem { Text = "11th", Value = "11" });
            list_Pageno.Add(new SelectListItem { Text = "12th", Value = "12" });
            list_Pageno.Add(new SelectListItem { Text = "13th", Value = "13" });
            list_Pageno.Add(new SelectListItem { Text = "14th", Value = "14" });
            list_Pageno.Add(new SelectListItem { Text = "15th", Value = "15" });

            ViewBag.PageNumberSelection = list_Pageno;
            bool flag = isValidContentImage(model.file.ContentType);
            if (!flag)
            {

                ViewBag.Error = "Only jpg and png are allowed";
                return View();
            }
            if (model.file.ContentLength > 3000000)
            {
                ViewBag.Error = "File size must be less than 3MB.";
                return View();
            }

            model.ImageAddress = dal.AddNewsPaperImage(model);
            if (model.ImageAddress == "false")
            {
                ViewBag.Redirectionmsg = "Image was not Uploaded.";
                ViewBag.Btnlbl = "Try Again";
                ViewBag.BtnAction = "AddNewPaperPage";
                return View("Fail");
            }
            else
            {
                var path3 = Path.Combine(Server.MapPath("~/NewsPaperPage/"), model.ImageAddress);
                model.file.SaveAs(path3);
                ViewBag.Redirectionmsg = "Image Uploaded Successfully.";
                ViewBag.Btnlbl = "Add Another Image";
                ViewBag.BtnAction = "AddNewPaperPage";
                return View("Success");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        [HttpGet]
        public ActionResult GetAdminID()
        {
            ViewBag.rmsg = TempData["rmsg"];
            return View();
        }
        [HttpPost]
        public ActionResult GetAdminID(GetAdminIDModel model)
        {
            TempData["Aid"] = model.adminID;
            return RedirectToAction("UpdateAdminProfile");
        }


        [HttpGet]
        public ActionResult UpdateAdminProfile()
        {
            try
            {
                int adminID =Convert.ToInt32(TempData["Aid"]);
                //int adminID = Convert.ToInt32(User.Identity.Name);
                //int adminID = 7005;
                AdminModel model = dal.getAdminProfile(adminID);
                if(model.adminID==0)
                {
                    TempData["rmsg"] = "Invalid ID";
                    return RedirectToAction("GetAdminID");
                }
                ViewBag.id = model.adminID;
                ViewBag.name = model.adminName;
                ViewBag.email = model.adminEmailID;
                ViewBag.mbl = model.mobileNo;
                ViewBag.AdminStatus = model.adminStatus;
                List<SelectListItem> list_Status = new List<SelectListItem>();
                list_Status.Add(new SelectListItem { Text = "Active", Value = "Active" });
                list_Status.Add(new SelectListItem { Text = "Inactive", Value = "Inactive" });
                ViewBag.lst = list_Status;

                return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult UpdateAdminProfile(AdminModel model)
        {
            try
            {
                //model.adminID = Convert.ToInt32(User.Identity.Name);
                if (dal.updateAdminProfile(model))
                {
                    ViewBag.Redirectionmsg = "Profile is updated";
                    ViewBag.Btnlbl = "Update Another Admin profile";
                    ViewBag.BtnAction = "GetAdminID";
                    return View("Success");
                }
                ViewBag.Redirectionmsg = "Profile not updated";
                ViewBag.Btnlbl = "Try Again";
                ViewBag.BtnAction = "GetAdminID";
                return View("Fail");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


    }
}
