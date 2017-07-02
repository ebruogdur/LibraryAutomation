using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryAutomation.Security;
using LibraryAutomation.Models;

namespace LibraryAutomation.Controllers
{
    public class StudentController : BaseController
    {

        KutuphaneEntities _db = new KutuphaneEntities();


        // GET: Student
        public ActionResult Index()
        {

            List<StudentVM> studentModelList = _db.Students.Select(s => new StudentVM
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                IdentityNumber = s.IdentityNumber,
                PhoneNumber = s.PhoneNumber,
                BirthDate = s.BirthDate

            }).ToList();

            return View(studentModelList);


        }

        public ActionResult Add()
        {
            return View();

        }
        [HttpPost]
        public JsonResult Add(StudentVM model)
        {
            var cek = _db.Students.FirstOrDefault(p => p.IdentityNumber == model.IdentityNumber);
            if (ModelState.IsValid)
            {
                
                    if (cek == null)
                    {
                        Student entity = new Student();
                        entity.FirstName = model.FirstName;
                        entity.LastName = model.LastName;
                        entity.Email = model.Email;
                        entity.BirthDate = model.BirthDate;
                        entity.IdentityNumber = model.IdentityNumber;
                        entity.PhoneNumber = model.PhoneNumber;
                        entity.CreatedDate = DateTime.Now;
                        entity.Debt = Convert.ToInt32(0);

                        int toplam = 0;
                        var borclar = _db.Book_Student.Where(x => x.DEPT >= 0).ToList();
                        for (int i = 0; i < borclar.Count; i++)
                        {
                            toplam = toplam + Convert.ToInt32(borclar[i].DEPT);
                        }
                        entity.Debt = toplam;


                        _db.Students.Add(entity);
                        _db.SaveChanges();
                        //return RedirectToAction("Index", "Student");
                        return Json(new { Success = true, Message = "ekleme başarılı" }, JsonRequestBehavior.AllowGet);
                    }
                    else {
                        //ViewData["ErrorMessage"] = "bu tc de başka öğrenci vardır.";
                        //return View(model);
                        return Json(new { Success = false, Message = "bu tc de başka ogrenci var" }, JsonRequestBehavior.AllowGet);
                    }
               
            }
            else
            {
                //return View();
                return Json(new { Success = false, Message = "ekleme başarısız" }, JsonRequestBehavior.AllowGet);
            }


            
        }
        public ActionResult Edit(int id)
        {
            Student entity = _db.Students.Find(id);
            StudentVM model = new StudentVM();
            model.FirstName = entity.FirstName;
            model.LastName = entity.LastName;
            model.BirthDate = entity.BirthDate;
            model.PhoneNumber = entity.PhoneNumber;
            model.Email = entity.Email;
            model.IdentityNumber = entity.IdentityNumber;


            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(StudentVM model)
        {
            Student studentedit = _db.Students.Find(model.Id);
            studentedit.FirstName = model.FirstName;
            studentedit.LastName = model.LastName;
            studentedit.Email = model.Email;

            studentedit.BirthDate = model.BirthDate;
            studentedit.IdentityNumber = model.IdentityNumber;
            studentedit.PhoneNumber = model.PhoneNumber;

            studentedit.UpdatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {

                _db.SaveChanges();
                return RedirectToAction("Index", "Student");
            }
            else
            {

                return View();
            }


        }
        public ActionResult Delete(int id)
        {

            var bul = _db.Book_Student.FirstOrDefault(p => p.StudentID == id);
            var bull = _db.Students.Find(id).Debt;

            if (bul != null && bul.Control == true)
            {
                ViewData["ErrorMessage"] = "öğrenciye kitap emanettir, silemezsiniz";
                return View();
                
            }
            else if (bull > 0)
            {
                ViewData["borc"] = "borcu var silemezsin";
                return View();
               

            }


            else
            {

                _db.Students.Remove(_db.Students.Find(id));
                _db.SaveChanges();
                return RedirectToAction("Index","Student");

            }
            

        }

           

        public ActionResult Read(int id)
        {
           
            List<LoadnedVM> okunanlar = _db.Book_Student.Where(p=> p.StudentID == id).Select(s => new LoadnedVM
            {                
                BookName = s.Book.Name,
                _ISBN =s.Book.ISBN,
                loadnedEndDate=s.LoanedEndDate,
                LoadnedStartDate=s.LoadnedStartDate
            }).ToList();

            return View(okunanlar);
          
        }



        public ActionResult Borc(int id)
        {

            Student e = _db.Students.Find(id);
            StudentVM borcmodel = new StudentVM();
            borcmodel.Id = id;
           borcmodel.Debt = _db.Students.Find(borcmodel.Id).Debt;
          
          
            
            if (borcmodel.Debt==0)
            {
                ViewData["borchata"] = "borcu yoktur";
                return View(borcmodel);
            }
            else
            {
                return View(borcmodel);
            }

          
        }


        [HttpPost]
        public ActionResult Borc(StudentVM model)
        {

            Student en = _db.Students.Find(model.Id);
            model.Debt = en.Debt;
            
                var odenen = model.odenen;
                var borc = model.Debt;
                var kalan = borc - odenen;
          
                if(odenen>borc)
            {
                ViewData["negatif"] = "fazla ödeme yapamazsınız";
            }

           else
            {
                en.Debt = kalan;
                _db.SaveChanges();


                ViewData["kalan"] = "kalan borc: " + kalan + "TL DİR";

                return View();
            }
            return View();   
        }


    }
       
    }
