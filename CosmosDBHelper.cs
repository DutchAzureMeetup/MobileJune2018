using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace DutchAzureMeetup
{
    public static class CosmosDBHelper
    {
        public static DocumentClient CreateDocumentClient()
        {
            var endpointUrl = new Uri(GetEnvironmentVariable("Cosmos.EndpointUrl"));
            var authorizationKey = GetEnvironmentVariable("Cosmos.AuthorizationKey");

            return new DocumentClient(endpointUrl, authorizationKey);
        }

        public static async Task<Uri> GetDocumentCollectionUri(this DocumentClient client)
        {
            var databaseName = GetEnvironmentVariable("Cosmos.DatabaseName");
            var collectionName = GetEnvironmentVariable("Cosmos.CollectionName");

            // Create the database (if it doesn't exist yet).
            Database database = new Database { Id = databaseName };

            await client.CreateDatabaseIfNotExistsAsync(database);

            var databaseUri = UriFactory.CreateDatabaseUri(databaseName);

            // Create a collection in the database (if it doesn't exist yet).
            DocumentCollection collection = new DocumentCollection { Id = collectionName };
            RequestOptions requestOptions = new RequestOptions { OfferThroughput = 1000 };

            await client.CreateDocumentCollectionIfNotExistsAsync(databaseUri, collection, requestOptions);

            return UriFactory.CreateDocumentCollectionUri(databaseName, collectionName);
        }

        private static string GetEnvironmentVariable(string name)
        {
            return System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}