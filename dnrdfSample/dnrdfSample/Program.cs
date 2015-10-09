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

            //foreach (SparqlResult result in results)
            //{
            //    File.AppendAllText("E:\\" + searchTerm + ".txt", result.ToString() + Environment.NewLine);
            //    File.AppendAllText("E:\\" + searchTerm + ".txt", result.GetType().ToString() + Environment.NewLine);
            //}
            // Make a SELECT query against the Endpoint
            // SparqlResultSet results = endpoint.QueryWithResultSet("SELECT DISTINCT ?Concept WHERE {[] a ?Concept}");
            // foreach (SparqlResult result in results)
            // {
            //     File.AppendAllText("E:\\out2.txt", result.ToString() + "\n");
            //     Console.WriteLine(result.ToString());
            // }
            // SparqlRemoteEndpoint endpoint2 = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"));

            // Ask DBPedia to describe the first thing it finds which is a Person
            //var query = "DESCRIBE ?type ?drug ?content  WHERE {?type a <http://dbpedia.org/ontology/Drug>} LIMIT 20";

            // Get the result
            // var dbpGraph = endpoint2.QueryWithResultGraph(query);
            // int count = 1;
            // var query2 = "SELECT ?drug  WHERE {<http://dbpedia.org/resource/PIM-35><http://dbpedia.org/ontology/abstract> ?drug } LIMIT 8";
            // SparqlResultSet result = (SparqlResultSet)dbpGraph.ExecuteQuery(query2);
            // File.AppendAllText("E:\\out.txt", Environment.NewLine + result.ToString() + Environment.NewLine);
            // foreach (var r in result)
            // {
            //     File.AppendAllText("E:\\out.txt", r.ToString() + Environment.NewLine);
            //     File.AppendAllText("E:\\out.txt", "Here" + Environment.NewLine);
            //     Console.WriteLine(result.ToString());
            // }

            // var t1 = dbpGraph.GetTriplesWithPredicate("");
            // foreach (Triple T in dbpGraph.Triples)
            // {
            //     File.AppendAllText("E:\\out.txt", T.ToString() + "\n");
            //     string context = "null", subject = "null", predicate = "null", obj = "null";
            //     try
            //     {
            //         context = T.Context.ToString();
            //         subject = T.Subject.ToString();
            //         predicate = T.Predicate.ToString();
            //         obj = T.Object.ToString();
            //     }
            //     catch (Exception e)
            //     {
            //         Console.WriteLine(e.Message);
            //         throw e;
            //     }
            //     string ou = "Triple " + count + ":\nContext: " + context + "\n\nSubject: " + subject + "\n\nPredicate: " + predicate + "\n\nObject: " + obj + "\n\nTriple: " + T.ToString() + "\n\n";
            //     File.AppendAllText("E:\\out2.txt", Environment.NewLine + ou + Environment.NewLine);
            //     string ou = "Triple " + count + Environment.NewLine;
            //     foreach (var x in T.Nodes)
            //     {
            //         string tp = "";
            //         File.AppendAllText("E:\\out.txt", tp + "\n");
            //     }
            //     count += 1;
            // }

            // Console.WriteLine(dbpGraph.ToString());

            // Make a DESCRIBE query against the Endpoint
            // IGraph g = endpoint.QueryWithResultGraph("DESCRIBE");
            // try
            // {
            //     IGraph g2 = endpoint.QueryWithResultGraph("DESCRIBE");
            // }
            // catch (Exception)
            // {

            //     throw;
            // }
            // Triple[] ou = dbpGraph.GetListAsTriples();
            // foreach (Triple t in ou)
            // {
            //     Console.WriteLine(t.ToString());
            // }
        }
    }
}
