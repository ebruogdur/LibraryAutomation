using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryAutomation.Models;
using LibraryAutomation.Security;
using System.Web.UI.WebControls;
using System.IO;
using System.Net.Mail;

namespace LibraryAutomation.Controllers

{
    public class BookController : BaseController
    {
        KutuphaneEntities _db = new KutuphaneEntities();


        // GET: Book
        public ActionResult Index(string option, string ss)
        {

            List<BookVM> bookModelList = _db.Books.Select(s => new BookVM
            {
                Id = s.Id,
                Name = s.Name,
                Page = s.Page,
                ISBN = s.ISBN,
                Author = s.Author,
                IsActive = s.IsActive,
                Count = s.Count,


            }).ToList();

            if (!string.IsNullOrEmpty(ss))
            {
                if (option == "Name")
                {
                    List<BookVM> SearchList = _db.Books.Where(p => p.Name.Contains(ss) || p.Author.Contains(ss)).Select(s => new BookVM
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Page = s.Page,
                        ISBN = s.ISBN,
                        Author = s.Author,
                        IsActive = s.IsActive,
                        Count = s.Count,


                    }).ToList();

                    if (SearchList != null)
                    {
                        return View(SearchList);
                    }
                }
                if (option == "Author")
                {
                    List<BookVM> SearchList = _db.Books.Where(p => p.Author == ss).Select(s => new BookVM
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Page = s.Page,
                        ISBN = s.ISBN,
                        Author = s.Author,
                        IsActive = s.IsActive,
                        Count = s.Count,


                    }).ToList();

                    if (SearchList != null)
                    {
                        return View(SearchList);
                    }
                }



            }

