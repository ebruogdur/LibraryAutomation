using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryAutomation.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Lütfen  eposta adresinizi giriniz.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "adresinizi dogru formatta giriiniz")]
        public string Email { get; set; }
        [StringLength(6, ErrorMessage = "6 karakterli şifrenizi giriniz ")]
        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        public string Password { get; set; }
    }
}