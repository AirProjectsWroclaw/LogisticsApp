using LogisticsProject.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static GoogleApiClient.Program.GoogleCityResponse;
using static GoogleApiClient.Program.GoogleRouteResponse;
using LogisticsProject.Domain;
using LogisticsProject.Domain.Entities;

namespace GoogleApiClient
{
    class Program
    {
        static HttpClient client = new HttpClient();

        public class GoogleCityResponse
        {
            public class Location
            {
                public double lat { get; set; }
                public double lng { get; set; }
            }

            public class Northeast
            {
                public double lat { get; set; }
                public double lng { get; set; }
            }

            public class Southwest
            {
                public double lat { get; set; }
                public double lng { get; set; }
            }

            public class Viewport
            {
                public Northeast northeast { get; set; }
                public Southwest southwest { get; set; }
            }

            public class Geometry
            {
                public Location location { get; set; }
                public Viewport viewport { get; set; }
            }

            public class Photo
            {
                public int height { get; set; }
                public List<string> html_attributions { get; set; }
                public string photo_reference { get; set; }
                public int width { get; set; }
            }

            public class Result
            {
                public string formatted_address { get; set; }
                public Geometry geometry { get; set; }
                public string icon { get; set; }
                public string id { get; set; }
                public string name { get; set; }
                public List<Photo> photos { get; set; }
                public string place_id { get; set; }
                public int rating { get; set; }
                public string reference { get; set; }
                public List<string> types { get; set; }
            }

            public class RootObject
            {
                public List<object> html_attributions { get; set; }
                public List<Result> results { get; set; }
                public string status { get; set; }
            }
        }

        public class GoogleRouteResponse
        {

            public class Distance
            {
                public string text { get; set; }
                public int value { get; set; }
            }

            public class Duration
            {
                public string text { get; set; }
                public int value { get; set; }
            }

            public class Element
            {
                public Distance distance { get; set; }
                public Duration duration { get; set; }
                public string status { get; set; }
            }

            public class Row
            {
                public List<Element> elements { get; set; }
            }

            public class RouteObject
            {
                public List<string> destination_addresses { get; set; }
                public List<string> origin_addresses { get; set; }
                public List<Row> rows { get; set; }
                public string status { get; set; }
            }
        }

        //static async Task<Uri> CreateProductAsync(City city)
        //{
        //    HttpResponseMessage response = await client.PostAsJsonAsync(
        //        "api/products", city);
        //    response.EnsureSuccessStatusCode();

        //    // return URI of the created resource.
        //    return response.Headers.Location;
        //}

        static async Task<City> GetCityAsync(string path)
        {
            City city = new City();
            var json = await client.GetStringAsync(path);
            RootObject cityRes = JsonConvert.DeserializeObject<RootObject>(json);
            Console.WriteLine(cityRes.results[0].name);
            city.CityName = cityRes.results[0].name;
            city.Latitude = cityRes.results[0].geometry.location.lat;
            city.Longitude = cityRes.results[0].geometry.location.lng;

            return city;
        }

        static async Task<City> UpdateCityAsync(City city)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/products/{city.CityId}", city);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            city = await response.Content.ReadAsAsync<City>();
            return city;
        }

        static async Task<HttpStatusCode> DeleteCityAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/products/{id}");
            return response.StatusCode;
        }

        static async Task<IDictionary<string, Double>> GetRouteAsync(string path)
        {
            Dictionary<string, Double> results = new Dictionary<string, Double>();
            var json = await client.GetStringAsync(path);
            RouteObject routeRes = JsonConvert.DeserializeObject<RouteObject>(json);
            Console.WriteLine(routeRes.rows[0].elements[0].distance.text);
            results.Add("distance", routeRes.rows[0].elements[0].distance.value);
            results.Add("duration", routeRes.rows[0].elements[0].duration.value);

            return results;
        }

        public static void Main()
        {
            string[] cities = { "Poznan", "Wrocław", "Łódź", "Berlin", "Wiedeń"
            , "Bukareszt", "Warszawa", "Gdańsk"
            , "Brusela", "Monachium", "Paryż", "Ljubljana"
            , "Salzburg", "Stuttgart"
            , "Amsterdam", "Manchester"
            , "Rzym", "Neapol", "Sarajevo", "Brno"
            , "Praga", "Genewa", "Bratysława", "Kraków", "Katowice", "Wenecja", "Zurych"
            , "Marsylia", "Lizbona"};
            //RunAsync(cities).GetAwaiter().GetResult();
            RunAsyncRoutes().GetAwaiter().GetResult();
        }

        static async Task RunAsync(string[] citiesArray)
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
                List<City> citiesList = new List<City>();

                foreach(string oneCity in citiesArray)
                {
                    City locCity = await GetCityAsync("https://maps.googleapis.com/maps/api/place/textsearch/json?query=" +
                    oneCity +
                    "&key=AIzaSyCs1bDNHDz0M6TlIjUo2_gBWS4my0yuQ-Q");
                    citiesList.Add(locCity);
                    //Console.ReadLine();
                }


                using (var db = new EFDbContext())
                {
                    foreach (City c in citiesList)
                    {
                        db.Cities.Add(c);
                    }
                    db.SaveChanges();
                }


                //City city = await GetProductAsync("/maps/api/place/textsearch/json?query=" +
                //    "New_York" +
                //    "&key=AIzaSyCs1bDNHDz0M6TlIjUo2_gBWS4my0yuQ-Q");

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

        static async Task RunAsyncRoutes()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://maps.googleapis.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Get the updated product
                List<City> citiesList = new List<City>();
                List<Route> routesList = new List<Route>();

                using (var db = new EFDbContext())
                {
                    var query = from city in db.Cities
                                orderby city.CityName
                                select city;
                    citiesList = query.ToList();

                    foreach (City oneCity in citiesList)
                    {
                        foreach (City twoCity in citiesList)
                        { 
                            if(oneCity != twoCity)
                            {
                                IDictionary<string, Double> routeResult = await GetRouteAsync("https://maps.googleapis.com/maps/api/distancematrix/json?origins=" +
                                oneCity.CityName + "&destinations=" +
                                twoCity.CityName +
                                "&key=AIzaSyCs1bDNHDz0M6TlIjUo2_gBWS4my0yuQ-Q");
                                Route route = new Route();
                                route.cityFrom = oneCity;
                                route.cityTo = twoCity;
                                route.distance = routeResult["distance"];
                                route.duration = routeResult["duration"];
                                routesList.Add(route);
                                //Console.ReadLine();
                            }
                       
                        }
                    }
                }



                using (var db = new EFDbContext())
                {
                    foreach (Route r in routesList)
                    {
                        db.Routes.Add(r);
                    }
                    db.SaveChanges();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }

}
