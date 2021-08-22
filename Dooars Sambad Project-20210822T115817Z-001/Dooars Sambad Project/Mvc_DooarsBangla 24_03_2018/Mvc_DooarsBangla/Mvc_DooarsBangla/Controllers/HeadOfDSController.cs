using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_DooarsBangla.Models;
using System.Web.Security;

namespace Mvc_DooarsBangla.Controllers
{
    public class HeadOfDSController : Controller
    {
        public ActionResult LoginToPortalAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginToPortalAdmin(LoginModel model)
        {
            try
            {
                HomeDAL dal = new HomeDAL();
                if (model.LogInId.ToString().Length == 2)
                {
                    CheckPasswordStatusEnum status = (CheckPasswordStatusEnum)dal.login(model);
                    if (status == CheckPasswordStatusEnum.WrongPassword)
                    {
                        ViewBag.errmsg = "Invalid login ID or password!";
                        return View();
                    }
                    else if (status == CheckPasswordStatusEnum.NewUser)
                    {
                        FormsAuthentication.SetAuthCookie(model.LogInId.ToString(), false);
                        return RedirectToAction("UpdatePasswordAdmin", "Admin");
                    }
                    else if (status == CheckPasswordStatusEnum.Updated)
                    {
                        FormsAuthentication.SetAuthCookie(model.LogInId.ToString(), false);
                        return RedirectToAction("Index", "Admin");
                    }
                }
                ViewBag.errmsg = "Enter Valid Login ID";
                return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}
