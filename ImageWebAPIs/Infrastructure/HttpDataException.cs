using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ImageWebAPIs.Infrastructure
{
    public class HttpDataException:Exception
    {
        public HttpDataException(HttpStatusCode code,string message)
            :base(message)
        {
            ResponseStatus = code;
        }

        public HttpStatusCode ResponseStatus { get; set; }
       
    }
}