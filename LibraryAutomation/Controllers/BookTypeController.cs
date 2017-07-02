using LibraryAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryAutomation.Security;

namespace LibraryAutomation.Controllers
{
    public class BookTypeController : BaseController
    {
        KutuphaneEntities _db = new KutuphaneEntities();
        // GET: BookType
        public ActionResult Index()
        {
            List<BookTypeVM> booktypeModelList = _db.BookTypes.Select(s => new BookTypeVM
            {

                ID = s.ID,
                TypeName = s.TypeName

            }).ToList();
            return View(booktypeModelList);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Add(BookTypeVM model)
        {
            BookType yeni = new BookType();
            yeni.TypeName = model.TypeName;
            if (ModelState.IsValid)
            {
                _db.BookTypes.Add(yeni);
                _db.SaveChanges();
                //return RedirectToAction("Index", "BookType");
                return Json(new { Success = true, Message = "Ekleme başarılı" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //return View();
                return Json(new { Success = false, Message = "Ekleme başarısız" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Edit(int id)
        {
            BookType entity = _db.BookTypes.Find(id);
            BookTypeVM model = new BookTypeVM();
            model.TypeName = entity.TypeName;
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(BookTypeVM model)
        {
            BookType mevcut = _db.BookTypes.Find(model.ID);
            mevcut.TypeName = model.TypeName;


            if (ModelState.IsValid)
            {

                _db.SaveChanges();
                return RedirectToAction("Index", "BookType");
            }
            else
            {

                return View();
            }

        }

        public ActionResult Delete(int id)
        {

            _db.BookTypes.Remove(_db.BookTypes.Find(id));

            var bul = _db.Book_BookType.FirstOrDefault(p => p.BookTypeID == id);

            if (bul != null)
            {
                
                    ViewData["ErrorMessage"] = "bu türde kitap vardır silemezsiniz";
                    return View();
              
            }

            else
            {
                _db.SaveChanges();
                return RedirectToAction("Index", "BookType");

            }


          

        }

      
        public ActionResult Listele(int id)
        {

            List<Book_BookTypeVM> Listele = _db.Book_BookType.Where(p=>p.BookTypeID==id).Select (s => new Book_BookTypeVM
            {

                BookName = s.Book.Name,
                BookAuthor=s.Book.Author

            }).ToList();

            return View(Listele);
        }

      
    }
}