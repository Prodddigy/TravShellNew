using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;
using System.Globalization;
using TravShell.Core.Entities;
using TravShell.Core.Entities.ViewModels;

namespace TravShell.App.GoogleAPI;
public class GoogleService :IGoogleService
{
    public static string apiKey = "AIzaSyARfnT3GsxeslzR5yjxcqDrlRGb88qBnUs";

    public async Task<TripViewModel> GetCoordinates(TripViewModel model)
    {
        string placesApi = $"https://maps.googleapis.com/maps/api/place/textsearch/json?query={Uri.EscapeDataString(model.City)}&key={apiKey}";

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(placesApi);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();

                JObject a = JObject.Parse(result);

                var coordsResult = a["results"]?.First;

                if (coordsResult != null)
                {
                     model.latitude = coordsResult["geometry"]["location"]["lat"].ToObject<double>();
                     model.longitude = coordsResult["geometry"]["location"]["lng"].ToObject<double>();
                    return model;
                    //return $"{latitude.ToString(CultureInfo.InvariantCulture)}%2C{longitude.ToString(CultureInfo.InvariantCulture)}";
                }
            }
            else
            {
                Console.WriteLine("Error" + response.StatusCode);
            }
        }
        return null;
    }

    public async Task<List<AttractionViewModel>> GetSetAttractions(string coords, AttractionType type, List<AttractionViewModel> list)
    {
        string placesApi = null;

        string zone = "";

        if (type == AttractionType.Attraction)
        {
            placesApi = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?&location={coords}&radius=1500&rankby=prominence&type=tourist_attraction&key={apiKey}";
            zone = "1";
        
        }

        if (type == AttractionType.Restaurant)
        {
            placesApi = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?&location={coords}&radius=1500&rankby=prominence&type=restaurant&key={apiKey}";
            zone = "2";
        }

        if (type == AttractionType.Accommodation)
        {
            placesApi = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?&location={coords}&radius=1500&rankby=prominence&type=lodging&key={apiKey}";
            zone = "3";
        }

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(placesApi);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();

                JObject parsed = JObject.Parse(result);

                var attractions = parsed["results"];
                Console.WriteLine("number of attractions" + attractions.Count());

                for (int i = 0; i < attractions.Count(); i++)
                {
                    list.Add(new AttractionViewModel
                    {
                        PlaceCode = attractions[i]["place_id"].ToString(),
                        Name = attractions[i]["name"].ToString(),
                        Vicinity = attractions[i]["vicinity"].ToString(),
                        Rating = attractions[i]["rating"] != null ? attractions[i]["rating"].ToString() : "No rating",

                        latitude = attractions[i]["geometry"]["location"]["lat"].ToObject<double>(),
                        longitude = attractions[i]["geometry"]["location"]["lng"].ToObject<double>(),

                        Type = type,

                        dropzone= zone

                    }
                    );
                }
            }
            else
            {
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error: " + response.StatusCode);
                Console.WriteLine("Response content: " + result);
            }
        }
        return list;
    }

    public async Task<List<AttractionViewModel>?> GetSetAttractionsDetails(List<AttractionViewModel> list)
    {
        if(list ==null)
        {
            return null;
        }
        foreach (var attraction in list)
        {
            string du = $"https://maps.googleapis.com/maps/api/place/details/json?fields=opening_hours%2Cwebsite&place_id={attraction.PlaceCode}&key={apiKey}";
            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(du);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    JObject details = JObject.Parse(result);
                    var placeResult = details["result"];

                    var openingHours = placeResult["opening_hours"];
                    var website = placeResult["website"]?.ToString();
                    string opening_hours = "Opening Hours:";

                    if (openingHours != null)
                    {
                        int i = 1;
                        // string opening_hours = "Opening Hours:";

                        foreach (var period in openingHours["periods"])
                        {
                            string openTime = period["open"]?["time"]?.ToString();
                            string closeTime = period["close"]?["time"]?.ToString();
                            opening_hours += $"\n day {i++}/7 - {openTime} to {closeTime}";
                            //   Console.WriteLine($"day {i++}/7 - {openTime} to {closeTime}");
                        }

                        attraction.Opening_Hours = opening_hours;
                    }
                    else
                    {
                        attraction.Opening_Hours = "Opening hours are not available.";
                        //   Console.WriteLine("Opening hours are not available.");
                    }

                    if (!string.IsNullOrEmpty(website))
                    {
                        attraction.Website = website;
                        //  Console.WriteLine($"Website: {website}");
                    }
                    else
                    {
                        attraction.Website = "Website is not available.";
                        //  Console.WriteLine("Website is not available.");
                    }
                }
                else
                {
                    Console.WriteLine("Error fetching details: " + response.StatusCode);
                }
            }
        }

        return list;
    }
}

