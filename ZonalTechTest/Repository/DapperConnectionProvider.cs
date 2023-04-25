using Dapper;
using System.Data.SQLite;
using System.Data;

namespace ZonalTechTest.Repository;

public class DapperConnectionProvider
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperConnectionProvider(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("SqlConnection");
    }

    public IDbConnection Connect() 
        => new SQLiteConnection(_connectionString);

    public async Task Init()
    {
        if (!File.Exists("LaunchDatabase.sqlite")) 
        {
            SQLiteConnection.CreateFile("LaunchDatabase.sqlite");
        }

        using var connection = Connect();
        
        connection.Open();
        var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Launch';").FirstOrDefault();

        if (!string.IsNullOrEmpty(table) && table == "Launch")
        {
            return;
        }

        await _setDatabase();

        async Task _setDatabase()
        {
            var sql = @"
CREATE TABLE Rocket (
	RocketId VARCHAR(50) PRIMARY KEY NOT NULL, 
	RocketName VARCHAR(50) NOT NULL,
	RocketType VARCHAR(50) NOT NULL
);

CREATE TABLE Launch (
	FlightNumber INT PRIMARY KEY NOT NULL,
	MissionName VARCHAR(100) NOT NULL,
	LaunchYear VARCHAR(4) NOT NULL,
	LaunchDateUTC DATETIME NOT NULL,
	RocketId VARCHAR(50) NOT NULL
)
            ";

            await connection.ExecuteAsync(sql);
        }
    }
}
