using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using gRPC;
using Grpc.Net.Client;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Calling a gRPC Service");
            var HttpClientHandler = new HttpClientHandler();
            HttpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(HttpClientHandler);
            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = httpClient });
            var client = new Users.UsersClient(channel);
            try
            {
                UserRequest request = new UserRequest(){ CompanyId = 1 };
                using (var call = client.getUsers(request))
                {
                    //stream 
                    while(await call.ResponseStream.MoveNext(CancellationToken.None))
                    {
                        var currentUser = call.ResponseStream.Current;
                        Console.WriteLine(currentUser.FirstName + " " + currentUser.LastName + " is being fetched from the service.");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
