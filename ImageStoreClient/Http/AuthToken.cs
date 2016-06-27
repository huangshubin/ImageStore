using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageClient.Http
{
    public class AuthToken
    {
        public string Key { get; set; }
        public int ExpiresIn{ get; set; }
        public string TokenType { get; set; }
    }
}
