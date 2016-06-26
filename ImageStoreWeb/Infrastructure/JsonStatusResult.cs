using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ImageStoreWeb.Infrastructure
{
    public class JsonStatusResult : IHttpActionResult
    {
        object _content;
        HttpStatusCode _code;
        HttpRequestMessage _request;
        public JsonStatusResult(HttpRequestMessage request, HttpStatusCode code, object content)
        {
            _request = request;
            _content = content;
            _code = code;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = _request.CreateResponse(_code, _content);

            return Task.FromResult(response);
        }

    }
}