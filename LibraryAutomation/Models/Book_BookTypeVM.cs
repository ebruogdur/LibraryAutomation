using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAutomation.Models
{
    public class Book_BookTypeVM
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int BookTypeID { get; set; }

        public string BookName { get; set; }
        public string BookAuthor { get; set; }

       
    }
}