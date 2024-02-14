using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Documents;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace APICustomers
{    
public static class StoreClientData
    {
        [FunctionName("StoreClientData")]
        public static async Task Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] ClientData input,
            [CosmosDB(
                databaseName: "AppDemoCustomers",
                containerName: "Customers",
                Connection = "CosmosDBConnection")] IAsyncCollector<ClientData> documentsOut,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            input.id = Guid.NewGuid().ToString();

            await documentsOut.AddAsync(input);
        }
    }

    public class ClientData
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Product { get; set; }
    }
}
