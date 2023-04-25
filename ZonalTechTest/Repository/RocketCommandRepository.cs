using Dapper;
using ZonalTechTest.Entities;

namespace ZonalTechTest.Repository;

public class RocketCommandRepository : IRocketCommandRepository
{
    private readonly DapperConnectionProvider _dapperProvider;

    public RocketCommandRepository(DapperConnectionProvider dapperProvider)
    {
        _dapperProvider = dapperProvider;
    }

    public async Task<bool> AddRocketAsync(Rocket rocket)
    {
        string sql = @"
                    INSERT INTO Rocket(RocketId, RocketName, RocketType)
                    VALUES(@RocketId, @RocketName, @RocketType)
                ";

        var parameters = new
        {
            RocketId = rocket.RocketId,
            RocketName = rocket.RocketName,
            RocketType = rocket.RocketType
        };

        using var connection = _dapperProvider.Connect();

        int count = await connection.ExecuteAsync(sql, parameters);

        return count > 0;
    }
}
