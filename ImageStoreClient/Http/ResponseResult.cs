using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ImageClient.Http
{
    public class ResponseResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public JObject JContent { get; set; }

        public static ResponseResult Empty
        {
            get
            {

                return new ResponseResult()
                {
                    StatusCode = HttpStatusCode.Unused,
                    JContent = new JObject()
                };
            }
        }
    }
}
