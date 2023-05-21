using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Web.UI.HtmlControls;

namespace HW5
{
    public partial class Default : System.Web.UI.Page
    {
        public static string ziplookup = new WebClient().DownloadString("https://github.com/millbj92/US-Zip-Codes-JSON/blob/master/USCities.json?raw=true");

        
        private static string state;
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["mycookies"];
            if(!IsPostBack)
            {
                if (cookie != null)
                {
                    textbox1.Text = cookie["zipcode"];
                }
            }
                
        }

        protected void button1_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(textbox1.Text)) //check if textbox is empty
            {
                
                label1.Visible = true;
                label1.Text = "Invalid zipcode";
                label2.Visible = false;
                TextBox4.Visible = false;
                storeButton.Visible = false;
                return;
            } else
            {
               

                string zipcode = textbox1.Text;

                bool iszip = int.TryParse(zipcode, out int temp);

                if(temp <= 0 || temp >= 99999 || iszip == false)
                {
                    label1.Visible = true;
                    label1.Text = "Invalid zipcode";
                    return;
                }

                if(zipcode.Contains(zipcode))
                {
                    label1.Visible = true;
                    label1.Text = "Location search";
                    label2.Visible = true;
                    label2.Text = "Based on your area search, enter any location name";
                    TextBox4.Visible = true;
                    TextBox4.Text = "";
                    storeButton.Visible = true;
                } else
                {
                    label1.Visible = true;
                    label1.Text = "Invalid zipcode";
                    return;
                }
                
                HttpCookie cookie = new HttpCookie("mycookies");
                cookie["zipcode"] = textbox1.Text;
                cookie.Expires = DateTime.Now.AddMinutes(1);
                Response.Cookies.Add(cookie);
                
                

                label3.Visible = true;
                label3.Text = "Crime data";
                label4.Visible = true;
                label4.Text = "Below is crime data of the area in the past 10 years";
                
                string url = @"http://localhost:64395/Service1.svc/crimedata?x=" + zipcode;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                request.ContentType = "application/json";
                Stream stream = response.GetResponseStream();
                StreamReader sreader = new StreamReader(stream);
                string responsereader = sreader.ReadToEnd();
                response.Close();

                DataC data = new DataC();
                data = JsonConvert.DeserializeObject<DataC>(responsereader);

                string crimeInfo = "";
                if (data == null)
                {
                    label5.Text = "Invalid zipcode";
                    label5.Visible = true;
                }
                else
                {
                    label5.Text = "From " + data.fromDate + " to " + data.toDate + ", in the state of " + data.state + ":";
                    label6.Text = data.violent_crime + " crimes was a violent crime";
                    label7.Text =  data.homicide + " crimes was a homicide";
                    label8.Text = data.robbery + " crimes was a robbery";
                    label9.Text = data.aggAssault + " crimes was aggravated assault";
                    label10.Text =  data.property_crime + " crimes was a property crime";
                    label12.Text =  data.larceny + " crimes was larceny";
                    label13.Text = data.motor_theft + " crimes was motor vehicle theft";
                    label14.Text =  data.arson + " crimes was arson";

                    label5.Visible = true;
                    label6.Visible = true;
                    label7.Visible = true;
                    label8.Visible = true;
                    label9.Visible = true;
                    label10.Visible = true;
                    label12.Visible = true;
                    label13.Visible = true;
                    label14.Visible = true;
                }

                state = data.state;
                
                label15.Text = state + " News";
                label15.Visible = true;
                
                Button2.Visible = true;
                
            }

            
        }

        protected void storeButton_Click(object sender, EventArgs e)
        {
            string zipcode = textbox1.Text;
            string storeName = TextBox4.Text;
            if (String.IsNullOrEmpty(storeName))
            {
                textbox3.Text = "Invalid location name";
                textbox3.Visible = true;
                return;
            } else
            {
                string url = @"http://localhost:64395/Service1.svc/findNearestStore?x=" + zipcode + "&y=" + storeName;
                StoreObject store = new StoreObject();
                List<StoreObject> sList = new List<StoreObject>();


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                WebResponse response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();
                StreamReader sreader = new StreamReader(dataStream);
                string responsereader = sreader.ReadToEnd();
                response.Close();

                string storeList = "";

                sList = JsonConvert.DeserializeObject<List<StoreObject>>(responsereader);

                if (sList == null || sList.Count == 0)
                {
                    textbox3.Visible = true;
                    textbox3.Text = "Invalid zipcode";
                    return;
                }

                for (int i = 0; i < sList.Count; i++)
                {
                    storeList += sList[i].name + "\n\t\t";
                    storeList += sList[i].address + "\n\n";
                }

                textbox3.Text = storeList;
                textbox3.Visible = true;

                //location find successful
                
                
            }
        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            label15.Text = state + " News";
            label15.Visible = true;
            string sort = "popularity";

            string url1 = @"http://localhost:64395/Service1.svc/NewsFocus?x=" + state + "&y=" + sort + "&z=All";
            //url = @"http://webstrar47.fulton.asu.edu/page1/Service1.svc/NewsFocus?x=" + topic + "&y=" + sort + "&z=" + from;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url1);
            WebResponse response = request.GetResponse();
            request.ContentType = "application/json";
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();

            NewsObject obj = JsonConvert.DeserializeObject<NewsObject>(responsereader);

            int maxArticles = obj.articles.Count;
            for (int i = 0; i < maxArticles; i++)
            {
                HtmlGenericControl div = new HtmlGenericControl("div");
                HtmlGenericControl article = new HtmlGenericControl("a");
                HtmlGenericControl link = new HtmlGenericControl("p");
                HtmlGenericControl source = new HtmlGenericControl("p");
                HtmlGenericControl image = new HtmlGenericControl("img");
                HtmlGenericControl des = new HtmlGenericControl("p");
                image.Attributes.Add("src", obj.articles[i].urlToImage);
                image.Attributes.Add("style", "float:left;width:50px;height:50px;");
                article.InnerText = obj.articles[i].title;
                article.Attributes.Add("href", obj.articles[i].url);
                link.Controls.Add(article);
                source.InnerHtml = obj.articles[i].source.name;
                des.InnerText = obj.articles[i].description;

                div.Controls.Add(image);
                div.Controls.Add(source);
                div.Controls.Add(link);
                div.Controls.Add(des);
                div.Attributes.Add("style", "background: lightgray;");

                outDiv.Controls.Add(div);
            }
        }
    }
}