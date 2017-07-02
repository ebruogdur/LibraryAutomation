using LibraryAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryAutomation.Security;

namespace LibraryAutomation.Controllers
{
    public class RoleController : BaseController
    {
        KutuphaneEntities _db = new KutuphaneEntities();
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(RoleVM model)
        {
           
            if (ModelState.IsValid)
            {
                Role yenirole = new Role();
                yenirole.RoleName = model.RoleName;
                _db.Role.Add(yenirole);
                _db.SaveChanges();

                //TempData["msg"] = "ekleme başarılı";
               
                return Json(new { Success = true, Message = "ekleme başarılı" }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                ModelState.AddModelError("","boş geçilemez");
                //TempData["msg2"] = "ekleme başarısız";
                return Json(new { Success = false, Message = "ekleme başarısız" }, JsonRequestBehavior.AllowGet);
            }

         
        }

        
    }
}