using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ink_recognizer
{
    public class InkRecognizerRequest
    {
        public class Strokes
        {
            public int id { get; set; }
            public int points { get; set; }
        }

        public string language { get; set; }
        public int version { get; set; }
        public Strokes[] strokes { get; set; } = new Strokes[] { };
    }

    public class InkRecognizerManager
    {
        private readonly string endpoint = "https://ink-mug.cognitiveservices.azure.com/";
        private readonly string apiKey = "e4af79f1228b43a3b67461906f5a9129";
        private readonly string inkRecognitionUrl = "/inkrecognizer/v1.0-preview/recognize";


        public async Task<string> Request(InkRecognizerRequest request)
        {
            RestClient client = new RestClient(endpoint);
            RestRequest restRequest = new RestRequest(inkRecognitionUrl, Method.PUT);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddJsonBody(request);

            var respone = await client.ExecuteTaskAsync<string>(restRequest);
            return respone.Data;
        }
    }
}
