using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

public class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
{
    static readonly HttpClient client = new HttpClient();
    public BikeRentalStationList stationList;
    public async Task<int> GetBikeCountInStation(string stationName)
    {
        for(int i = 0; i < stationName.Length; i++)
        {
            if(Char.IsDigit(stationName, i))
            {
                throw new ArgumentException("Invalid argument: " + stationName);
            }
        }

        try	
        {
            HttpResponseMessage response = await client.GetAsync("http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            // Above three lines can be replaced with new helper method below
            // string responseBody = await client.GetStringAsync(uri);

            stationList = JsonConvert.DeserializeObject<BikeRentalStationList>(responseBody);

            foreach(BikeRentalStationList.Station s in stationList.stations)
            {
                if(s.name == stationName) {
                    return s.bikesAvailable;
                }
            }
            throw new NotFoundException("Not found: " + stationName);

        }
        catch(HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");	
            Console.WriteLine("Message :{0} ",e.Message);
        }

        return 0;
    }
}