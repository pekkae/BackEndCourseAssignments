using System.Threading.Tasks;
using System.IO;
using System;
using System.Collections.Generic;

public class OfflineCityBikeDataFetcher : ICityBikeDataFetcher
{
    public BikeRentalStationList stationList;

    public OfflineCityBikeDataFetcher()
    {
        stationList = new BikeRentalStationList();

        string fileName = "bikedata.txt";

        string[] lines = File.ReadAllLines(fileName);

        List<BikeRentalStationList.Station> st = new List<BikeRentalStationList.Station>();

        foreach(string line in lines) {
            string[] split = line.Split(':');

            st.Add(new BikeRentalStationList.Station() { name = split[0].Trim(), bikesAvailable = Int32.Parse(split[1]) });

        }

        stationList.stations = st.ToArray();
    }

    public Task<int> GetBikeCountInStation(string stationName)
    {
        for(int i = 0; i < stationName.Length; i++)
        {
            if(Char.IsDigit(stationName, i))
            {
                throw new ArgumentException("Invalid argument: " + stationName);
            }
        }

        foreach(BikeRentalStationList.Station s in stationList.stations)
            {
                if(s.name == stationName) {
                    return Task.FromResult(s.bikesAvailable);
                }
            }
            throw new NotFoundException("Not found: " + stationName);

    }


}