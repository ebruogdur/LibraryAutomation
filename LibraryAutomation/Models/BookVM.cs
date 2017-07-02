using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAutomation.Models
{
    public class BookVM
    {
        public int Id{ get; set; }
        [Required(ErrorMessage = "Lütfen  kitabın İsmini giriniz.")]
        public string Name  { get; set; }
        [Required(ErrorMessage = "Lütfen  kitabın sayfasını giriniz.")]
        public int Page { get; set; }
        [Required(ErrorMessage = "Lütfen  kitabın yazarını giriniz.")]
        public string Author{ get; set; }
        [Required(ErrorMessage = "Lütfen  kitabın ISBN numarasını giriniz.")]
        [StringLength(13, ErrorMessage = "13 karakterden çok girdiniz ")]
        [MinLength(13, ErrorMessage = "13 karakterden az girdiniz")]
        public string ISBN { get; set; }
        
        public bool IsActive { get; set; }

        public int Count { get; set; }

    
        public List<BookType> Tipler { get; set; }

        public List<int> ListBookType { get; set; }


     

    }
}