using Microsoft.AspNetCore.SignalR;
using RealTimeDB.Hubs;
using System.ComponentModel;
using TableDependency.SqlClient;

namespace RealTimeDB.Subscription;

public interface IDataBaseSubscription
{
    void Configure(string tableName);
}

public class DatabaseSubscription<T> : IDataBaseSubscription where T : class, new()
{
    IConfiguration _configuration;
    IHubContext<SatisHub> _hubContext;

    public DatabaseSubscription(IConfiguration configuration, IHubContext<SatisHub> hubContext)
    {

        _configuration = configuration;
        _hubContext = hubContext;

    }

    SqlTableDependency<T> _tableDependency;
    public void Configure(string tableName)
    {
        _tableDependency = new SqlTableDependency<T>(_configuration.GetConnectionString("DefaultConnection"), tableName);
        _tableDependency.OnChanged += async (o, e) =>
        
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Salam");
        };

        _tableDependency.OnError += (o, e) =>
        {

        };


        _tableDependency.Start();
    }

    ~DatabaseSubscription()
    {
        _tableDependency.Stop();
    }

}
