using Dapper;
using ZonalTechTest.Entities;

namespace ZonalTechTest.Repository;

public class LaunchCommandRepository : ILaunchCommandRepository
{
    private readonly DapperConnectionProvider _dapperProvider;

    public LaunchCommandRepository(DapperConnectionProvider dapperProvider)
    {
        _dapperProvider = dapperProvider;
    }

    public async Task<bool> AddLaunchAsync(Launch launch)
    {
        string sql = @"
                    INSERT INTO Launch(FlightNumber, MissionName, LaunchYear, LaunchDateUTC, RocketId)
                    VALUES(@flightNumber, @missionName, @year, @launchDate, @rocketId)
                ";

        var parameters = new
        {
            flightNumber = launch.FlightNumber,
            missionName = launch.MissionName,
            year = launch.LaunchYear,
            launchDate = launch.LaunchDateUTC,
            rocketId = launch.RocketId
        };

        using var connection = _dapperProvider.Connect();

        int count = await connection.ExecuteAsync(sql, parameters);

        return count > 0;
    }
}
