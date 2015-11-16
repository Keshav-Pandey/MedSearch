using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Xml;

namespace MedSearch
{
    public class DailyMed
    {
        public String searchDrugs(String drugname)
        {
            String URL = "http://dailymed.nlm.nih.gov/dailymed/services/v2/drugnames.xml?drug_name=" + drugname;
            XmlDocument xdoc = new XmlDocument();
            try
            {
                xdoc.Load(URL);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);                
            }
            XmlNodeList xn = xdoc.SelectNodes("//drug_name");
            String output = "<center>Related Terms<center><br>";
            if (xn.Count > 0)
            {
                for (int i = 0; i < xn.Count; i++)
                {
                    output += (i + 1) + ": " + xn[i].InnerXml + "<br>";
                }
            }
            else
            {
                output += "No related medication available.";
            }
            return output;
        }
    }
}