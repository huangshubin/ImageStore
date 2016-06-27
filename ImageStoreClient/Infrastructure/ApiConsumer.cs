using ImageClient.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageClient.Infrastructure
{
    public class ApiConsumer
    {

        public async Task<ResponseResult> LoginAsync(string userName, string password)
        {
            var loginUrl = ImageWebAPIs.Login;

            var postData = new List<HttpRequestData>();
            postData.Add(new HttpRequestData() { key = "username", value = userName });
            postData.Add(new HttpRequestData() { key = "password", value = password });
            postData.Add(new HttpRequestData() { key = "grant_type", value = "password" });

            var result = await WebAPIClient.PostAsync(loginUrl, postData, FormPostType.FormUrlEncoded);

            return result;
        }

        public async Task<ResponseResult> SendImageAsync(string imagePath, bool isStore)
        {
            var url = ImageWebAPIs.SendImage;

            var postData = new List<HttpRequestData>();
            postData.Add(new HttpRequestData() { key = "image", value = imagePath, DataType = HttpRequestDataType.File });
            postData.Add(new HttpRequestData() { key = "store", value = isStore ? "1" : "0" });

            var token = AppContext.Current.AuthToken;

            var result = await WebAPIClient.PostAsync(url, postData, FormPostType.MultipartFormData, token);

            return result;
        }
        public async Task<ResponseResult> LogoutSync()
        {
            var url = ImageWebAPIs.Logout;
            if (AppContext.Current.AuthToken == null)
                return ResponseResult.Empty;

            var result = await WebAPIClient.GetAsync(url, AppContext.Current.AuthToken);
            return result;
        }

    }
}
