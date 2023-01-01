using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestW2uiWeb.Model
{
    

    public class BaseResponseModel
    {
        public string message { get; set; }
        public Errors errors { get; set; }
        public int status_code { get; set; }
    }
}
