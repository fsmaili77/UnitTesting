using EmployeesApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesApp.IntegrationTests
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        // Override the ConfigureWebHost method to configure services and setup an in-memory database for testing.
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Find the existing DbContextOptions for the EmployeeContext.
                var descriptor = services.SingleOrDefault(d => d.ServiceType == 
                    typeof(DbContextOptions<EmployeeContext>));

                // If the descriptor for DbContextOptions<EmployeeContext> is found, remove it.
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add a new DbContext for EmployeeContext configured to use an in-memory database.
                services.AddDbContext<EmployeeContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryEmployeeTest");
                });

                // Build the service provider and create a scope.
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<EmployeeContext>())
                {
                    try
                    {
                        // Ensure that the in-memory database is created (if it doesn't exist).                        
                        appContext.Database.EnsureCreated();
                    }
                    catch (Exception ex)
                    {
                        
                        throw;
                    }
                }
            });
        }
    }

}
