using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Web.Ultilities
{
    public class ApiHelper<T> where T : class
    {
        //sss
        private static readonly HttpClient httpClient = new HttpClient();

        public static T HttpGetAsync(string apiUrl, string method = "GET")
        {
            //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            //httpWebRequest.Method = method;
            //var response = httpWebRequest.GetResponse();
            //{
            //    string responseData;
            //    Stream responseStream = response.GetResponseStream();
            //    try
            //    {
            //        using (StreamReader sr = new StreamReader(responseStream))
            //        {
            //            responseData = sr.ReadToEnd();
            //        }
            //    }
            //    finally
            //    {
            //        ((IDisposable)responseStream).Dispose();
            //    }
            //    return JsonConvert.DeserializeObject<T>(responseData);

            //}
            using (var response = httpClient.GetAsync(apiUrl).Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = response.Content.ReadAsStreamAsync().Result)
                    using (var streamReader = new StreamReader(responseStream))
                    {
                        string responseData = streamReader.ReadToEnd();
                        return JsonConvert.DeserializeObject<T>(responseData);
                    }
                }
                else
                {
                    throw new Exception($"Failed to get response, status code: {response.StatusCode}");
                }
            }
        }

        public static T HttpPostAsync(string apiUrl, object model)
        {
            //string result = string.Empty;
            //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            //httpWebRequest.ContentType = "application/json";
            //httpWebRequest.Method = "POST";
            //using (var streamWrite = new StreamWriter(httpWebRequest.GetRequestStream()))
            //{
            //    var json = JsonConvert.SerializeObject(model);
            //    streamWrite.Write(json);
            //}

            //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //{
            //    result = streamReader.ReadToEnd();
            //}
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = httpClient.PostAsync(apiUrl, content).Result;
            response.EnsureSuccessStatusCode();

            string responseData = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(responseData);
         
        }
    }
}