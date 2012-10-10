using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OGDI_WaterlooCSharp_v1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // var context = new Ogdi.dcDataService(new Uri("http://ogdi.cloudapp.net/v1/dc"));
            var context = new v1Service.CityOfWaterlooOpenDataDataService(new Uri("http://CityOfWaterlooOpenData.cloudapp.net:8080/v1/CityOfWaterlooOpenData"));

            // NOTE: You can replace BankLocations with any EntitySet available
            //       See http://CityOfWaterlooOpenData.cloudapp.net
            //       for a full list.

            // Customize for Waterloo dataset

            // QUERY 1:
            var query = from locations in context.FacilitiesWaterloo20120329
                        select locations;

            // The query above translates into the following URL:
            //      http://CityOfWaterlooOpenData.cloudapp.net:8080/v1/CityOfWaterlooOpenData/FacilitiesWaterloo20120329()

            
            // QUERY 2:            
            // var query = from locations in context.FacilitiesWaterloo20120329
            //        where locations.address == "2001 UNIVERSITY AVE E"
            //        select locations;
            // The query above translates into the following URL:
            //      http://cityofwaterlooopendata.cloudapp.net:8080/v1/CityOfWaterlooOpenData/FacilitiesWaterloo20120329()?$filter=address%20eq%20'2001%20UNIVERSITY%20AVE%20E'

            // QEUERY 3:
            // var query = (from locations in context.FacilitiesWaterloo20120329
            //         select locations).Take(5);
            // The query above translates into the following URL:
            //      http://cityofwaterlooopendata.cloudapp.net:8080/v1/CityOfWaterlooOpenData/FacilitiesWaterloo20120329()?$top=5

            // Execute query
            List<v1Service.FacilitiesWaterloo20120329Item> results = query.ToList();

            // Bind the reults to the GridView.
            GridView1.DataSource = results;
            GridView1.DataBind();

            // Execute the same query, but return KML
            var wc = new System.Net.WebClient();
            var queryUrl = query.ToString() + "?format=kml";   // or &format=kml if already ?filter, etc. is provided
            TextBox1.Text = wc.DownloadString(new Uri(queryUrl));

            // Currently, the OGDI service only supports the $filter 
            // and $top WCF Data Services querystring paramaters.        

        }
    }
}