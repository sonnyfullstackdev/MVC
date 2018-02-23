using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectName.Models.Requests.Tools
{
    public class ErrorLog
    {
        public int ID {get ; set; }	
        public string ErrorFunction { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorStackTrace { get; set; }
        public string CreatedBy { get; set; }
    }
}


