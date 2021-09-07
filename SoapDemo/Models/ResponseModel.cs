using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoapDemo.Models
{
        public class ResponseModel<T>
        {
            public T Data { get; set; }
            public int resultCode { get; set; }
            public string message { get; set; }
        }

}