using System;
using Adwaer.ApiContract.Net;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = ContractService
                .GetFromHttp("http://ru.global.nba.com/stats2/league/conferenceteamlist.json?locale=ru")
                .Result;

            Console.WriteLine(s);
            Console.ReadLine();
        }
    }
    
}
