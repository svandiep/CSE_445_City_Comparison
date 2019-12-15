using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI.DataVisualization.Charting;
using System.Xml.Linq;

/* Homework 5 Scott VanDiepenbos
 * City Comparison application prototype
 * Application makes use of multiple APIS
 * 
 * Application uses Object Caching and Cookies to store state information
 * 
 * ASU Webstrar server being used has been very intermittent causing data to
 * not always be available.
 * 
 * */

namespace Hwk5
{
    public partial class CityComparison : System.Web.UI.Page
    {
        string AirKey = " "; //Air Quality Index API key
        string ZipKey = " "; //Zip code API key

        public string HomeValue1, HomeValue2;
        public string CrimeRate1, CrimeRate2;
        public string Walk1, Walk2;
        public int Air1, Air2;
        bool displayCity1 = false;
        bool displayCity2 = false;
        bool displayAir = true;
        bool displayHomeValues = true;
        bool displayCrimeRate = true;
        bool displayWalk = true;
        
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        protected void CompareButton_Click(object sender, EventArgs e)
        {
            ErrorLabel1.Text = "";
            ErrorLabel2.Text = "";
            ErrorAir.Text = "";
            ErrorWalk.Text = "";

            HttpCookie cookie = new HttpCookie("search");
            cookie["City1"] = CityInput1.Text;
            cookie["City2"] = CityInput2.Text;
            cookie["State1"] = StateInput1.Text;
            cookie["State2"] = StateInput2.Text;
            cookie["Zip1"] = ZipCodeInput1.Text;
            cookie["Zip2"] = ZipCodeInput2.Text;
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie);

            displayAir = true;
            displayHomeValues = true;
            displayCrimeRate = true;
            displayWalk = true;
            displayCity1 = false;
            displayCity2 = false;
            HomeValue();
            CrimeRate();
            Walkability();
            AirQuality();

            if (displayCrimeRate)
            {
                DisplayCrimeRate();
            }
            if (displayHomeValues)
            {
                DisplayHomeValue();
            }
            if (displayWalk)
            {
                DisplayWalkability();
            }
            if (displayAir)
            {
                DisplayAirQuality();
            }
            string[] charts = new string[14] { HomeValue1, HomeValue2, CrimeRate1, CrimeRate2, Air1.ToString(), Air2.ToString(), Walk1, Walk2,
            CityInput1.Text, CityInput2.Text, StateInput1.Text, StateInput2.Text, ZipCodeInput1.Text, ZipCodeInput2.Text};
            Cache.Add("charts" ,charts , null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);
            
            Cache.Add("bool1", displayCity1, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);
            Cache.Add("bool2", displayCity2, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);
        }

        private void DisplayAirQuality()
        {
            Series series = Chart4.Series["Series1"];
            if (displayCity1)
            {
                series.Points.AddXY(CityInput1.Text, Air1);
            }
            else
            {
                series.Points.AddXY(ZipCodeInput1.Text, Air1);
            }
            if (displayCity2)
            {
                series.Points.AddXY(CityInput2.Text, Air2);
            }
            else
            {
                series.Points.AddXY(ZipCodeInput2.Text, Air2);
            }
        }

        private void AirQuality()
        {
            if (ZipCodeInput1.Text == "")
            {
                try
                {
                    Air1 = GetCityAir(CityInput1.Text, StateInput1.Text);
                    displayCity1 = true;
                }
                catch (Exception)
                {
                    ErrorAir.Text = "Data Not Available or server down";
                    displayAir = false;
                }
            }
            else
            {
                try
                {
                    Air1 = GetZipAir(ZipCodeInput1.Text);
                }
                catch (Exception)
                {
                    ErrorAir.Text = "Data Not Available or server down";
                    displayAir = false;
                }
            }
            if (ZipCodeInput2.Text == "")
            {
                try
                {
                    Air2 = GetCityAir(CityInput2.Text, StateInput2.Text);
                    displayCity2 = true;
                }
                catch (Exception)
                {
                    ErrorAir.Text = "Data Not Available or server down";
                    displayAir = false;
                }
            }
            else
            {
                try
                {
                    Air2 = GetZipAir(ZipCodeInput2.Text);
                }
                catch (Exception)
                {
                    ErrorAir.Text = "Data Not Available or server down";
                    displayAir = false;
                }
            }
        }

