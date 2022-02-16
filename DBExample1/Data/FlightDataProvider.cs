using System.Data.SqlClient;
using Dapper;
using DBExample1.Models;
using NLog;

namespace DBExample1.Data;

public class FlightDataProvider
{
    private Logger _logger = LogManager.GetCurrentClassLogger();
    private string _connStr = "Server=DESKTOP-IFN84LU\\SQLEXPRESS;Database=FlightData;Trusted_Connection=True;";
    
    public FlightDataProvider(){}

    public Task<List<FlightData>> GetFlightData()
    {
        _logger.Trace("Entering...");
        List<FlightData> results;
        const string sql = "SELECT TOP 100 * FROM [dbo].[feds1]";
        using SqlConnection conn = new(_connStr);
        try
        {
            conn.Open();
            results = conn.Query<FlightData>(sql).ToList();
            
            return Task.FromResult(results);
        }
        catch (Exception e)
        {
            _logger.Trace($"SQLException: {e.Message}");
            results = new List<FlightData>();
        }
        return Task.FromResult(results);
    }

    private SqlConnection GetConnection()
    {
        _logger.Trace("Entering");
        try
        {
            return new SqlConnection(_connStr);
        }
        catch (Exception e)
        {
            _logger.Error("Unable to connect to host");
        }
        return null;
    }
    
}