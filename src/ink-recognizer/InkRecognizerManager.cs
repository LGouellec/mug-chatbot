using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;

namespace ink_recognizer
{
    public class InkRecognizerRequest
    {
        public class Strokes
        {
            public int id { get; set; }
            public string points { get; set; }
        }

        public string language { get; set; }
        public int version { get; set; }
        public List<Strokes> strokes { get; set; } = new List<Strokes> { };

        internal void AddStroke(int v, Stroke c)
        {
            string points = "";
            foreach(var point in c.StylusPoints)
            {
                points += $"{point.X},{point.Y},";
            }
            points = points.Remove(points.Length - 1, 1);
            strokes.Add(new Strokes { id = v, points = points });
        }
    }

    public class InkRecognizerManager
    {
        private readonly string endpoint = "https://ink-mug.cognitiveservices.azure.com/";
        private readonly string apiKey = "e4af79f1228b43a3b67461906f5a9129";
        private readonly string inkRecognitionUrl = "/inkrecognizer/v1.0-preview/recognize";


        public string Request(InkRecognizerRequest request)
        {
            RestClient client = new RestClient(endpoint);
            RestRequest restRequest = new RestRequest(inkRecognitionUrl, Method.PUT);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Ocp-Apim-Subscription-Key", apiKey);
            restRequest.AddJsonBody(request);

            var response = client.Execute(restRequest);
            if (response.StatusCode == HttpStatusCode.OK)
                return AnalyseResult(JsonConvert.DeserializeObject<RootObject>(response.Content));
            else
                return $"Error : {response.StatusCode} - {response.Content}";
        }

        private string AnalyseResult(RootObject response)
        {
            var units = response.recognitionUnits.FirstOrDefault(r => r.category.Equals("line"));
            if (units != null)
                return units.recognizedText;
            else
            {
                var inkWordUnits = response.recognitionUnits.Where(r => r.category.Equals("inkWord")).OrderBy(r => r.id);
                string result = "";
                foreach (var i in inkWordUnits)
                    result += $"{i.recognizedText} ";
                return result;
            }
        }
    }
}
