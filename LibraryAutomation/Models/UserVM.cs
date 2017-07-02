using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAutomation.Models
{
    public class UserVM
    {
        public int ID { get; set; }

       
        [Required(ErrorMessage = "Lütfen  kullanıcının tc sini giriniz.")]
        [StringLength(11, ErrorMessage = "11 karakterden çok girdiniz ")]
        [MinLength(11,ErrorMessage ="11 karakterden az girdiniz")]
        public string IdentityNumber { get; set; }

        [Required(ErrorMessage = "Lütfen  kullanıcının adını  giriniz.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lütfen  kullanıcının soyadını giriniz.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Lütfen kullanıcının eposta adresini giriniz.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "adresinizi dogru formatta giriiniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen  kullanıcının doğum tarihini seçiniz")]

        public DateTime? Birthdate { get; set; }

        [Required(ErrorMessage = "Lütfen  kullanıcının telefonunu  giriniz.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "telefonu doğru formatta  giriniz.")]
        public string PhoneNumber { get; set; }

       
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen  kullanıcının işe giriş tarihini seçiniz")]
        public DateTime? WorkStartDate { get; set; }

        [Required(ErrorMessage = "Lütfen  pozisyon seçiniz")]
        public int  RoleId { get; set; }

        public int RoleName { get; set; }


        public List<Role> Roller { get; set; }
        
        //public List<int> ListRoller { get; set; }



    }
    

}