        private void Walkability()
        {
            if (ZipCodeInput1.Text == "")
            {
                try
                {
                    Walk1 = GetCityWalk(CityInput1.Text, StateInput1.Text);
                    displayCity1 = true;
                }
                catch (Exception)
                {
                    ErrorWalk.Text = "Data Not Available or server down";
                    displayWalk = false;
                }
            }
            else
            {
                try
                {
                    Walk1 = GetZipWalk(ZipCodeInput1.Text);
                }
                catch (Exception)
                {
                    ErrorWalk.Text = "Data Not Available or server down";
                    displayWalk = false;
                }
            }
            if (ZipCodeInput2.Text == "")
            {
                try
                {
                    Walk2 = GetCityWalk(CityInput2.Text, StateInput2.Text);
                    displayCity2 = true;
                }
                catch (Exception)
                {
                    ErrorWalk.Text = "Data Not Available or server down";
                    displayWalk = false;
                }
            }
            else
            {
                try
                {
                    Walk2 = GetZipWalk(ZipCodeInput2.Text);
                }
                catch (Exception)
                {
                    ErrorWalk.Text = "Data Not Available or server down";
                    displayWalk = false;
                }
            }
        }

        private void CrimeRate()
        {
            if (ZipCodeInput1.Text == "")
            {
                try
                {
                    CrimeRate1 = GetCityCrime(CityInput1.Text, StateInput1.Text);
                    displayCity1 = true;
                }
                catch (Exception)
                {
                    ErrorCrime.Text = "Data Not Available or server down";
                    displayCrimeRate = false;
                }
            }
            else
            {
                try
                {
                    CrimeRate1 = GetZipCrime(ZipCodeInput1.Text);
                }
                catch (Exception)
                {
                    ErrorCrime.Text = "Data Not Available or server down";
                    displayCrimeRate = false;
                }
            }
            if (ZipCodeInput2.Text == "")
            {
                try
                {
                    CrimeRate2 = GetCityCrime(CityInput2.Text, StateInput2.Text);
                    displayCity2 = true;
                }
                catch (Exception)
                {
                    ErrorCrime.Text = "Data Not Available or server down";
                    displayCrimeRate = false;
                }
            }
            else
            {
                try
                {
                    CrimeRate2 = GetZipCrime(ZipCodeInput2.Text);
                }
                catch (Exception)
                {
                    ErrorLabel2.Text = "Data Not Available or server down";
                    displayCrimeRate = false;
                }
            }
        }

        private void HomeValue()
        {
            if (ZipCodeInput1.Text == "")
            {
                try
                {
                    HomeValue1 = GetCityValue(CityInput1.Text, StateInput1.Text);
                    displayCity1 = true;
                }
                catch (Exception)
                {
                    ErrorHome.Text = "Data Not Available or server down";
                    displayHomeValues = false;
                }
            }
            else
            {
                try
                {
                    HomeValue1 = GetZipValue(ZipCodeInput1.Text);
                }
                catch (Exception)
                {
                    ErrorHome.Text = "Data Not Available or server down";
                    displayHomeValues = false;
                }
            }
            if (ZipCodeInput2.Text == "")
            {
                try
                {
                    HomeValue2 = GetCityValue(CityInput2.Text, StateInput2.Text);
                    displayCity2 = true;
                }
                catch (Exception)
                {
                    ErrorHome.Text = "Data Not Available or server down";
                    displayHomeValues = false;
                }
            }
            else
            {
                try
                {
                    HomeValue2 = GetZipValue(ZipCodeInput2.Text);
                }
                catch (Exception)
                {
                    ErrorHome.Text = "Data Not Available or server down";
                    displayHomeValues = false;
                }
            }
        }

        private void DisplayWalkability()
        {
            Series series = Chart3.Series["Series1"];
            if (displayCity1)
            {
                series.Points.AddXY(CityInput1.Text, Walk1);
            }
            else
            {
                series.Points.AddXY(ZipCodeInput1.Text, Walk1);
            }
            if (displayCity2)
            {
                series.Points.AddXY(CityInput2.Text, Walk2);
            }
            else
            {
                series.Points.AddXY(ZipCodeInput2.Text, Walk2);
            }
        }

