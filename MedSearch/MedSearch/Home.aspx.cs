﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Parsing;

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
            SparqlResultSet results = endpoint.QueryWithResultSet(query);
            if (results.Count != 0)
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
                results = endpoint.QueryWithResultSet(query);
                //File.AppendAllText("E:\\newTest.txt", query + Environment.NewLine);
                if (results.Count != 0)
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
        public string a1 = "Nothing";
        protected void Page_Load(object sender, EventArgs e)
        {
            Console.Write("Hello");
            medsearch test = new medsearch("Calpol");
            a1 = test.getAbstract();
            Console.Write(a1);
            
        }
        
        
    }
}