            if (bookModelList != null)
            {
                return View(bookModelList);
            }
            else
            {

                ModelState.AddModelError("", "kayıtlı kitap yok");
                return View();
            }


        }



        public ActionResult Add()
        {
            BookVM model = new BookVM();
            model.ListBookType = new List<int>();
            model.Tipler = _db.BookTypes.ToList();
           
            return View(model);

        }

        [HttpPost]
        public ActionResult Add(BookVM model, HttpPostedFileBase file)
        {

            var cek = _db.Books.FirstOrDefault(p => p.ISBN == model.ISBN);
            Book yeni = new Book();


            if (file != null)
            {

                file.SaveAs(Server.MapPath("~/Images/") + file.FileName);
                yeni.ImagePath = "/Images/" + file.FileName;
            }
         
            
                if (ModelState.IsValid  )
                {
                    if (cek == null)
                    {
                        yeni.Name = model.Name;
                        yeni.Page = model.Page;
                        yeni.ISBN = model.ISBN;
                        yeni.Count = model.Count;
                        yeni.Author = model.Author;
                        yeni.CreatedDate = DateTime.Now;
                        yeni.IsActive = true;
                    

                        _db.Books.Add(yeni);
                        _db.SaveChanges();

                  
                    for (int i = 0; i < model.ListBookType.Count(); i++)
                            {
                                Book_BookType ornek = new Book_BookType();
                                ornek.BookTypeID = model.ListBookType[i];
                                ornek.BookId = yeni.Id;
                                _db.Book_BookType.Add(ornek);
                                _db.SaveChanges();
                            }
                   



                    return RedirectToAction("Index", "Book");

                    }

                    else
                    {
                        model.Tipler = _db.BookTypes.ToList();
                        ViewData["ErrorMessage"] = "bu ISBN de de başka kitap vardır.";
                        return View(model);
                    }
                }
                else
                {
                        model.Tipler = _db.BookTypes.ToList();
                        return View(model);
                }
           
           
        }


        public ActionResult Edit(int id)
        {

            Book entity = _db.Books.Find(id);
            BookVM model = new BookVM();
            model.Name = entity.Name;
            model.Author = entity.Author;
            model.Page = entity.Page;
            model.Count = entity.Count;
            model.IsActive = entity.IsActive;
            model.ISBN = entity.ISBN;


            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(BookVM model, HttpPostedFileBase file)
        {

            Book mevcut = _db.Books.Find(model.Id);
            if (file != null)
            {

                file.SaveAs(Server.MapPath("~/Images/") + file.FileName);
                mevcut.ImagePath = "/Images/" + file.FileName;
            }
            if (ModelState.IsValid)
            {
                mevcut.Name = model.Name;
                mevcut.Author = model.Author;
                mevcut.Page = model.Page;
                mevcut.Count = model.Count;
                mevcut.ISBN = model.ISBN;

               
                mevcut.UpdatedDate = DateTime.Now;
                mevcut.IsActive = model.IsActive;

                if (mevcut.Count > 0)
                {
                    mevcut.IsActive = true;
                }

                _db.SaveChanges();
                return RedirectToAction("Index", "Book");
            }
            else
            {

                return View(model);
            }



        }
        public ActionResult Delete(int id)
        {

           

            var bul = _db.Book_Student.FirstOrDefault(p => p.BookID == id);
         
                if (bul!=null &&  bul.Control == true)
                {
                    ViewData["ErrorMessage"] = "kıtap ogrencide,silemzsin";
                    return View();
                }
                else
                {
                    _db.Books.Remove(_db.Books.Find(id));
              
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Book");
                }
        

        }




        public ActionResult AddBookofStudent(int id)
        {

            LoadnedVM oduncModel = new LoadnedVM();
            oduncModel.BookID = id;

            oduncModel.Students = new SelectList(_db.Students.ToList(), "Id", "Email");


            if (_db.Books.Find(id).Count > 0)
            {
                _db.Books.Find(id).IsActive = true;
                return View(oduncModel);
            }
            else
            {
                ViewData["ErrorMessage"] = "İstenilen kitap stokta bulunmamaktadır.";
                return View(oduncModel);

            }


        }




        [HttpPost]
        public ActionResult CAddBookofStudent(LoadnedVM model)
        {

            Book_Student entity = new Book_Student();


            if (ModelState.IsValid)
            {
                entity.StudentID = model.StudentID;
                entity.BookID = model.BookID;
                entity.LoadnedStartDate = model.LoadnedStartDate;
                entity.LoanedEndDate = model.loadnedEndDate;
                entity.Price = Convert.ToInt32(10);
                entity.Control = true;
                entity.DEPT = Convert.ToInt32(0);

                Book en = _db.Books.Find(entity.BookID);
                int sayi = en.Count;
                sayi--;
                en.Count = sayi;
                if (en.Count == 0)
                {
                    en.IsActive = false;
                }
                else
                {
                    en.IsActive = true;
                }



                var getir = _db.Book_Student.FirstOrDefault(p => p.StudentID == model.StudentID && p.BookID == model.BookID && p.Control == true);
                if (getir!=null)
                {

                    ViewData["Error"] = "İstenilen kitap zaten bu öğrencide var.";
                    return View();
                }


                _db.Book_Student.Add(entity);

                _db.SaveChanges();

                var bugun = DateTime.Now.Day;
                if(bugun>entity.LoanedEndDate.Value.Day)
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(_db.Book_Student.FirstOrDefault(x=>x.StudentID==model.StudentID).Student.Email);
                    mail.From = new MailAddress("kutuhanesistemi@gmail.com");
                    mail.Subject = "kitap iade süresi gecikmesi";
                    string Body = "kitabınız " + (bugun-entity.LoanedEndDate.Value.Day) + "gün gecikmiştir iade ediniz.günlük borcunuz 10 tl dir";
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential
                    ("kutuphanesistemi", "temmuz2016");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);

                }
                return RedirectToAction("Index", "Book");


            }
            else
            {
                return View();
            }




        }


        public ActionResult Emanet()
        {
            List<LoadnedVM> EmanetModelList = _db.Book_Student.Select(s => new LoadnedVM
            {
                ID=s.ID,
                StudentID = s.StudentID,
                BookID = s.BookID,
                loadnedEndDate = s.LoanedEndDate,
                LoadnedStartDate = s.LoadnedStartDate,
                StudentName = s.Student.FirstName + " " + s.Student.LastName,
                BookName = s.Book.Name + " / " + s.Book.Author,
                _ISBN = s.Book.ISBN,


            }).ToList();

            return View(EmanetModelList);



        }

        public ActionResult Iade(int id,int sid,int oid)
        {

           
            var bul = _db.Book_Student.FirstOrDefault(x => x.BookID==id && x.StudentID == sid && x.ID==oid);
            var bugun = DateTime.Now;
            var bitis = bul.LoanedEndDate;
            Book e = _db.Books.Find(id);

            if(bul!=null && bul.Control==true)
            {
                if(bitis.Value.Day >= bugun.Day)
                {
                    e.IsActive = true;
                    int sayi = e.Count;
                    sayi++;
                    e.Count = sayi;
                    bul.Control = false;
                    ViewData["Message"] = "seçilen kitap borçsuz iade edildi.";
                }
                else
                {
                    e.IsActive = true;
                    int sayi = e.Count;
                    sayi++;
                    e.Count = sayi;
                    bul.Control = false;
                    int hesapla = bugun.Day - bitis.Value.Day;
                    int sonuc = hesapla * bul.Price;
                    
                    bul.DEPT = sonuc;
                  

                    ViewData["Borc"] = "secilen kitap borclu olarak iade edildi" + " " + "BORC:" + sonuc + "TL DİR";

                }

                int toplam = 0;
                var borclar = _db.Book_Student.Where(x => x.DEPT >= 0).ToList();
                for (int i = 0; i < borclar.Count; i++)
                {
                    toplam = toplam + Convert.ToInt32(borclar[i].DEPT);
                }
                Student en = _db.Students.Find(sid);
                en.Debt = toplam;

                
            }
            else
            {
                ViewData["hata"] = "zaten iade olmuş";
            }

            _db.SaveChanges();

            return View();

        }

      

        public ActionResult Ozellik(int id)

        {
            Book en = _db.Books.Find(id);
            BookVM model = new BookVM();
            model.Name = en.Name;
            model.Author = en.Author;
            model.Page = en.Page;
            model.Count = en.Count;
            model.IsActive = en.IsActive;
            model.ISBN = en.ISBN;


            return View(model);
        }

        }
    }




     

    

