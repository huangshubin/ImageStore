using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageClient.Http
{
    public class HttpRequestData
    {
        public HttpRequestData()
        {
            DataType = HttpRequestDataType.String;
        }
        public string key { get; set; }
        public string value { get; set; }
        public HttpRequestDataType DataType { get; set; }
    }
}
