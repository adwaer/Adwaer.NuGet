using System;
using Adwaer.ApiContract.Net;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = ContractService
                .GetFromHttp("http://testapi.nashanyanya.ru/api/workers?distanceLat=55.786513&distanceLng=49.114419")
                .Result;

            Console.WriteLine(s);
            Console.ReadLine();
        }
    }
    
}
