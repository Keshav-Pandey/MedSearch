using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Parsing;

namespace dnrdfSample
{
    class Program
    {
        static void Main(string[] args)
        {
           // Define a remote endpoint
           // Use the DBPedia SPARQL endpoint with the default Graph set to DBPedia
           SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"), "http://dbpedia.org");
            string searchTerm = "prolia";
            var query = "";
            string resUri = "<http://dbpedia.org/resource/" + searchTerm + ">";
            query = "select ?out WHERE { ?out <http://dbpedia.org/property/tradename> ?use. ?use bif:contains \""+searchTerm+"\"} ";
            SparqlResultSet results = endpoint.QueryWithResultSet(query);
            File.AppendAllText("E:\\newTest.txt", query + Environment.NewLine);
            if (results.Count != 0)
            {
                SparqlResult res = results.First();
                string s = res.ToString();
                string g = s.Substring(7, s.Length - 7);
                Console.WriteLine(g);
                File.AppendAllText("E:\\newTest.txt", g.Substring("http://dbpedia.org/resource/".Length ) + Environment.NewLine);
                resUri = "<" + g + ">";
                query = "select ?out WHERE {" + resUri + " <http://dbpedia.org/ontology/abstract> ?out FILTER langMatches(lang(?out),'en')}";
                results = endpoint.QueryWithResultSet(query);
                res = results.First();
                s = res.ToString();
                g = s.Substring(7, s.Length - 10);
                File.AppendAllText("E:\\newTest.txt", g + Environment.NewLine);
            }
            else
            {
                File.AppendAllText("E:\\newTest.txt", "none" + Environment.NewLine);
            }
            medsearch first = new medsearch("Shigellosis");
            File.AppendAllText("E:\\newTest2.txt", first.getAbstract() + Environment.NewLine);
            File.AppendAllText("E:\\newTest2.txt", first.getImageURI() + Environment.NewLine);
        }
    }
}
