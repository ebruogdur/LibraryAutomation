using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAutomation.Models
{
    public class LoadnedVM
    {
        public int ID { get; set; }

        public int StudentID { get; set; }
        public int BookID { get; set; }
        [Required(ErrorMessage = "Lütfen  alış tarihini sini giriniz.")]
        public  DateTime? LoadnedStartDate { get; set; }
        [Required(ErrorMessage = "Lütfen  veriş tarihini giriniz.")]
        public DateTime? loadnedEndDate { get; set; }

        public bool Control { get; set; }

        public int Price { get; set; }

        public int DEPT { get; set; }
        public string StudentName { get; set; }//foreign keylere ulaşmak için
        public string BookName { get; set; }
        public string _ISBN { get; set; }

        public bool _IsActive { get; set; }

        public int CountLoad { get; set; }

        public string Email { get; set; }

        public SelectList Students { get; set; }

        public SelectList Emanetler { get; set; }
    }
}