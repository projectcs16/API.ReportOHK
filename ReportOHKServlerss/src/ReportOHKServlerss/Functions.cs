using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ReportOHKServlerss;

public class Functions
{
    private readonly HttpClient _client;

    /// <summary>
    /// Default constructor that Lambda will invoke.
    /// </summary>
    public Functions()
    {
        _client = new();
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        _client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

        
    }

    static async Task<string> ProcessRepositoriesAsync(HttpClient client)
    {
        var json = await client.GetStringAsync(
            "https://0ge09ja2v2.execute-api.ap-southeast-1.amazonaws.com/Prod/cats");

        Console.Write(json);
        return json;
    }


    /// <summary>
    /// A Lambda function to respond to HTTP Get methods from API Gateway
    /// </summary>
    /// <param name="request"></param>
    /// <returns>The API Gateway response.</returns>
    public APIGatewayProxyResponse Get(APIGatewayProxyRequest request, ILambdaContext context)
    {
        
        context.Logger.LogInformation("Get  People Request\n");
        //var x = ProcessRepositoriesAsync(_client).GetAwaiter().GetResult();
        var ohk = new Repositories.OHKDataBaseRepository();

        //var x = ohk.GetPeople();
        context.Logger.LogInformation("test console");
        var response = new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.OK,
            Body = $"Hello AWS Serverless Response Get",
            Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
        };

        return response;
    }

    /// <summary>
    /// A Lambda function to respond to HTTP Get methods from API Gateway
    /// </summary>
    /// <param name="request"></param>
    /// <returns>The API Gateway response.</returns>
    public APIGatewayProxyResponse Get2(APIGatewayProxyRequest request, ILambdaContext context)
    {
        context.Logger.LogInformation("Get Request\n");

        var response = new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.OK,
            Body = $"Hello AWS Serverless Get22 {request.Path}",
            Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
        };

        return response;
    }

    // <summary>
    /// A Lambda function to respond to HTTP Get methods from API Gateway
    /// </summary>
    /// <param name="request"></param>
    /// <returns>The API Gateway response.</returns>
    public APIGatewayProxyResponse GetCats(APIGatewayProxyRequest request, ILambdaContext context)
    {
        context.Logger.LogInformation($"GetCats Request  {request.Path}-{JsonSerializer.Serialize(request)}------{JsonSerializer.Serialize(context)}\n");

        var response = new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.OK,
            Body = $"Hello AWS Serverless Cats {request.Path}-{JsonSerializer.Serialize(request)}------{JsonSerializer.Serialize(context)}",
            Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
        };

        return response;
    }

}
