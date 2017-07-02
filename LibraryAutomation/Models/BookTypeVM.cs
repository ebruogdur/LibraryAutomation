using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAutomation.Models
{
    public class BookTypeVM
    {

        public int ID { get; set; }

        [Required(ErrorMessage = "Lütfen  kitabın türünü giriniz.")]
        public string TypeName{ get; set; }

       






    }
}