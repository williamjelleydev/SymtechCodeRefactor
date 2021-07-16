using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using refactor_this.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_this
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            // TODO: would be nicer to have some Configuration classes rather than calling into the _configuration[] directly here
            //var connectionString = _configuration["connectionStrings:msqlDatabase"];

            // TODO: put this into appsettings.json or a more secret location even
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\refactor-this.mdf;Integrated Security=True;Connect Timeout=30";

            var dataConnectionProvider = new DataConnectionProvider(connectionString);
            services.AddSingleton<IDataConnectionProvider>(dataConnectionProvider);

            services.AddScoped<IAccountsService, AccountsService>();
            services.AddScoped<ITransactionService, TransactionService>();


            // by default json output formatter is setup so just this should be fine
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // I don't really know why this is throwing, but whatever - will just comment this out for now as not essential lol
                //app.UseDeveloperExceptionPage();
            }



            app.UseMvc();


        }
    }
}