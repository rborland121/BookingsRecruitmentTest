using AutoMapper;
using Dapper;
using System.Collections.Generic;
using ZonalTechTest.DataObjects;
using ZonalTechTest.Entities;

namespace ZonalTechTest.Repository;

public class LaunchQueryRepository : ILaunchQueryRepository
{
    private readonly IMapper _mapper;
    private readonly DapperConnectionProvider _dapperProvider;

    public LaunchQueryRepository(IMapper mapper, DapperConnectionProvider dapperProvider)
    {
        _mapper = mapper;
        _dapperProvider = dapperProvider;
    }

    public async Task<IEnumerable<LaunchDTO>> GetLaunchAsync(int flightNumber)
    {
        string sql = @"
                    SELECT FlightNumber,
	                       MissionName,
	                       LaunchYear,
	                       LaunchDateUTC
                    FROM Launch
                    WHERE FlightNumber = @flightNumber
                ";

        var parameters = new
        {
            flightNumber = flightNumber
        };

        using var connection = _dapperProvider.Connect();

        var launches = await connection.QueryAsync<Launch>(sql, parameters);

        return _mapper.Map<IEnumerable<LaunchDTO>>(launches);
    }
}