        private void DisplayHomeValue()
        {            
            Series series = Chart1.Series["Series1"];
            if(displayCity1)
            {               
                series.Points.AddXY(CityInput1.Text, HomeValue1);                
            }
            else
            {
                series.Points.AddXY(ZipCodeInput1.Text, HomeValue1);
            }
            if(displayCity2)
            {
                series.Points.AddXY(CityInput2.Text, HomeValue2);
            }
            else
            {
                series.Points.AddXY(ZipCodeInput2.Text, HomeValue2);
            }
        }

        private void DisplayCrimeRate()
        {
            Series series = Chart2.Series["Series1"];
            if (displayCity1)
            {
                series.Points.AddXY(CityInput1.Text, CrimeRate1);
            }
            else
            {
                series.Points.AddXY(ZipCodeInput1.Text, CrimeRate1);
            }
            if (displayCity2)
            {
                series.Points.AddXY(CityInput2.Text, CrimeRate2);
            }
            else
            {
                series.Points.AddXY(ZipCodeInput2.Text, CrimeRate2);
            }
        }

        private string GetZipWalk(string zip)
        {
            var client = new WebClient();//create new webclient
            var response = client.DownloadString("http://webstrar10.fulton.asu.edu/page2/Service1.svc/WalkZip?zip=" + zip);
            return response;
        }

        private string GetCityWalk(string city, string state)
        {
            var client = new WebClient();//create new webclient
            var response = client.DownloadString("http://webstrar10.fulton.asu.edu/page2/Service1.svc/WalkCity?city=" + city + "&state=" + state);
            return response;//return string from webservice
        }

        private string GetZipCrime(string zip)
        {
            string url = ("http://webstrar10.fulton.asu.edu/page4/Service1.svc/crimeZip?zipcode=" + zip);
            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(url); // get City from zip code
            WebResponse response2 = request2.GetResponse();
            Stream dataStream2 = response2.GetResponseStream();
            StreamReader sreader2 = new StreamReader(dataStream2);
            XDocument xmlDoc = new XDocument();
            xmlDoc = XDocument.Parse(sreader2.ReadToEnd());
            response2.Close();
            string rate = xmlDoc.Root.Value;
            return rate;//return string from webservice
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            CityInput1.Text = "";
            CityInput2.Text = "";
            StateInput1.Text = "";
            StateInput2.Text = "";
            ZipCodeInput2.Text = "";
            ZipCodeInput1.Text = "";
            ErrorLabel1.Text = "";
            ErrorLabel2.Text = "";
        }

        private string GetCityCrime(string city, string state)
        {            
            string url = ("http://webstrar10.fulton.asu.edu/page4/Service1.svc/crimeCity?city=" + city + "&state=" + state);
            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(url); // get City from zip code
            WebResponse response2 = request2.GetResponse();
            Stream dataStream2 = response2.GetResponseStream();
            StreamReader sreader2 = new StreamReader(dataStream2);
            XDocument xmlDoc = new XDocument();
            xmlDoc = XDocument.Parse(sreader2.ReadToEnd());
            response2.Close();
            string rate = xmlDoc.Root.Value;
            return rate;//return string from webservice            
        }

        private string GetZipValue(string zip)
        {
            var client = new WebClient();//create new webclient
            var response = client.DownloadString("http://webstrar10.fulton.asu.edu/page3/Service1.svc/ValueByZip?zip=" + zip);
            return response;//return string from webservice
        }

        private string GetCityValue(string city, string state)
        {
            var client = new WebClient();//create new webclient
            var response = client.DownloadString("http://webstrar10.fulton.asu.edu/page3/Service1.svc/CityAverage?city=" + city + "&state=" + state);
            return response;//return string from webservice
        }

