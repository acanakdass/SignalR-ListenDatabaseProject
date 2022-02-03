using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using SignalRLiveDataProject.Data;
using SignalRLiveDataProject.Hubs;
using TableDependency.SqlClient;
using System.Linq;

namespace SignalRLiveDataProject.Subscriptions
{
    public class DatabaseSubscription<T> : IDatabaseSubscription where T:class,new()
    {

        IConfiguration _configuration;
        IHubContext<SalesHub> _hubContext;
        public DatabaseSubscription(IConfiguration configuration, IHubContext<SalesHub> hubContext)
        {
            _configuration = configuration;
            _hubContext = hubContext;
        }

        SqlTableDependency<T> _tableDependency;

        public void Configure(string tableName)
        {
            _tableDependency = new SqlTableDependency<T>(_configuration.GetConnectionString("SqlServer"), tableName);
            _tableDependency.OnChanged += async (object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<T> e) =>
            {
                var tablename = ((SqlTableDependency<T>)sender).TableName;
                var entity = e.Entity;

                AppDbContext context = new AppDbContext();
                var query = from employee in context.Employees
                            join sale in context.Sales
                            on employee.Id equals sale.EmployeeId
                            select new
                            {
                                employee,
                                sale
                            };
                var data = query.ToList();
                var employees = context.Employees;
                var sales = context.Sales;

                await _hubContext.Clients.All.SendAsync("receiveMessage", employees.ToArray());
                await _hubContext.Clients.All.SendAsync("test", "Hello Test");
                
            };
            _tableDependency.OnError +=(sender, e)=>{
                
            };
            _tableDependency.Start();
        }

        //Deconstructor
        ~DatabaseSubscription()
        {
            _tableDependency.Stop();
        }
    }
}

