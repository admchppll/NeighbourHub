using Community.Models;
using Newtonsoft.Json;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;

namespace Community.Helpers
{
    public class Postcode
    {
        public string postcode;
        public double longitude;
        public double latitude;

        private const string URL = "http://api.postcodes.io/postcodes/";

        /// <summary>
        /// Constructor that will also retrieve long/lat of a given postcode
        /// </summary>
        /// <param name="code">Postcode</param>
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

        public static bool PostcodeIsValid(string code)
        {
            string url = URL + code + "/validate";

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";

            var getResponse = (HttpWebResponse)getRequest.GetResponse();
            Stream newStream = getResponse.GetResponseStream();
            StreamReader sr = new StreamReader(newStream);
            var result = sr.ReadToEnd();
            var postcodeInfo = JsonConvert.DeserializeObject<PostcodeValidateResult>(result);

            if (postcodeInfo.Status == 200 && postcodeInfo.Result == true)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public static DbGeography CreateGeographyPoint(double latitude, double longitude)
        {
            var text = string.Format(CultureInfo.InvariantCulture.NumberFormat, "POINT({0} {1})", longitude, latitude);
            // 4326 is the coordinate system
            return DbGeography.PointFromText(text, 4326);
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

    public class PostcodeValidateResult {
        [JsonProperty("status")]
        public int Status;
        [JsonProperty("result")]
        public bool Result;
        [JsonProperty("error")]
        public string Error;
    }

    public class AddressHelper {
        public static bool AddressExists(string userID)
        {
            CommunityEntities db = new CommunityEntities();
            var exists = db.Addresses
                    .Where(p => p.UserID == userID)
                    .Any();
            return exists;   
        }

        public static void SetDefault(int addressID) {
            CommunityEntities db = new CommunityEntities();
            var address = db.Addresses.Find(addressID);

            var addresses = db.Addresses.Where(p => p.UserID == address.UserID).ToList();

            foreach (var a in addresses) {
                a.Default = (a.ID == addressID) ? true : false;
            }

            db.SaveChanges();
        }
    }
}
