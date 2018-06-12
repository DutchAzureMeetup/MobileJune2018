
Required application settings (you can create a `local.settings.json` file for running locally):

- Cosmos.EndpointUrl
- Cosmos.AuthorizationKey
- Cosmos.DatabaseName
- Cosmos.CollectionName

Due to a bug relating to Cosmos DB in 2.0.1-beta.28 (https://github.com/Azure/azure-functions-host/pull/2958) we must use the 2.0.1-beta.25 version of the Azure Functions Core Tools.
On macOS, don't install using Homebrew, it will give you beta.28. Use NPM instead.

