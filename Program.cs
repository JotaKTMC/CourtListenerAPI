using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;
using System.Linq;

namespace ConsoleApp_ConsumeCourtListenerAPI
{
    public class Court
    {
        public string citation_string { get; set; }
        public string short_name { get; set; }
        public string full_name { get; set; }
        public string url { get; set; }
        public string jurisdiction { get; set; }
    }
    public class CourtResponse
    {
        public int count { get; set; }
        public string next { get; set; }
        public IList<Court> results  { get; set; }
    }

    internal class Program
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        static readonly HttpClient client = new HttpClient();

        /* Asynchronous way */
        static async Task Main()
        {
            client.BaseAddress = new Uri("https://www.courtlistener.com/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Token 58c753e8b4d42f71c899fe3f08a8f5a9498cb86e");
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = await client.GetAsync("rest/v3/courts/?fields=citation_string,short_name,full_name,url,jurisdiction");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var resultJson = JsonConvert.DeserializeObject<CourtResponse>(responseBody);
                Console.WriteLine($"Total: {resultJson.count}");
                Console.WriteLine($"Lista: {resultJson.results[0].citation_string.ToString()}");
                foreach (var i in resultJson.results)
                {
                    Console.WriteLine(i.full_name);
                }
                /*List<Court> courts = new List<Court>();
                courts = resultJson.courts.ToList<Court>();
                foreach (var i in courts)
                {
                    Console.WriteLine(i.full_name);
                } */
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }


        /* Old method */

        /*
        static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.courtlistener.com/api/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Token 58c753e8b4d42f71c899fe3f08a8f5a9498cb86e");

                var responseTask = client.GetAsync("rest/v3/courts/?fields=citation_string,short_name,full_name,url,jurisdiction");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    Console.WriteLine(readTask.Result.ToString());
                    
                    /*
                    var readTask = result.Content.ReadAsAsync<Court[]>();
                    readTask.Wait();

                    var students = readTask.Result;

                    foreach (var student in students)
                    {
                        Console.WriteLine(student.Name);
                    }
                }
            }
            Console.ReadLine();
        }
        */




        /*
        static async Task<Court> GetCourts(string path)
        {
            Court court = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                court = await response.Content.ReadAsAsync<Court>();
            }
            return court;
        }
        static async Task RunAsync()
        {
            var client = new RestClient("https://www.courtlistener.com/api/rest/v3/courts/?fields=citation_string,short_name,full_name,url,jurisdiction");
            client.T = -1;
            var request = new RestRequest(Method.Get);
            request.AddHeader("Authorization", "Token 58c753e8b4d42f71c899fe3f08a8f5a9498cb86e");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);


            client.BaseAddress = new Uri("https://www.courtlistener.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                List<Court> courts = new List<Court>();

                
                /*
                 * // Create a new product
                Product product = new Product
                {
                    Name = "Gizmo",
                    Price = 100,
                    Category = "Widgets"
                };

                var url = await CreateProductAsync(product);
                Console.WriteLine($"Created at {url}");

                // Get the product
                product = await GetProductAsync(url.PathAndQuery);
                ShowProduct(product);

                // Update the product
                Console.WriteLine("Updating price...");
                product.Price = 80;
                await UpdateProductAsync(product);

                // Get the updated product
                product = await GetProductAsync(url.PathAndQuery);
                ShowProduct(product);

                // Delete the product
                var statusCode = await DeleteProductAsync(product.Id);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine(); */
    }
}