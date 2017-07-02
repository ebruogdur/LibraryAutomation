using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryAutomation.Models;
using LibraryAutomation.Security;
using System.Text;
using System.Net.Mail;


namespace LibraryAutomation.Controllers
{
    public class UserController :BaseController
    {

        KutuphaneEntities _db = new KutuphaneEntities();


        // GET: User
        public ActionResult Index()
        {

            List<UserVM> userModelList = _db.Users.Select(s => new UserVM
            {

                ID = s.ID,
                FirstName = s.FirstName,
                LastName = s.LastName,
                IdentityNumber = s.IdentityNumber,
                Email = s.Email,
                Birthdate = s.Birthdate,
                PhoneNumber = s.PhoneNumber,
                WorkStartDate = s.WorkStartDate,

            }).ToList();
            return View(userModelList);

        }
      

       
        public ActionResult Add()
        {
            var yetki = Session["User"];
            UserVM model = new UserVM();

            if(Convert.ToInt32(yetki)==1)
            {
                model.Roller = _db.Role.ToList();
                return View(model);
            }
           else
            {
                ViewData["yetki"] = "Bu işlem için yetkiniz yoktur.";
                return View("Yetki");

            }
          
           
        }

        [HttpPost]
        public ActionResult Add(UserVM model)
        {

            if (ModelState.IsValid)
            {

                User entity = new User();
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Email = model.Email;
                entity.Birthdate = model.Birthdate;
                entity.IdentityNumber = model.IdentityNumber;
                entity.WorkStartDate = model.WorkStartDate;
                entity.PhoneNumber = model.PhoneNumber;
                entity.IsActive = true;
                entity.CreatedDate = DateTime.Now;
                entity.Password = CreatePassword(6);


                //for (int i = 0; i < model.ListRoller.Count(); i++)
                //{
                //    entity.RoleId = model.ListRoller[i];
                //}

                model.Roller = _db.Role.ToList();
                //var secilen = model.Roller.Where(x => x.Id == model.RoleId);

                entity.RoleId = model.RoleId;
                _db.Users.Add(entity);
                _db.SaveChanges();

                MailMessage mail = new MailMessage();
                mail.To.Add(entity.Email);
                mail.From = new MailAddress("kutuhanesistemi@gmail.com");
                mail.Subject = "kullanıcı şifreniz";
                string Body = entity.Password;
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
              
                return RedirectToAction("Index", "User");
            }
            else
            {
                model.Roller = _db.Role.ToList();
                return View(model);
            }

           

        }

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public ActionResult Edit(int id)
        {
            var yetki2 = Session["User"];

            User entity = _db.Users.Find(id);
            UserVM model = new UserVM();

            if (Convert.ToInt32(yetki2) == 1)
            {
                model.FirstName = entity.FirstName;
                model.LastName = entity.LastName;
                model.Birthdate = entity.Birthdate;
                model.PhoneNumber = entity.PhoneNumber;
                model.WorkStartDate = entity.WorkStartDate;
                model.Email = entity.Email;

                model.IdentityNumber = entity.IdentityNumber;

                return View(model);
            }
            else
            {
                ViewData["yetki"] = "Bu işlem için yetkiniz yoktur.";
                return View("Yetki");
            }
            
        }

        [HttpPost]
        public ActionResult Edit(UserVM model)
        {
            var mevcut = _db.Users.Find(model.ID);
            mevcut.FirstName = model.FirstName;
            mevcut.LastName = model.LastName;
            mevcut.UpdatedDate = DateTime.Now;
            mevcut.IdentityNumber = model.IdentityNumber;
            mevcut.IsActive = true;
            mevcut.Email = model.Email;
            mevcut.PhoneNumber = model.PhoneNumber;
            mevcut.WorkStartDate = model.WorkStartDate;
            mevcut.Birthdate = model.Birthdate;


            if (ModelState.IsValid)
            {

                _db.SaveChanges();
                return RedirectToAction("Index", "User");

            }
            else
            {
                return View();
            }



        }
        public ActionResult Delete(int id)
        {

            var yetki3 = Session["User"];
            if(Convert.ToInt32(yetki3)==1)
            {
                _db.Users.Remove(_db.Users.Find(id));
                _db.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewData["yetki"] = "Bu işlem için yetkiniz yoktur.";
                return View("Yetki");
            }
            

        }

        //çıkış yapınca şifre 0 lanıyor
        public ActionResult SifreDegistir(UserVM model)
        {
            if(Session["loginuser"]!=null)
            {
                User us = (User)Session["loginuser"];

                User kullanici = _db.Users.Find(us.ID);
                kullanici.Password = model.Password;

                _db.SaveChanges();
             
                return View();
            }
            else
            {
                return View("Login", "Login");
            }
            
         
        
        }

    }
}