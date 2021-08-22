using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_DooarsBangla.Models;
using System.Web.Security;
using PagedList;
using PagedList.Mvc;

namespace Mvc_DooarsBangla.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        HomeDAL dal = new HomeDAL();
        int  pNo = 1;
        static string tempId;

        public ActionResult Index()
        {
            try
            { 
            MyCustomHomeModel model = new MyCustomHomeModel();
            model.ScrollerList = dal.GetScrollerImage();
            model.MarqueeList = dal.getAllMarqueeNewsToDisplay();
            model.LatestList = dal.LatsetNews();
            model.PopularList = dal.PopulartNews();
            model.PhotoGallery = dal.GetGalleryImage();
            model.listFeedback = dal.showAllVisibleFeedback();

           // AdminDAL Adal = new AdminDAL();
            ViewBag.c = dal.getNoOfPages();
            //ViewBag.c = obj.Count;

            return View(model);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


       


        //////subha 12/12

        //public ActionResult BlockAllNews()
        //{
        //    return View();
        //}

        ///subha 13/12//////////////////////////

        public ActionResult FalakataAllNews(int? pageNo)
        {
            try
            {
            AllNewsTableModel model = new AllNewsTableModel();
            List<AllNewsTableModel> list_allNews = new List<AllNewsTableModel>();
            list_allNews = dal.getBlockAllNews("Falakata");
            TempData["allNews"] = list_allNews;
            if (list_allNews.Count == 0)
            {
                return RedirectToAction("ContentNotFound");
            }
            return View(list_allNews.ToPagedList(pageNo ?? 1, 10));
             }
            catch(Exception)
            {
                return View("Error");
            }
        }
        public ActionResult MadarihatAllNews(int? pageNo)
        {
            try
            { 
            AllNewsTableModel model = new AllNewsTableModel();
            List<AllNewsTableModel> list_allNews = new List<AllNewsTableModel>();
            list_allNews = dal.getBlockAllNews("Madarihat-Birpara");
            TempData["allNews"] = list_allNews;
            if (list_allNews.Count == 0)
            {
                return RedirectToAction("ContentNotFound");
            }
            return View(list_allNews.ToPagedList(pageNo ?? 1, 10));
            }
            catch (Exception)
            {
                return View("Error");
            }

        }
        public ActionResult KumargramAllNews(int? pageNo)
        {
            try
            {
                AllNewsTableModel model = new AllNewsTableModel();
                List<AllNewsTableModel> list_allNews = new List<AllNewsTableModel>();
                list_allNews = dal.getBlockAllNews("Kumargram");
                TempData["allNews"] = list_allNews;
                if (list_allNews.Count == 0)
                {
                    return RedirectToAction("ContentNotFound");
                }
                return View(list_allNews.ToPagedList(pageNo ?? 1, 10));
            }
            catch (Exception)
            {
                return View("Error");
            }

        }
        public ActionResult Alipuduar1AllNews(int? pageNo)
        {
            try
            {
                AllNewsTableModel model = new AllNewsTableModel();
                List<AllNewsTableModel> list_allNews = new List<AllNewsTableModel>();
                list_allNews = dal.getBlockAllNews("Alipurduar-1");
                TempData["allNews"] = list_allNews;
                if (list_allNews.Count == 0)
                {
                    return RedirectToAction("ContentNotFound");
                }
                return View(list_allNews.ToPagedList(pageNo ?? 1, 10));
            }
            catch (Exception)
            {
                return View("Error");
            }

        }
        public ActionResult Alipuduar2AllNews(int? pageNo)
        {
            try
            { 
            AllNewsTableModel model = new AllNewsTableModel();
            List<AllNewsTableModel> list_allNews = new List<AllNewsTableModel>();
            list_allNews = dal.getBlockAllNews("Alipurduar-2");
            TempData["allNews"] = list_allNews;
            if (list_allNews.Count == 0)
            {
                return RedirectToAction("ContentNotFound");
            }
            return View(list_allNews.ToPagedList(pageNo ?? 1, 10));
            }
            catch (Exception)
            {
                return View("Error");
            }
      
        }
        public ActionResult KalchiniAllNews(int? pageNo)
        {
            try
            {

                AllNewsTableModel model = new AllNewsTableModel();
                List<AllNewsTableModel> list_allNews = new List<AllNewsTableModel>();
                list_allNews = dal.getBlockAllNews("Kalchini");
                TempData["allNews"] = list_allNews;
                if (list_allNews.Count == 0)
                {
                    return RedirectToAction("ContentNotFound");
                }
                return View(list_allNews.ToPagedList(pageNo ?? 1, 10));
            }
            catch (Exception)
            {
                return View("Error");
            }

        }
        public ActionResult AamarDooarsAllNews(int? pageNo)
        {
            try
            { 
            AllNewsTableModel model = new AllNewsTableModel();
            List<AllNewsTableModel> list_allNews = new List<AllNewsTableModel>();
            list_allNews = dal.getBlockAllNews("AamarDooars");
            TempData["allNews"] = list_allNews;
            if (list_allNews.Count == 0)
            {
                return RedirectToAction("ContentNotFound");
            }
            return View(list_allNews.ToPagedList(pageNo ?? 1, 10));
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult BibidhaAllNews(int? pageNo)
        {
            try
            { 
            AllNewsTableModel model = new AllNewsTableModel();
            List<AllNewsTableModel> list_allNews = new List<AllNewsTableModel>();
            list_allNews = dal.getBlockAllNews("Bibidha");
            TempData["allNews"] = list_allNews;
            if (list_allNews.Count == 0)
            {
                return RedirectToAction("ContentNotFound");
            }
            return View(list_allNews.ToPagedList(pageNo?? 1,10));
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult BriefNews(string id)
        {
            try
            { 
            if(TempData["NewsID"] != null)
            {
                id = TempData["NewsID"].ToString();
            }
            else if(id == null)
            {
                id = tempId;
            }
            int count = 1;
            BlockSingleNewsCustomModel model = new BlockSingleNewsCustomModel();
            model.fullNews = dal.getBriefNews(id);
            model.PopularList = dal.RelatedNews(id,model.fullNews.category);
            PrevNextModel pnmodel = new PrevNextModel();
            if(model.PopularList.Count>1)
            {
                foreach(var item in model.PopularList)
                {
                     if(count>2)
                     {
                         break;
                     }
                     if(count==1)
                     {
                         pnmodel.NewsID = item.NewsID;
                         pnmodel.NewsTitle = item.NewsTitle;
                         pnmodel.ImageAddress = item.Section1ImageString;
                     }
                    else if(count==2)
                     {
                         pnmodel.NewsID1 = item.NewsID;
                         pnmodel.NewsTitle1 = item.NewsTitle;
                         pnmodel.ImageAddress1 = item.Section1ImageString;
                     }
                    count++;
                   
                }
            }
            model.prevnxtList = pnmodel;
            ViewBag.Title1 = model.fullNews.NewsTitle;
            if (model.fullNews.Section1Description.Length >= 400)
            {
                ViewBag.Desc = model.fullNews.Section1Description.Substring(0, 400);
            }
            else
            {
                ViewBag.Desc = model.fullNews.Section1Description;
            }
            ViewBag.image = model.fullNews.Section1ImageFile;
            ViewBag.Url = "http://www.dooarssambad.in/Home/BriefNews/"+model.fullNews.NewsID;
            return View(model);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult ContentNotFound()
        {
            try
            { 
            BlockSingleNewsCustomModel model = new BlockSingleNewsCustomModel();
            model.PopularList = dal.PopulartNews();
            return View(model);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }



        public ActionResult MarqueeRedirection(int mID)
        {
            try
            { 
            string newsID = dal.getNewsId(mID);
            TempData["NewsID"] = newsID;
            tempId=newsID;
            if(newsID==null)
            {
                return RedirectToAction("ContentNotFound");
            }
            return RedirectToAction("BriefNews","Home");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult ScrollerRedirection(int sID)
        {
            try
            { 
            string newsID = dal.getNewsIdForScroller(sID);
            TempData["NewsID"] = newsID;
            tempId = newsID;
            if (newsID == null)
            {
                return RedirectToAction("ContentNotFound");
            }
            return RedirectToAction("BriefNews", "Home");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }



        //////////////Contact Page//////////////////////
        [HttpGet]
        public ActionResult ContactUS()
        {
            try
            { 
            MycustomForFeedBackModel model = new MycustomForFeedBackModel();
            model.MarqueeList = dal.getAllMarqueeNewsToDisplay();
            model.PopularList = dal.PopulartNews();
            return View(model);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }



        public ActionResult SendFeedbackForm()
        {   
            return PartialView();
        }
        [HttpPost]
        public ActionResult SendFeedbackForm(AddFeedBackModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (dal.AddFeedBack(model))
                    {
                        ViewBag.msg = "Feedback sent";
                        ModelState.Clear();
                        //return Json(new { Success = true, Message = "" });
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.msg = "Feedback not sent";
                        ModelState.Clear();
                        return RedirectToAction("Error");
                    }
                }
                else
                {
                    ViewBag.msg = "Feedback not sent";
                    ModelState.Clear();
                    // return Json(new { Success = false, Message = "" }); 
                    return RedirectToAction("Error");
               }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }









        [HttpGet]
        public ActionResult ForgetPasswordToEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPasswordToEmail(ForgetPasswordToEmailModel model)
        {
            AdminDAL Adal = new AdminDAL();
            try
            {
                if (Adal.forgetPasswordToEmail(model))
                {
                    ViewBag.msg = "Your Password is sent to your registered Email";
                    return View();
                }
                else
                {
                    ViewBag.Emsg = "Invalid Admin ID";
                    return View();
                }

                //string[] arr = str.Split(' ');


                //string emailID = arr[0];
                //string password = arr[1];

                // dal.mailConnection(model,emailID);
                // return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        
            

        //static int pNo=1;
        public ActionResult E_paper(int pNo)
        {
            try
            {
                MyCustomEpaperModel model = new MyCustomEpaperModel();
                model.PopularList = dal.PopulartNews();
                model.PaperImage = dal.getNewsPaperPage(pNo);
                ViewBag.c = dal.getNoOfPages();
                return View(model);
            }
             
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult GetDevelopers()
        {
            return View();
        }

    }
}
