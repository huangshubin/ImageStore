using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ImageClient.Http
{
  public static  class WebRequestExtensions
    {

        public static async Task<WebResponse> GetApiResponseAsync(this WebRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            try
            {
                var response = await request.GetResponseAsync();
                return response;
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    throw;
                }

                return e.Response;
            }
        }

    }
}
