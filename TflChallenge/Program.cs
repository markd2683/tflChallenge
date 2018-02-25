using System;
using System.Net.Http;
using TflChallenge.API;
using TflChallenge.Models;
using TflChallenge.Services;

namespace TflChallenge
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            validateArgs(args);
            var id = args[0];

            var api = new RoadApi(client);
            var roadService = new RoadService(api);
            var response = roadService.GetRoadStatusById(id).GetAwaiter().GetResult();

            if (!response.Success)
            {
                Console.WriteLine($"An unexpected error occurred whilst retrieving the status for road with Id {id}");
                Environment.Exit((int)ExitCode.Error_UnexpectedError);
            }

            if (response.RoadStatus == null)
            {
                Console.WriteLine($"{id} is not a valid road");
                Environment.Exit((int)ExitCode.Error_EntityNotFound);
            }

            Console.WriteLine($"The status of the {response.RoadId} is as follows");
            Console.WriteLine($"\t Road Status is {response.RoadStatus.StatusSeverity}");
            Console.WriteLine($"\t Road Status Description is {response.RoadStatus.StatusSeverityDescription}");
            Environment.Exit((int)ExitCode.Success);
        }

        private static void validateArgs(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please provide a valid road ID (e.g. A2 or \"Blackwall Tunnel\")");
                Environment.Exit((int)ExitCode.Error_InvalidArgs);
            }
        }
    }
}
