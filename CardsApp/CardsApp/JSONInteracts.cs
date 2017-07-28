using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CardsApp.common;

namespace CardsApp
{
    static class JSONInteracts
    {

        internal static async Task<string> GETJson(string url, string Method ="GET", string body="")
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(Common.ApiUri + url));
            request.ContentType = "application/json";
            request.Method = Method;

            //Add the body if it applies.
            if(body.Length > 0)
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    
                    streamWriter.Write(body);
                    streamWriter.Flush();
                    streamWriter.Close();

                }
                //request.ContentLength = body.Length;
            }
            else
            {
                request.ContentLength = 0;
            }
            

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    //JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    // convert stream to string
                    StreamReader reader = new StreamReader(stream);
                    string text = reader.ReadToEnd();
                    
                    return text;



                }
            }
            //
        }

        internal static async Task<ReturnObject> JSONDecodetoReturnObject(string json)
        {
            return JsonConvert.DeserializeObject<ReturnObject>(json);
        }

        internal static async Task<TargetType> BasicRequest<TargetType>(string URL, string Method = "GET", string Body="")
        {
            return await Task.Run(async () => JsonConvert.DeserializeObject<TargetType>(await GETJson(URL, Method, Body)));
        }

    }
}
