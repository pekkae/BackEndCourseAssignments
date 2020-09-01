using System;
using System.Threading.Tasks;

namespace assignments
{
  class Program
    {
       static async Task<int> Main(string[] args)
        {
            ICityBikeDataFetcher fetcher;

            if(args.Length < 2)
            {
                Console.WriteLine("More arguments needed: arg1 = station name; arg2 {realtime|offline}");
                return await Task.FromResult(-1);
            }

            if(args[1] == "realtime") {
                 fetcher = new RealTimeCityBikeDataFetcher();
            } 
            else if(args[1] == "offline") {
                fetcher = new OfflineCityBikeDataFetcher();
            }
            else {
                fetcher = null;
                throw new Exception("No fetcher type provided");
            }
            try {
                Console.WriteLine(await fetcher.GetBikeCountInStation(args[0]));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return await Task.FromResult(0);
        }
    }
}
