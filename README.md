# Dutch Mobile & Azure Meetup 21-6-2018

Welcome to the Dutch Mobile & Azure Crossover Meetup!

# Lab

## Create a back-end API to get movie data
- Add a new **ASP.NET Core Web Application** project to the solution called *AppFlixApi*.
- When asked to select a template, choose **API**.
- Add a reference to the *Common* project.
- Rename `ValuesController` to `MoviesController`. 
- Add a method to list all movies in the Cosmos DB database:

```csharp
[HttpGet]
public async Task<IActionResult> Get()
{
    using (DocumentClient client = CosmosDBHelper.CreateDocumentClient())
    {
        var collectionUri = await client.GetDocumentCollectionUri();
        var movies = client.CreateDocumentQuery<MovieSummary>(collectionUri)
            .AsEnumerable()
            .OrderByDescending(movie => movie.Title)
            .ToList();

        return new JsonResult(movies);
    }
}
```

- And a method to get the details of a single movie:

``` csharp
[HttpGet("{id}")]
public async Task<IActionResult> Get(string id)
{
    using (var client = CosmosDBHelper.CreateDocumentClient())
    {
        var collectionUri = await client.GetDocumentCollectionUri();

        var movie = client.CreateDocumentQuery<MovieDetails>(collectionUri)
            .Where(m => m.Id == id)
            .AsEnumerable()
            .FirstOrDefault();

        if (movie != null)
        {
            return new JsonResult(movie);
        }

        return new NotFoundResult();
    }
}
```

- Start the API project, when you navigate to `api/movies`, you should see a list of movies.

## Create your own Cosmos DB database
- You can create a Cosmos DB of your own using the Azure Portal. Alternatively, you can download the [Cosmos DB Emulator](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator).
- Update the `CosmosEndpointUrl` and `CosmosAuthorizationKey` constants in the `CosmosDBHelper` class.

## Create a Function to periodically import popular movies
- Add a **Functions** project named `MovieImporter` to solution and select a **Timer trigger**.
- Rename your function to `ImportPopularMovies`.
- Add a reference to the *Common* project. 
- Add the *TMDbLib 1.0.0* NuGet package. This package allows us to easily query The Movie Database website.
- Add the following code to your function to get a list of popular movies and add them to the database:

```csharp
var tmdbClient = new TMDbClient("c6b31d1cdad6a56a23f0c913e2482a31");

var movies = await tmdbClient.GetMoviePopularListAsync();

using (var cosmosClient = CosmosDBHelper.CreateDocumentClient())
{
    var collectionUri = await cosmosClient.GetDocumentCollectionUri();

    foreach (var movie in movies.Results)
    {
        var movieDetails = new MovieDetails
        {
            Id = $"tmdb:{movie.Id}",
            Title = movie.Title,
            Description = movie.Overview,
            Rating = (decimal)movie.Popularity
        };

        await cosmosClient.UpsertDocumentAsync(collectionUri, movieDetails);
    }
}
```

- Running the function will import the movies into your Cosmos DB database. 