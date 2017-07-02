using LibraryAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace LibraryAutomation.Controllers
{
    public class HomeController : Controller
    {

        KutuphaneEntities _db = new KutuphaneEntities();

        // GET: Home
        public ActionResult Index()
        {
            SiteHomeVM vm = new SiteHomeVM();
            vm.Books = _db.Books.ToList();
            vm.BookTypes = _db.BookTypes.ToList();
            
            return View(vm);
         
        }

      


    }
}