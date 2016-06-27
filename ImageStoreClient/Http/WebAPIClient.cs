using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ImageClient.Http
{
    class WebAPIClient
    {
        public static async Task<ResponseResult> PostAsync(string url, IList<HttpRequestData> postData, FormPostType postType, AuthToken authToken = null)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = HttpMethodType.POST;

            if (authToken != null)
            {
                req.Headers.Add("authorization", $"Bearer {authToken.Key}");
            }

            var dataChunks = new List<byte[]>();
            if (FormPostType.FormUrlEncoded == postType)
            {

                req.ContentType = "application/x-www-form-urlencoded";
                var strData = "";
                foreach (var cur in postData)
                {
                    strData += $"{cur.key}={cur.value}&";
                }
                strData = strData.TrimEnd('&');
                dataChunks.Add(Encoding.ASCII.GetBytes(strData));
            }
            else
            {
                string boundary = "----ImageWebClient" + DateTime.Now.Ticks.ToString("x");
                var newLine = Environment.NewLine;

                req.ContentType = $"multipart/form-data; boundary={boundary}";
                req.KeepAlive = true;

                byte[] boundarybytes = Encoding.UTF8.GetBytes($"{newLine}--" + boundary + newLine);

                string formItem;
                foreach (var cur in postData)
                {
                    dataChunks.Add(boundarybytes);

                    switch (cur.DataType)
                    {
                        case HttpRequestDataType.String:
                            formItem = $"Content-Disposition: form-data; name=\"{cur.key}\"{newLine}{newLine}{cur.value}";
                            dataChunks.Add(Encoding.UTF8.GetBytes(formItem));
                            break;
                        case HttpRequestDataType.File:
                            formItem = $"Content-Disposition: form-data; name=\"{cur.key}\"; filename=\"{Path.GetFileName(cur.value)}\"{newLine}Content-Type: {GetContentType(cur.value)}{newLine}{newLine}";
                            dataChunks.Add(Encoding.UTF8.GetBytes(formItem));

                            using (var fileStream = new FileStream(cur.value, FileMode.Open, FileAccess.Read))
                            {
                                byte[] buffer = new byte[4096];
                                int bytesRead = 0;
                                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                                {
                                    if (bytesRead != buffer.Length)
                                    {
                                        var temp = new byte[bytesRead];
                                        Buffer.BlockCopy(buffer, 0, temp, 0, bytesRead);
                                        dataChunks.Add(temp);
                                    }
                                    else
                                        dataChunks.Add(buffer);

                                    buffer = new byte[4096];
                                }
                            }

                            break;
                        default:
                            break;
                    }

                }
                dataChunks.Add(Encoding.UTF8.GetBytes($"{newLine}--{boundary}--{newLine}"));
            }

            var result = new ResponseResult();
            using (Stream os = await req.GetRequestStreamAsync())
            {
                foreach (var chunk in dataChunks)
                    os.Write(chunk, 0, chunk.Length);

                using (var resp = (HttpWebResponse)await req.GetApiResponseAsync())
                {

                    result.StatusCode = resp.StatusCode;

                    using (var sr = new StreamReader(resp.GetResponseStream()))
                    {
                        var content = await sr.ReadToEndAsync();
                        var jobject = JObject.Parse(content);
                        result.JContent = jobject;
                    }
                }
            }

            return result;

        }
        public static async Task<ResponseResult> GetAsync(string url, AuthToken token = null)
        {

            var req = WebRequest.Create(url);
            if (token != null)
            {
                req.Headers.Add("authorization", $"Bearer {token.Key}");
            }
            using (var resp = (HttpWebResponse)await req.GetApiResponseAsync())
            {
                var result = new ResponseResult();
                result.StatusCode = resp.StatusCode;

                using (var sr = new StreamReader(resp.GetResponseStream()))
                {
                    var content = await sr.ReadToEndAsync();
                    var d = JObject.Parse(content);
                    result.JContent = d;
                }
                return result;
            }
        }
        public static ResponseResult Get(string url, AuthToken token = null)
        {

            var req = WebRequest.Create(url);
            if (token != null)
            {
                req.Headers.Add("authorization", $"Bearer {token.Key}");
            }
            using (var resp = (HttpWebResponse)req.GetResponse())
            {
                var result = new ResponseResult();
                result.StatusCode = resp.StatusCode;

                using (var sr = new StreamReader(resp.GetResponseStream()))
                {
                    var content = sr.ReadToEnd();
                    var d = JObject.Parse(content);
                    result.JContent = d;
                }
                return result;
            }
        }
        private static string GetContentType(string fileName)
        {
            var contentType = "image/jpeg";
            switch (Path.GetExtension(fileName))
            {
                case ".gif":
                    contentType = "image/gif";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                default:
                    contentType = "image/jpeg";
                    break;
            }
            return contentType;
        }

    }
}
