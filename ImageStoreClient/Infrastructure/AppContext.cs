using ImageClient.Http;
using ImageClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageClient.Infrastructure
{
    public class AppContext
    {

        private static readonly AppContext _context;
        static AppContext() { _context = new AppContext(); }
        private AppContext() {
            AuthToken = new AuthToken();
        }

        public static AppContext Current { get { return _context; } }

        public AuthToken AuthToken { get; set; }

        public ApplicationViewModel App { get; set; }
    }
}
