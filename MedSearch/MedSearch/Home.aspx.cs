using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Parsing;
using BingSynonyms;

namespace MedSearch
{
    class medsearch
    {
        string searchURI;
        string searchTerm;
        bool valid;
        string type;
        string imageURI;
        string abst;
        SparqlResultSet results = new SparqlResultSet();
        private SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"), "http://dbpedia.org");
        public medsearch(string q)
        {
            searchTerm = q;
            searchURI = "";
            abst = "not yet";
            valid = false;
            type = "none";
            imageURI = "none";
            buildSearch();
        }
        private void buildSearch()
        {
            var query = "";
            string resUri = "<http://dbpedia.org/resource/" + searchTerm + ">";
            query = "select ?out WHERE {" + resUri + " <http://dbpedia.org/ontology/abstract> ?out FILTER langMatches(lang(?out),'en')}";
            try
            {
                results = endpoint.QueryWithResultSet(query);
            }
            catch (Exception)
            {

                //Handle Issues
                
            }
            //if (results.Count != 0)
            if(!results.IsEmpty)
            {
                SparqlResult res = results.First();
                string s = res.ToString();
                string g = s.Substring(7, s.Length - 10);
                abst = g;
                searchURI = resUri.Substring(1, resUri.Length - 2);
                valid = true;
                type = "drug proper";
            }
            else
            {
                query = "select ?out WHERE { ?out <http://dbpedia.org/property/tradename> ?use. ?use bif:contains \"" + searchTerm + "\"} ";
                try
                {
                    results = endpoint.QueryWithResultSet(query);
                }
                catch (Exception)
                {

                    results = new SparqlResultSet();
                }
                //File.AppendAllText("E:\\newTest.txt", query + Environment.NewLine);
                if (!results.IsEmpty)
                {
                    SparqlResult res = results.First();
                    string s = res.ToString();
                    string g = s.Substring(7, s.Length - 7);
                    searchURI = g;
                    type = "drug trade";
                    valid = true;
                }
            }
        }
        public string getType()
        {
            return type;
        }
        public string getAbstract()
        {
            if (!valid)
            {
                return "invalid search query";
            }
            else if (abst == "not yet" && type == "drug trade")
            {
                var query = "";
                string resUri = "<" + searchURI + ">";
                query = "select ?out WHERE {" + resUri + " <http://dbpedia.org/ontology/abstract> ?out FILTER langMatches(lang(?out),'en')}";
                SparqlResultSet results = endpoint.QueryWithResultSet(query);
                SparqlResult res = results.First();
                string s = res.ToString();
                string g = s.Substring(7, s.Length - 10);
                abst = searchTerm + " is the common name for " + searchURI.Substring("http://dbpedia.org/resource/".Length) + Environment.NewLine + g;
            }
            return abst;
        }
        public string getImageURI()
        {
            var query = "";
            string resUri = "<" + searchURI + ">";
            query = "select ?out WHERE {" + resUri + " <http://dbpedia.org/ontology/thumbnail> ?out}";
            SparqlResultSet results = endpoint.QueryWithResultSet(query);
            if (results.Count == 0)
            {
                return "no image";
            }
            else
            {
                imageURI = results.First().ToString().Substring(7);
            }
            return imageURI;
        }
    }
    public partial class Home : System.Web.UI.Page
    {
        public string searchResponse = "<center>MedSearch</center><br>A search engine designed for use in medical field with the assistance of machine learning,context awareness and personalization.";
        public string searchResult = "The given term coudn't be found. Please try again.";
        public string searchImage = "Content/images/Medicine.jpg";
        public string imageResult = "no image";
        public string searchSynonyms = "No related medicine.";

        medsearch search;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void performSearch(object sender, EventArgs e)
        {
            
            if (searchEntry.Text != "")
            {
                searchSynonyms = new DailyMed().searchDrugs(searchEntry.Text);
                search = new medsearch(searchEntry.Text);
                searchResult = search.getAbstract();
                if (searchResult != "invalid search query")
                {
                    searchResponse = searchResult;
                    imageResult = search.getImageURI();
                    if (imageResult == "no image")
                        searchImage = "Content/images/Medicine.jpg";
                    else
                        searchImage = imageResult;
                }
                else
                {
                    searchResponse = "The given term coudn't be found. Please try again.";
                    searchImage = "Content/images/Sad.jpg";
                }
            }
            
        }

        protected async void populateMap()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create("http://127.0.0.1/test.php");
            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
            System.IO.Stream Answer = WebResp.GetResponseStream();
            System.IO.StreamReader _Answer = new System.IO.StreamReader(Answer);
            Console.WriteLine(_Answer.ReadToEnd());
            
        }
        protected void findSynonymsFromBing()
        {
            Uri syn = new Uri("https://api.datamarket.azure.com/Bing/Synonyms/v1/");
            BingSynonymsContainer a = new BingSynonymsContainer(syn);
            //GetSynonymsEntitySet ans = a.GetSynonyms("");
            //ans = a.GetSynonyms("canon 400d");
            //searchSynonyms = a.GetSynonyms("canon 600d");
            System.Diagnostics.Debug.WriteLine(a.GetSynonyms("canon 400d"));
        }
    }
}