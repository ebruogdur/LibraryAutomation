using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAutomation.Models
{
    public class RoleVM
    {
        public int  Id{ get; set; }
        [Required(ErrorMessage = "lütfen bir rol ismi giriniz")]
        public string RoleName{ get; set; }
       

    }
}