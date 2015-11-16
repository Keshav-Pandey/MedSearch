using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MedSearch
{
    /// <summary>
    /// Summary description for MedLocation
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MedLocation : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetLocations(string term)
        {
            if(String.Equals(term,"Malaria",StringComparison.OrdinalIgnoreCase))
            {
                return "Papua New Guinea:-6.314993,143.955550;DR Congo:-1.982331,24.736632;Nigeria:9.081999,8.675277;Mozambique:-18.665695,35.529562;Madagascar:-18.766947,46.869107;Argentina:-34.579235,-58.415521;Burkina Faso:12.238333,-1.561593;Sierra Leone:8.460555,-11.779889;";
            }
            else if (String.Equals(term, "Cholera", StringComparison.OrdinalIgnoreCase))
            {
                return "Angola:-11.202692,17.873887;Burundi:-3.373056,29.918886;DR Congo:-1.982331,24.736632;Nigeria:9.081999,8.675277;Mozambique::-18.665695,35.529562;Somalia:2.0371100:45.3437500;Mexico:23.634501,-102.552784;Dominican Republic:18.5001200:-69.9885700;Haiti:18.971187,-72.285215;Cuba:23.1330200,-82.3830400;Afghanistan:33.939110,67.709953;";
            }
            else if (String.Equals(term, "Leprosy", StringComparison.OrdinalIgnoreCase))
            {
                return "Bangladesh:23.684994,90.356331;Brazil:-11.092165893502,-51.50390625;DR Congo:-0.228021,15.827659;Ethiopia:9.145000,40.489673;India:20.593684,78.962880;Indonesia:0.789275,113.921327;Madagascar:-18.766947,46.869107;Myanmar:21.913965,95.956223;Nepal:28.394857,84.124008;Nigeria:9.081999,8.675277;Philippines:12.879721,121.774017;Sri Lanka:7.873054,80.771797;Tanzania:6.369028,34.888822;";
            }
            else if (String.Equals(term, "Diclofenac", StringComparison.OrdinalIgnoreCase))
            {
                return "China:35.861660,104.195397;Czech Republic:49.817492,15.472962;Egypt:26.820553,30.802498;Estonia:58.595272,25.013607;Georgia:41.715138,44.827096;Hong Kong:22.396428,114.109497;Latvia:56.879635,24.603189;Lithuania:55.169438,23.881275;Malaysia:55.169438,23.881275;Malta:35.937496,14.375416;Oman:21.512583,55.923255;Poland:51.919438,19.145136;Portugal:39.399872,-8.224454;Singapore:1.352083,103.819836;Slovenia:46.151241,14.995463;Switzerland:46.818188,8.227512;";
            }
            else if (String.Equals(term, "Calpol", StringComparison.OrdinalIgnoreCase))
            {
                return "UK:55.378051,-3.435973;Ireland:53.412910,-8.243890;India:20.593684,78.962880;Cyprus:35.126413,33.429859;Hong Kong:22.396428,114.109497;Malta:35.937496,14.375416;Philippines:12.879721,121.774017;";
            }
            else if (String.Equals(term, "Benadryl", StringComparison.OrdinalIgnoreCase))
            {
                return "Australia:-29.075375179558346,137.28515625;Belgium:50.80593472676908,4.405517578125;Brazil:-11.092165893502,-51.50390625;Bulgaria:42.733883,25.485830;Canada:56.130366,-106.346771;Germany:51.165691,10.451526;New Zealand:40.900557,174.885971;South Africa:-30.559482,22.937506;UK:55.378051,-3.435973;USA:37.50972584293751,-96.943359375;";
            }
            else { return ";"; }
        }
    }
}
