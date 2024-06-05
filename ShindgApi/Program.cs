
using AlgorandAuthentication;
using Elasticsearch.Net;
using Microsoft.OpenApi.Models;
using Nest;
using RestDWH.Base.Extensios;
using RestDWH.Base.Repository;
using RestDWH.Elastic.Extensions;
using RestDWH.Elastic.Model;
using RestDWH.Elastic.Repository;

namespace ShindgApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ALedgerApi",
                    Version = "v1",
                    Description = File.ReadAllText("doc/readme.md")
                });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "ARC-0014 Algorand authentication transaction",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                });
                c.OperationFilter<Swashbuckle.AspNetCore.Filters.SecurityRequirementsOperationFilter>();
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                var xmlFile = $"doc/documentation.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            });
            builder.Services.AddProblemDetails();

            // Add authentication
            var algorandAuthenticationOptions = new AlgorandAuthenticationOptions();
            builder.Configuration.GetSection("AlgorandAuthentication").Bind(algorandAuthenticationOptions);

            builder.Services
             .AddAuthentication(AlgorandAuthenticationHandler.ID)
             .AddAlgorand(o =>
             {
                 o.CheckExpiration = algorandAuthenticationOptions.CheckExpiration;
                 o.Debug = algorandAuthenticationOptions.Debug;
                 o.AlgodServer = algorandAuthenticationOptions.AlgodServer;
                 o.AlgodServerToken = algorandAuthenticationOptions.AlgodServerToken;
                 o.AlgodServerHeader = algorandAuthenticationOptions.AlgodServerHeader;
                 o.Realm = algorandAuthenticationOptions.Realm;
                 o.NetworkGenesisHash = algorandAuthenticationOptions.NetworkGenesisHash;
                 o.MsPerBlock = algorandAuthenticationOptions.MsPerBlock;
                 o.EmptySuccessOnFailure = algorandAuthenticationOptions.EmptySuccessOnFailure;
                 o.EmptySuccessOnFailure = algorandAuthenticationOptions.EmptySuccessOnFailure;
             });


            // Add CORS policy
            var corsConfig = builder.Configuration.GetSection("Cors").AsEnumerable().Select(k => k.Value).Where(k => !string.IsNullOrEmpty(k)).ToArray();
            if (!(corsConfig?.Length > 0)) throw new Exception("Cors not defined");

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins(corsConfig)
                                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .AllowCredentials();
                });
            });

            // Add DB config 


            var elasticConfig = new Model.Config.Elastic();
            builder.Configuration.GetSection("Elastic").Bind(elasticConfig);
            var config2 = new RestDWH.Elastic.Model.Config.Elastic()
            {
                ApiKey = elasticConfig.Token,
                Host = elasticConfig.Server
            };
            var settings =
                new ConnectionSettings(new Uri(elasticConfig.Server))
                .ApiKeyAuthentication(new ApiKeyAuthenticationCredentials(elasticConfig.Token))
                .ExtendElasticConnectionSettings(config2);

            // add RestDWH entities
            builder.Services.AddSingleton<IDWHRepository<Model.RestDWH.Event>, RestDWHElasticSearchRepository<Model.RestDWH.Event>>();
            builder.Services.AddSingleton<RestDWHEventsElastic<Model.RestDWH.Event>, Model.RestDWH.EventEvents>();
            builder.Services.AddSingleton<IElasticDWHRepository<Model.RestDWH.Event>, RestDWHElasticSearchRepositoryExtended<Model.RestDWH.Event>>();
            builder.Services.AddSingleton<RestDWHElasticSearchRepositoryExtended<Model.RestDWH.Event>>();


            builder.Services.AddSingleton<IDWHRepository<Model.RestDWH.NFT>, RestDWHElasticSearchRepository<Model.RestDWH.NFT>>();
            builder.Services.AddSingleton<RestDWHEventsElastic<Model.RestDWH.NFT>, Model.RestDWH.NFTEvents>();
            builder.Services.AddSingleton<IElasticDWHRepository<Model.RestDWH.NFT>, RestDWHElasticSearchRepositoryExtended<Model.RestDWH.NFT>>();
            builder.Services.AddSingleton<RestDWHElasticSearchRepositoryExtended<Model.RestDWH.NFT>>();

            builder.Services.AddSingleton<IDWHRepository<Model.RestDWH.Thread>, RestDWHElasticSearchRepository<Model.RestDWH.Thread>>();
            builder.Services.AddSingleton<RestDWHEventsElastic<Model.RestDWH.Thread>, Model.RestDWH.ThreadEvents>();
            builder.Services.AddSingleton<IElasticDWHRepository<Model.RestDWH.Thread>, RestDWHElasticSearchRepositoryExtended<Model.RestDWH.Thread>>();
            builder.Services.AddSingleton<RestDWHElasticSearchRepositoryExtended<Model.RestDWH.Thread>>();

            builder.Services.AddSingleton<IDWHRepository<Model.RestDWH.Message>, RestDWHElasticSearchRepository<Model.RestDWH.Message>>();
            builder.Services.AddSingleton<RestDWHEventsElastic<Model.RestDWH.Message>, Model.RestDWH.MessageEvents>();
            builder.Services.AddSingleton<IElasticDWHRepository<Model.RestDWH.Message>, RestDWHElasticSearchRepositoryExtended<Model.RestDWH.Message>>();
            builder.Services.AddSingleton<RestDWHElasticSearchRepositoryExtended<Model.RestDWH.Message>>();


            var client = new ElasticClient(settings);
            builder.Services.AddSingleton<IElasticClient>(client);


            var app = builder.Build();
            app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            var serviceEvent = app.Services.GetService<IDWHRepository<Model.RestDWH.Event>>();
            app.MapEndpoints<Model.RestDWH.Event>(serviceEvent);
            var serviceEventExtended = app.Services.GetService<IElasticDWHRepository<Model.RestDWH.Event>>();
            app.MapElasticEndpoints<Model.RestDWH.Event>(serviceEventExtended);


            var serviceNFT = app.Services.GetService<IDWHRepository<Model.RestDWH.NFT>>();
            app.MapEndpoints<Model.RestDWH.NFT>(serviceNFT);
            var serviceNFTExtended = app.Services.GetService<IElasticDWHRepository<Model.RestDWH.NFT>>();
            app.MapElasticEndpoints<Model.RestDWH.NFT>(serviceNFTExtended);

            var serviceThread = app.Services.GetService<IDWHRepository<Model.RestDWH.Thread>>();
            app.MapEndpoints<Model.RestDWH.Thread>(serviceThread);
            var serviceThreadExtended = app.Services.GetService<IElasticDWHRepository<Model.RestDWH.Thread>>();
            app.MapElasticEndpoints<Model.RestDWH.Thread>(serviceThreadExtended);

            var serviceMessage = app.Services.GetService<IDWHRepository<Model.RestDWH.Message>>();
            app.MapEndpoints<Model.RestDWH.Message>(serviceMessage);
            var serviceMessageExtended = app.Services.GetService<IElasticDWHRepository<Model.RestDWH.Message>>();
            app.MapElasticEndpoints<Model.RestDWH.Message>(serviceMessageExtended);


            app.MapControllers();

            app.Run();
        }
    }
}
