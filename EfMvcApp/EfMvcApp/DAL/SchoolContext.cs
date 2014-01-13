using EfMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;


using EFCachingProvider;
using EFCachingProvider.Caching;
using EFProviderWrapperToolkit;
using EFTracingProvider;
using System.Data.Common;
using System.Configuration;

namespace EfMvcApp.DAL
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public SchoolContext() { }



         public SchoolContext(DbConnection existingConnection) : base(existingConnection, true) { }

         private static DbConnection CreateConnection(String nameOrConnectionString)
    {
        var connectionStringSetting = ConfigurationManager.ConnectionStrings[nameOrConnectionString];
        var connectionString = "";
        var providerName = "";
     
        if (connectionStringSetting != null)
        {
            connectionString = connectionStringSetting.ConnectionString;
           providerName = connectionStringSetting.ProviderName;
       }
       else
       {
           connectionString = nameOrConnectionString;
           providerName = "System.Data.SqlClient";
       }
    
       return (CreateConnection(connectionString, providerName));
   }
    
  private static DbConnection CreateConnection(String connectionString, String providerInvariantName)
  {
       var wrapperConnectionString = String.Format(@"wrappedProvider={0};{1}", providerInvariantName, connectionString);
       var connection = new EFTracingConnection { ConnectionString = wrapperConnectionString };
       return (connection);
   }

     public static SchoolContext CreateTracingContext(String nameOrConnectionString, Action<CommandExecutionEventArgs> logAction, Boolean logToConsole = true, String logToFile = null)
    {
        EFTracingProviderConfiguration.LogToFile = logToFile;
        EFTracingProviderConfiguration.LogToConsole = logToConsole;
        EFTracingProviderConfiguration.LogAction = logAction;
     
        var ctx = new SchoolContext(CreateConnection(nameOrConnectionString));
       //(ctx as IObjectContextAdapter).ObjectContext.EnableTracing();
        return (ctx);
   }
    





      
    }
}