using LibraryAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAutomation.Controllers
{
    public class LoginController : Controller
    {
        KutuphaneEntities _db = new KutuphaneEntities();

        // GET: Login

        public ActionResult Login()
        {
            if (Session["User"] != null)
            {
                
                return RedirectToAction("Index", "Home");
            }
            return View();
          

        }
        [HttpPost]
    
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                User loginuser = _db.Users.FirstOrDefault(p => p.Email == model.Email && p.Password == model.Password);
                if (loginuser != null)
                {
                    //başarılı login

                    Session["User"] = loginuser.RoleId;
                    Session["loginuser"] = loginuser;
                   

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //hatalı login
                    ModelState.AddModelError("hata", "kullanıcı adı veya şifre hatalı");
                    return View(model);
                }
            }
            else
            {
                return View();
            }
     
        }

        public ActionResult Cikis()
        {
            Session.Abandon();
            return Redirect("/Login/Login");
        }


      
    }

    
}

