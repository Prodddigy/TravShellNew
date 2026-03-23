using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


using GoogleApi;
using GoogleApi.Entities.Places.AutoComplete.Request;

using Newtonsoft.Json.Linq;
using System.Globalization;
using TravShell.Core.Entities;
using TravShell.Core.Entities.ViewModels;
using GoogleApi.Entities.Places.AutoComplete.Response;
using Microsoft.Extensions.Configuration;

namespace TravShell.App.GoogleAPI;
public class GoogleService : IGoogleService
{

    public static List<String> days= new(){
		"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" ,"Sunday"};
        

     

    public static string apiKey = "";
    public static string autocompleteKey = "";

    public GoogleService(IConfiguration configuration)
    {
        apiKey = configuration["TravShellGoogleMapsApi"];
        autocompleteKey = configuration["Google:BackendApiKey"];
        Console.WriteLine(apiKey);
    }


    public async Task<TripViewModel> GetCoordinates(TripViewModel model, CancellationToken token)
    {

//        var apikey = builder.Configuration["GoogleAPIKey"];
        string placesApi = $"https://maps.googleapis.com/maps/api/place/textsearch/json?query={Uri.EscapeDataString(model.City)}&key={autocompleteKey}";

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(placesApi,token);

            token.ThrowIfCancellationRequested();

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
                   
                }
            }
            else
            {
                Console.WriteLine("Google API response error: " + response.StatusCode);
            }
        }
        return null;
    }

    public async Task<List<AttractionViewModel>> GetSetAttractions(string coords, AttractionType type, List<AttractionViewModel> list, CancellationToken token)
    {
        string placesApi = null;

        string zone = "";

        if (type == AttractionType.Attraction)
        {
            placesApi = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?&location={coords}&radius=3000&rankby=prominence&type=tourist_attraction&key={autocompleteKey}";
            zone = "1";
        
        }

        if (type == AttractionType.Restaurant)
        {
            placesApi = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?&location={coords}&radius=3000&rankby=prominence&type=restaurant&key={autocompleteKey}";
            zone = "2";
        }

        if (type == AttractionType.Accommodation)
        {
            placesApi = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?&location={coords}&radius=3000&rankby=prominence&type=lodging&key={autocompleteKey}";
            zone = "3";
        }

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage httpRespMess = await client.GetAsync(placesApi,token);
            token.ThrowIfCancellationRequested();

            if (httpRespMess.IsSuccessStatusCode)
            {
                string result = await httpRespMess.Content.ReadAsStringAsync();

                JObject parsed = JObject.Parse(result);

                var attractions = parsed["results"];
                Console.WriteLine("number of attractions of type:"+type+" => " + attractions.Count()+" successfully downloaded");
                

                for (int i = 0; i < attractions.Count(); i++)
                {
                    list.Add(new AttractionViewModel
                    {
                        PlaceCode = attractions[i]["place_id"].ToString(),

                        Name = attractions[i]["name"].ToString(),

                        Vicinity = attractions[i]["vicinity"].ToString(),

                        Rating = attractions[i]["rating"] != null ? attractions[i]["rating"].ToString() : "Rating unavailable",

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
                string result = await httpRespMess.Content.ReadAsStringAsync();
                Console.WriteLine("Google API response error: " + httpRespMess.StatusCode);
             
            }
        }
        return list;
    }

    public async Task<List<AttractionViewModel>?> GetSetAttractionsDetails(List<AttractionViewModel> list, CancellationToken token)
    {
        if(list ==null)
        {
            return null;
        }
        foreach (var attraction in list)
        {
            string du = $"https://maps.googleapis.com/maps/api/place/details/json?fields=opening_hours%2Cwebsite&place_id={attraction.PlaceCode}&key={autocompleteKey}";
            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage httpRespMess = await client.GetAsync(du,token);

                token.ThrowIfCancellationRequested();

                if (httpRespMess.IsSuccessStatusCode)
                {
                    string result = await httpRespMess.Content.ReadAsStringAsync();

                    JObject details = JObject.Parse(result);
                    var placeResult = details["result"];

                    var openingHours = placeResult["opening_hours"];
                    var website = placeResult["website"]?.ToString();
                    string opening_hours = "Working Hours:";

                    if (openingHours != null)
                    {
                        int i = 0;
                   
                        foreach (var period in openingHours["periods"])
                        {
                            string openTime = period["open"]?["time"]?.ToString();


                            string closeTime = period["close"]?["time"]?.ToString();

                            if (i > 6)
                            {
                                break;
                            }
                            opening_hours += $"{days[i++]} from: {openTime} to: {closeTime}, ";
                            if(i == 6)
                            {
                                break;
                            }
                        
                        }

                        attraction.Opening_Hours = opening_hours;
                    }
                    else
                    {
                        attraction.Opening_Hours = "Opening hours are not available.";
                    }

                    if (!string.IsNullOrEmpty(website))
                    {
                        attraction.Website = website;
               
                    }
                    else
                    {
                        attraction.Website = "Website is not available.";
                       
                    }
                }
                else
                {
                    Console.WriteLine("Google API response error: " + httpRespMess.StatusCode);
                }
            }
        }

        return list;
    }

    public async Task<IEnumerable<string>> GetPlaceNamesAutocomplete(string place, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(place))
            return Enumerable.Empty<string>();

        var request = new PlacesAutoCompleteRequest
        {
            Key = autocompleteKey,
            Input = place
        };


        PlacesAutoCompleteResponse? response = null;
        try
        {
            response = await GooglePlaces.AutoComplete.QueryAsync(request, token);

            token.ThrowIfCancellationRequested();
        }
      
        catch(Exception e)
        {

        }
             

        if (response?.Predictions == null || !response.Predictions.Any())
            return Enumerable.Empty<string>();
        

        return response.Predictions.Select(p => p.Description);
    }
}

