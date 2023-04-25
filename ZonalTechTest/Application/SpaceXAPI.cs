using ZonalTechTest.DataObjects;

namespace ZonalTechTest.Application;


public class SpaceXAPI
{
    private const string BASE_URL = "https://api.spacexdata.com/v3/";
    private const string LAUNCH_ENDPOINT = "launches";


    HttpClient client = new HttpClient();

    public async Task<SpaceXLaunchDTO> GetLaunchDataAsync(int launchId = 0)
    {
        string path = $"{BASE_URL}{LAUNCH_ENDPOINT}/";
        string addVariable = launchId == 0 ? "" : launchId.ToString();
        var response = await client.GetAsync(path + addVariable);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<SpaceXLaunchDTO>();
        }

        return null;
    }
}
