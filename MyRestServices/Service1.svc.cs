using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Web;
using System.Net;
using Newtonsoft.Json;

namespace MyRestServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public static string jsonStr = new WebClient().DownloadString("https://github.com/millbj92/US-Zip-Codes-JSON/blob/master/USCities.json?raw=true");
        
        
        public List<StoreObject> findNearestStore(string zipcode, string storeName)
        {

            List<StoreObject> list = new List<StoreObject>();

            string store = "";
            bool isZip = int.TryParse(zipcode, out int temp);

            if (temp <= 0 || temp >= 99999 || isZip == false) //validate zipcode
            {
                //return "Invalid zipcode";
                return list;
            }

            if(jsonStr.Contains(zipcode))
            {
                //convert zipcode to longitude and latitude
                string longLat = "";
                string radiusInMeters = "32186.94"; //equivlaent to 20 miles

                string url1 = "";
                url1 = @"http://api.positionstack.com/v1/forward?access_key=8b65ac1e3c186c9a776ca709c3c8c8bf&country=US&query=" + zipcode;

                RootLongLat longLati = new RootLongLat();
                Root location = new Root();


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url1);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader sreader = new StreamReader(dataStream);
                string responsereader = sreader.ReadToEnd();
                response.Close();

                longLati = JsonConvert.DeserializeObject<RootLongLat>(responsereader);
                location = JsonConvert.DeserializeObject<Root>(responsereader);
                if (longLati.data == null || longLati == null || location == null || longLati.data.Count == 0)
                {
                    //return "Invalid zipcode";
                    return list;
                }
                longLat = longLati.data[0].latitude + "," + longLati.data[0].longitude;



                url1 = @"https://dev.virtualearth.net/REST/v1/LocalSearch/?query=" + storeName
                        + "&userLocation=" + longLat + "," + radiusInMeters
                        + "&key=AgR-GoGs2VbWNIwjNidjkv_j3WHhsQkJbELAnhPACEN4iR4HQc-QGE6QEznnbTt1";

                HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url1);
                WebResponse response1 = request1.GetResponse();
                Stream dataStream1 = response1.GetResponseStream();
                StreamReader sreader1 = new StreamReader(dataStream1);
                string responsereader1 = sreader1.ReadToEnd();
                response1.Close();

                location = JsonConvert.DeserializeObject<Root>(responsereader1);
                for (int i = 0; i < location.resourceSets[0].estimatedTotal; i++)
                {
                    StoreObject storeObject = new StoreObject();
                    //store += location.resourceSets[0].resources[i].name;
                    //store = store + "\n\t" + location.resourceSets[0].resources[i].Address.formattedAddress + "\n\n";
                    storeObject.name = location.resourceSets[0].resources[i].name;
                    storeObject.address = location.resourceSets[0].resources[i].Address.formattedAddress;
                    list.Add(storeObject);
                }

                return list;
            }

            

            return null;
        }//end of findingNearestStore

        public DataC crimedata(string zipcode)
        {
            DataC data = new DataC();

            bool isZip = int.TryParse(zipcode, out int temp);

            if (temp <= 0 || temp >= 99999 || isZip == false) //validate zipcode
            {
                //return "Invalid zipcode";
                return null;
            }

            string crimeInfo = "";

            string url = "";


            //convert the latitude and longitude to the state region
            //url = @"http://api.positionstack.com/v1/reverse?access_key=8b65ac1e3c186c9a776ca709c3c8c8bf&query=" + latLng;
            url = @"http://api.positionstack.com/v1/forward?access_key=8b65ac1e3c186c9a776ca709c3c8c8bf&country=US&query=" + zipcode;
            //url = @"http://api.positionstack.com/v1/forward?access_key=8b65ac1e3c186c9a776ca709c3c8c8bf&country=US&query=85292";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();

            Location location = new Location();
            Root rootNode = new Root();
            location = JsonConvert.DeserializeObject<Location>(responsereader);
            rootNode = JsonConvert.DeserializeObject<Root>(responsereader);

            if (location == null || location.data == null || location.data.Count == 0)
            {
                //return "Invalid zipcode";
                return null;
            }
            
            string state = location.data[0].region_code;
            DateTime today = DateTime.Now;

            //gets data from the past 10 years
            int fromDate = today.Year - 10;
            int toDate = today.Year;
            data.state = location.data[0].region;
            data.fromDate = fromDate.ToString();
            data.toDate = toDate.ToString();

            string url1;
            //url1 = @"https://api.usa.gov/crime/fbi/sapi/api/estimates/states/" + state +"/" + fromDate + "/" + toDate + "?API_KEY=H9w5zF27dlfOCwRctUBA6Wza3MPAKeaku30EvXsk";
            url1 = @"https://api.usa.gov/crime/fbi/sapi/api/estimates/states/" + state + "/" + fromDate + "/" + toDate + "?API_KEY=H9w5zF27dlfOCwRctUBA6Wza3MPAKeaku30EvXsk";
            //url1 = @"https://api.usa.gov/crime/fbi/sapi/api/estimates/states/AZ/2017/2022?API_KEY=H9w5zF27dlfOCwRctUBA6Wza3MPAKeaku30EvXsk";
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url1);
            WebResponse response1 = request1.GetResponse();
            Stream dataStream1 = response1.GetResponseStream();
            StreamReader sreader1 = new StreamReader(dataStream1);
            string responsereader1 = sreader1.ReadToEnd();
            response1.Close();
            CrimeData cData = JsonConvert.DeserializeObject<CrimeData>(responsereader1);

            if(cData == null)
            {
                return data;
            }
           
            int violentCrimes = 0;
            int homieCide = 0;
            int robbery = 0;
            int aggAssault = 0;
            int propCrime = 0;
            int burg = 0;
            int larceny = 0;
            int motorTheft = 0;
            int arson = 0;

            for (int i = 0; i < cData.results.Count; i++)
            {
                violentCrimes += cData.results[i].violent_crime;
                homieCide += cData.results[i].homicide;
                robbery += cData.results[i].robbery;
                aggAssault += cData.results[i].aggravated_assault;
                propCrime += cData.results[i].property_crime;
                burg += cData.results[i].burgarly;
                larceny += cData.results[i].larceny;
                motorTheft += cData.results[i].motor_vehicle_theft;
                arson += cData.results[i].arson;
            }

            data.violent_crime = violentCrimes;
            data.homicide = homieCide;
            data.robbery = robbery;
            data.aggAssault = aggAssault;
            data.property_crime = propCrime;
            data.burgarly = burg;
            data.larceny = larceny;
            data.motor_theft = motorTheft;
            data.arson = arson;
            
            return data;
        }


        public NewsObject NewsFocus(string topic, string sortBy, string fromType)
        {
            NewsObject obj = new NewsObject();

            DateTime date = DateTime.Now;
            string fromDate = "";
            string url = "";
            string fromStr = "&fromDate=";
            fromDate = date.ToString("yyyy-MM-dd");
            if (fromType.Equals("All"))
            {
                fromStr = "";
                fromDate = "";
                //url = @"https://newsapi.org/v2/everything?q=" + topic + "%25" + "&sortBy=" + sortBy + "&apiKey=15614df1214245eb8d261da2dad4486e";
            }
            else if (fromType.Equals("Day"))
            {

                date.AddDays(-1);
            }
            else if (fromType.Equals("Week"))
            {
                date.AddDays(-7);
            }
            else if (fromType.Equals("Month"))
            {
                date.AddMonths(-1);
            }
            else if (fromType.Equals("Year"))
            {
                date.AddYears(-1);
            }
            else
            {
                return null; //something went wrong
            }

            url = @"https://newsapi.org/v2/everything?q=" + topic + "%25" + fromStr + fromDate + "&sortBy=" + sortBy + "&apiKey=15614df1214245eb8d261da2dad4486e";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();


            obj = JsonConvert.DeserializeObject<NewsObject>(responsereader);

            return obj;
        }



    }
}
