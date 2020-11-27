using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using city.api.Context;
using city.api.GraphQL.Queries;
using city.api.GraphQL.Types;
using city.core.entities;
using city.core.repositories;
using city.core.services;
using crud.api.core.fieldType;
using crud.api.core.repositories;
using data.provider.core;
using data.provider.core.mongo;
using graph.simplify.core;
using GraphQL.Server;
using GraphQL.Server.Internal;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace city.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Program.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options => {
                options.AddPolicy(Program.AllowSpecificOrigins,
                    builder => {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            #region Dependences
            services.AddScoped<CityService>();

            services.AddScoped<IRepository<City>, CityRepository>();
            services.AddScoped<IRepository<State>, BaseRepository<State>>();
            services.AddScoped<IRepository<Country>, BaseRepository<Country>>();

            services.AddScoped<IMongoClient, MongoClientFactory>();

            services.AddScoped<IDataProvider, DataProvider>(x =>
                new DataProvider(new MongoClientFactory(), Program.DataBaseName)
            );
            #endregion

            #region GraphQL Setup
            StartupResolve.ConfigureServices<Startup>(services);
            services.AddSingleton<ExternalAccessSettings>(serviceProvider => new ExternalAccessSettings("ExternalAccess", serviceProvider.GetService<IConfiguration>()));
            services.AddScoped<IGraphQLExecuter<AppScheme<MacroQuery>>, DefaultGraphQLExecuter<AppScheme<MacroQuery>>>();

            services.AddScoped<MacroQuery>();
            services.AddScoped<CityQuery>();
            services.AddScoped<StateQuery>();
            services.AddScoped<CountryQuery>();
            
            services.AddScoped<CityType>();
            services.AddScoped<StateType>();
            services.AddScoped<CountryType>();
            services.AddScoped<GuidGraphType>();
            
            services.AddScoped<AppScheme<MacroQuery>>();
            
            services.AddScoped<EnumerationGraphType<RecordStatus>>();
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddOptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                
            }

            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors(Program.AllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "ui/playground");
            app.UseRewriter(option);

            #region GraphQL Setup
            app.UseGraphQL<AppScheme<MacroQuery>>();
            StartupResolve.Configure(app);
            #endregion
        }
    }
}
