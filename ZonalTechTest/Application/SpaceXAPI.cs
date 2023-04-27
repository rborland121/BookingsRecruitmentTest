using System.Net.Http;
using ZonalTechTest.DataObjects;
namespace ZonalTechTest.Application;

public class SpaceXAPI : ISpaceXAPI
{
    public const string BASE_URL = "https://api.spacexdata.com/v3/";
    public const string LAUNCH_ENDPOINT = "launches";
    public const string ROCKET_ENDPOINT = "rockets";

    private readonly IHttpClientFactory _httpClientFactory;

    public SpaceXAPI(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    public async Task<SpaceXLaunchDTO?> GetLaunchDataAsync(int flightNumber = 0)
    {
        string path = $"{BASE_URL}{LAUNCH_ENDPOINT}/";
        string addVariable = flightNumber == 0 ? "" : flightNumber.ToString();
        var response = await GetHttpClient().GetAsync(path + addVariable);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<SpaceXLaunchDTO>();
        }

        return null;
    }

    public async Task<SpaceXRocketDTO?> GetRocketDataAsync(string rocketId)
    {
        string path = $"{BASE_URL}{ROCKET_ENDPOINT}/";
        var response = await GetHttpClient().GetAsync(path + rocketId);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<SpaceXRocketDTO>();
        }

        return null;
    }

    public async Task<IEnumerable<SpaceXRocketDTO>?> GetAllRocketDataAsync()
    {
        string path = $"{BASE_URL}{ROCKET_ENDPOINT}";
        var response = await GetHttpClient().GetAsync(path);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<SpaceXRocketDTO>>();
        }

        return null;
    }

    private HttpClient GetHttpClient()
    {
        return _httpClientFactory.CreateClient();
    }
}
