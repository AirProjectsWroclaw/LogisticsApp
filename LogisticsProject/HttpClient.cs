using LogisticsProject.Domain;
using LogisticsProject.Domain.Entities;
using LogisticsProject.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientLib
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static async Task<Uri> CreateProductAsync(City city)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/products", city);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<City> GetProductAsync(string path)
        {
            City product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<City>();
            }
            return product;
        }

        static async Task<City> UpdateProductAsync(City city)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/products/{city.CityId}", city);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            city = await response.Content.ReadAsAsync<City>();
            return city;
        }

        static async Task<HttpStatusCode> DeleteProductAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/products/{id}");
            return response.StatusCode;
        }

        public static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://maps.googleapis.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new product
                //City city = new City
                //{
                //    CityName = "Gizmo",
                //    Latitude = 100,
                //    Longitude = 299
                //};

                //var url = await CreateProductAsync(city);
                //Console.WriteLine($"Created at {url}");

                //// Get the product
                //city = await GetProductAsync(url.PathAndQuery);
                ////ShowProduct(city);

                //// Update the product
                //Console.WriteLine("Updating price...");
                ////city.Price = 80;
                //await UpdateProductAsync(city);

                // Get the updated product
                City city = await GetProductAsync("/maps/api/place/textsearch/json?query=gostyń&key=AIzaSyCs1bDNHDz0M6TlIjUo2_gBWS4my0yuQ-Q");
                //ShowProduct(city);

                // Delete the product
                //var statusCode = await DeleteProductAsync(city.Id);
                //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}