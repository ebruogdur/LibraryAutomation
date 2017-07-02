using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Models
{
    public class StudentVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen  öğrencinin numarasını giriniz.")]
        [StringLength(11, ErrorMessage = "11 karakterden çok girdiniz ")]
        [MinLength(11, ErrorMessage = "11 karakterden az girdiniz")]
        public string IdentityNumber{ get; set; }
        [Required(ErrorMessage = "Lütfen  öğrencinin adını  giriniz.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lütfen  öğrencinin soyadını giriniz.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Lütfen öğrencinin eposta adresinizi giriniz.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "adresinizi dogru formatta giriiniz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lütfen  öğrencinin doğun tarihini seçiniz")]
        public DateTime? BirthDate { get; set; }
        [Required(ErrorMessage = "Lütfen  öğrencinin telefonunu  giriniz.")]
        public string PhoneNumber { get; set; }

        public int Debt { get; set; }
        [Required(ErrorMessage ="bos gecilemez")]
        public int odenen { get; set; }
       


    }
}