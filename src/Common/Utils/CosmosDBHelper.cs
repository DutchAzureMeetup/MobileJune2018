using System;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Common.Utils
{
    public static class CosmosDBHelper
    {
        private const string CosmosEndpointUrl = "https://azuremeetupjune.documents.azure.com:443/";
        private const string CosmosAuthorizationKey = "<ADD_BEFORE_MEETUP>";
        private const string CosmosDatabaseName = "MovieDB";
        private const string CosmosCollectionName = "Movies";

        public static DocumentClient CreateDocumentClient()
        {
            var endpointUrl = new Uri(CosmosEndpointUrl);

            return new DocumentClient(endpointUrl, CosmosAuthorizationKey);
        }

        public static async Task<Uri> GetDocumentCollectionUri(this DocumentClient client)
        {
            // Create the database (if it doesn't exist yet).
            Database database = new Database { Id = CosmosDatabaseName };

            await client.CreateDatabaseIfNotExistsAsync(database);

            var databaseUri = UriFactory.CreateDatabaseUri(CosmosDatabaseName);

            // Create a collection in the database (if it doesn't exist yet).
            DocumentCollection collection = new DocumentCollection { Id = CosmosCollectionName };
            RequestOptions requestOptions = new RequestOptions { OfferThroughput = 1000 };

            await client.CreateDocumentCollectionIfNotExistsAsync(databaseUri, collection, requestOptions);

            return UriFactory.CreateDocumentCollectionUri(CosmosDatabaseName, CosmosCollectionName);
        }
    }
}