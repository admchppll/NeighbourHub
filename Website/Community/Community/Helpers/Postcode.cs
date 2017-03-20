using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Community.Helpers
{
    public class Postcode
    {
        public string postcode;
        public double longitude;
        public double latitude;
        public static DataSet dataSet;

        private const string URL = "http://api.postcodes.io/postcodes/";

        public Postcode(string code)
        {
            postcode = code;
            string url = URL + code;

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";

            var getResponse = (HttpWebResponse)getRequest.GetResponse();
            Stream newStream = getResponse.GetResponseStream();
            StreamReader sr = new StreamReader(newStream);
            var result = sr.ReadToEnd();
            var postcodeInfo = JsonConvert.DeserializeObject<PostcodeRoot>(result);

            if (postcodeInfo.status == 200)
            {
                latitude = postcodeInfo.result.Latitude;
                longitude = postcodeInfo.result.Longitude;
            }
            else {
                latitude = 0;
                longitude = 0;
            }
        }

    }

    public class PostcodeRoot {
        public int status;
        public PostcodeResult result;
    }

    public class PostcodeResult
    {
        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }
    }
}
