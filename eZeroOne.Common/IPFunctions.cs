using System;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Xml;

namespace eZeroOne.Common
{
 
    public class IPFunctions
    {
        private static string COUNTRY_NODE = "/ab:ipinfo/ab:Location/ab:CountryData/ab:country";
        private static string CITY_NODE = "/ab:ipinfo/ab:Location/ab:CityData/ab:city";

        /// <summary>
        ///  Gather the IP information
        /// </summary>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="countrycode"></param>
        public static void GetIpiInfo( out string lat,  out string lon,out string ip, out string city)
        {
            //address = "50.56.103.107";
            //string url = string.Format("http://api.ipinfodb.com/v2/ip_query.php?key=eb8be02ac7d20d14de2eeb51a93a9127719c69b8c9e74758c7d9dbe20c89ac80&ip={0}&timezone=false", address),
            string url =
                string.Format(
                    "http://api.ipinfodb.com/v2/ip_query.php?key=eb8be02ac7d20d14de2eeb51a93a9127719c69b8c9e74758c7d9dbe20c89ac80&timezone=false");
            city = string.Empty;
            lat = string.Empty;
            lon = string.Empty;
            ip = string.Empty;

            var wc = new WebClient();
            Stream data = wc.OpenRead(url);
            //read the data from the service response
            using (XmlReader xr = XmlReader.Create(data))
            {
                while (xr.Read())
                {
                    xr.MoveToElement();
                    //if (xr.NodeType == XmlNodeType.Element && (xr.Name == "CountryName" || xr.Name == "City" || xr.Name == "Latitude"))
                    if (xr.NodeType == XmlNodeType.Element && (xr.Name == "Ip" || xr.Name == "Longitude" || xr.Name == "Latitude" || xr.Name == "City"))
                    {
                        string element = xr.Name;
                        xr.Read();
                        xr.MoveToElement();
                        if (xr.NodeType == XmlNodeType.Text)
                        {
                            if (element == "Ip")
                            {
                                ip = xr.Value;
                            }
                            if (element == "Longitude")
                            {
                                lon = xr.Value;
                            }
                            if (element == "Latitude")
                            {
                                lat = xr.Value;
                            }
                            if (element == "City")
                            {
                                city = xr.Value;
                            }
                        }
                    }
                }
                data.Close();
            }


        }
        public static void CreateUsingIpiInfo(string address, out string countryCode)
        {
            //address = "50.56.103.107";
            string url = string.Format("http://api.ipinfodb.com/v2/ip_query.php?key=eb8be02ac7d20d14de2eeb51a93a9127719c69b8c9e74758c7d9dbe20c89ac80&ip={0}&timezone=false", address),
            countryName = string.Empty;
            countryCode = string.Empty;

            var wc = new WebClient();
            Stream data = wc.OpenRead(url);
            //read the data from the service response
            using (XmlReader xr = XmlReader.Create(data))
            {
                while (xr.Read())
                {
                    xr.MoveToElement();
                    if (xr.NodeType == XmlNodeType.Element && (xr.Name == "CountryCode"))
                    {
                        string element = xr.Name;
                        xr.Read();
                        xr.MoveToElement();
                        if (xr.NodeType == XmlNodeType.Text || xr.NodeType == XmlNodeType.EndElement)
                        {
                            if (element == "CountryCode")
                            {
                                countryCode = xr.Value;
                                break;
                            }
                            
                        }
                    }
                }
                data.Close();
            }


        }

        private static string  MD5GenerateHash(string strInput)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(strInput));

            // Create a new Stringbuilder to collect the bytes and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int nIndex = 0; nIndex < data.Length; ++nIndex)
            {
                sBuilder.Append(data[nIndex].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        //public static void GetIpInfo(string address, out string city, out string countrycode)
        //{
        //    string service = "http://api.quova.com/";
        //    string version = "v1/";
        //    string method = "ipinfo/";
        //    string ipAddress = "119.235.14.117";
        //    string apikey = System.Configuration.ConfigurationManager.AppSettings["apikey"].ToString();
        //    string secret = "s2Q3Cx6P";
        //    string sig = MD5GenerateHash(apikey + secret + (Int32)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds);
        //    string fullURL = service + version + method + ipAddress + "?apikey=" + apikey + "&sig=" + sig + "&format=xml";
        //    city = string.Empty;
        //    countrycode = string.Empty;

        //    // Create the web request
        //    HttpWebRequest request = WebRequest.Create(fullURL) as HttpWebRequest;

        //    // Get response
        //    string gResponse;
        //    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        //    {
        //        // Get the response stream
        //        StreamReader reader = new StreamReader(response.GetResponseStream());

        //        // Write response to the console
        //        gResponse = reader.ReadToEnd();
        //    }

        //    var Xml = new XmlDocument();
        //    Xml.LoadXml(gResponse);

        //    XmlNamespaceManager nsmgr = new XmlNamespaceManager(Xml.NameTable);
        //    nsmgr.AddNamespace("ab", "http://data.quova.com/1");


        //    XmlNode nodeCountry = Xml.SelectSingleNode(COUNTRY_NODE, nsmgr);
        //    if (nodeCountry != null)
        //    {
        //        countrycode = nodeCountry.InnerText;
        //    }

        //    XmlNode nodeCity = Xml.SelectSingleNode(CITY_NODE, nsmgr);
        //    if (nodeCity != null)
        //    {
        //        city = nodeCity.InnerText;
        //    }

        //}

    }
}