        public int GetZipAir(string zip)
        {
            string url = "http://api.zippopotam.us/us/" + zip;
            Console.WriteLine(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url); // get City from zip code
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            zipObject zipobject = JsonConvert.DeserializeObject<zipObject>(responsereader);
            int AQ = GetAirQuality(zipobject.places[0].placename); //Get a value for air quality
            
            return AQ;
        }

        public int GetCityAir(string city, string state)
        {
            string url = "http://api.zippopotam.us/us/" + state + "/" + city;
            Console.WriteLine(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url); // get City from zip code
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            cityObject cityobject = JsonConvert.DeserializeObject<cityObject>(responsereader);

            string zip = cityobject.places[0].postcode;
            int value = GetZipAir(zip);
            return value;
        }

        private int GetAirQuality(string city)
        {
            string url = "https://api.waqi.info/feed/" + city + "/?token=" + AirKey;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url); // get City from zip code
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            TestObject test = JsonConvert.DeserializeObject<TestObject>(responsereader);
            if (test.status == "ok") // if Air quality index is available for given place
            {
                airObject airobject = JsonConvert.DeserializeObject<airObject>(responsereader);
                //ErrorLabel2.Text = airobject.data.aqi.ToString();
                return airobject.data.aqi;
            }
            else
            {
                return 0;
            }
        }

        internal class airObject
        {
            public string status { get; set; }

            [JsonProperty(PropertyName = "data")]
            public Data data { get; set; }
        }

        public class Data
        {
            public int aqi { get; set; }
            public int idx { get; set; }
        }

        protected void MostRecentButton_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["search"];
            if (cookie != null)
            {
                CityInput1.Text = cookie["City1"];
                CityInput2.Text = cookie["City2"];
                StateInput1.Text = cookie["State1"];
                StateInput2.Text = cookie["State2"];
                ZipCodeInput1.Text = cookie["Zip1"];
                ZipCodeInput2.Text = cookie["Zip2"];
            }
        }

        protected void RecentDataButton_Click(object sender, EventArgs e)
        {
            if (Cache["charts"] == null)
            {
                ErrorLabel2.Text = "Nothing cached";
            }
            else
            {
                string[] charts = (string[])Cache["charts"];
                HomeValue1 = charts[0];
                HomeValue2 = charts[1];
                CrimeRate1 = charts[2];
                CrimeRate2 = charts[3];
                Air1 = Convert.ToInt32(charts[4]);
                Air2 = Convert.ToInt32(charts[5]);
                Walk1 = charts[6];
                Walk2 = charts[7];
                CityInput1.Text = charts[8];
                CityInput2.Text = charts[9];
                StateInput1.Text = charts[10];
                StateInput2.Text = charts[11];
                ZipCodeInput1.Text = charts[12];
                ZipCodeInput2.Text = charts[13];
                displayCity1 = (bool)Cache["bool1"];                
                displayCity2 = (bool)Cache["bool2"];
                DisplayHomeValue();
                DisplayCrimeRate();
                DisplayWalkability();
                DisplayAirQuality();

            }
        }

        internal class TestObject
        {
            public string status { get; set; }
        }

        public class zipObject
        {

            [JsonProperty(PropertyName = "post code")]
            public string postcode { get; set; }

            public string country { get; set; }

            [JsonProperty(PropertyName = "country abbreviation")]
            public string countryabbreviation { get; set; }
            public List<Places> places { get; set; }
        }

        public class Places
        {
            [JsonProperty(PropertyName = "place name")]
            public string placename { get; set; }

            public decimal longitude { get; set; }
            public string state { get; set; }

            [JsonProperty(PropertyName = "state abbreviation")]
            public string stateabbreviation { get; set; }

            public decimal latitude { get; set; }
        }

        internal class cityObject
        {
            [JsonProperty(PropertyName = "country abbreviation")]
            public string countryabbreviation { get; set; }

            public List<Places2> places { get; set; }
        }

        public class Places2
        {
            [JsonProperty(PropertyName = "place name")]
            public string placename { get; set; }

            [JsonProperty(PropertyName = "longitude")]
            public decimal longitude { get; set; }

            [JsonProperty(PropertyName = "post code")]
            public string postcode { get; set; }

            [JsonProperty(PropertyName = "latitude")]
            public decimal latitude { get; set; }
        }

        static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}