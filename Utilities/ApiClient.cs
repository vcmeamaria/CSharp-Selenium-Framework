using RestSharp;

namespace SauceDemo.Automation.Utilities;

public sealed class ApiClient
{
    private readonly RestClient _client;

    public ApiClient(string baseUrl)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            throw new ArgumentException(
                "API base URL cannot be empty.",
                nameof(baseUrl)
            );
        }

        var options = new RestClientOptions(baseUrl);

        _client = new RestClient(options);
    }

    public async Task<RestResponse> GetAsync(string endpoint)
    {
        if (string.IsNullOrWhiteSpace(endpoint))
        {
            throw new ArgumentException(
                "API endpoint cannot be empty.",
                nameof(endpoint)
            );
        }

        var request = new RestRequest(
            endpoint,
            Method.Get
        );

        return await _client.ExecuteAsync(request);
    }
}