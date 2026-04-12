using Microsoft.Azure.Cosmos; // To use CosmosClient and so on.
using System.Net; // To use HttpStatusCode.

// This is defined in the default empty namespace, so it merges with
// the SDK-generated partial Program class.
partial class Program
{
    // To use Azure Cosmos DB in the local emulator.
    private static string endpointUri = "https://localhost:8081/";
    private static string primaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
    /*
    // To use Azure Cosmos DB in the cloud.
    private static string account = "<your_account>";
    private static string endpointUri = $"https://{account}.documents.azure.com:433/";
    private static string primaryKey = "<your_key>";
    */

    static async Task CreateCosmosResources()
    {
        SectionTitle("Creating Cosmos resources");

        try
        {
            using CosmosClient client = new(accountEndpoint: endpointUri, authKeyOrResourceToken: primaryKey);

            // Create database
            DatabaseResponse dbResponse = await client
                .CreateDatabaseIfNotExistsAsync("Northwind", throughput: 400 /* RU/s */);
            string status = GetDatabaseStatus(dbResponse);
            WriteLine("Database Id: {0}, Status: {1}.", arg0: dbResponse.Database.Id, arg1: status);

            // Create container
            IndexingPolicy indexingPolicy = new()
            {
                IndexingMode = IndexingMode.Consistent,
                Automatic = true, // Items are indexed unless explicitly excluded.
                IncludedPaths = { new IncludedPath { Path = "/*" } }
            };
            ContainerProperties containerProperties = new("Products", partitionKeyPath: "/productId")
            {
                IndexingPolicy = indexingPolicy
            };
            ContainerResponse containerResponse = await dbResponse.Database
                .CreateContainerIfNotExistsAsync(containerProperties, throughput: 1000 /* RU/s */);
            status = GetDatabaseStatus(dbResponse);
            WriteLine("Container Id: {0}, Status: {1}.", arg0: containerResponse.Container.Id, arg1: status);

            // Print container props
            Container container = containerResponse.Container;
            ContainerProperties properties = await container.ReadContainerAsync();
            WriteLine($"  PartitionKeyPath: {properties.PartitionKeyPath}");
            WriteLine($"  LastModified: {properties.LastModified}");
            WriteLine("  IndexingPolicy.IndexingMode: {0}", arg0: properties.IndexingPolicy.IndexingMode);
            WriteLine("  IndexingPolicy.IncludedPaths: {0}", 
                arg0: string.Join(",", properties.IndexingPolicy.IncludedPaths.Select(path => path.Path)));
            WriteLine($"  IndexingPolicy: {properties.IndexingPolicy}");
        }
        catch (HttpRequestException ex)
        {
            WriteLine($"Error: {ex.Message}");
            WriteLine("Hint: If you are using the Azure Cosmos Emulator then please make sure that it is running.");
        }
        catch (Exception ex)
        {
            WriteLine("Error: {0} says {1}", arg0: ex.GetType(), arg1: ex.Message);
        }
    }

    private static string GetDatabaseStatus(DatabaseResponse dbResponse)
    {
        return dbResponse.StatusCode switch
        {
            HttpStatusCode.OK => "exists",
            HttpStatusCode.Created => "created",
            _ => "unknown"
        };
    }
}