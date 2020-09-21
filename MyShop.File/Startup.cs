using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyShop.ApiModels.Models.Response;
using MyShop.BL.DIConfigurations;
using MyShop.Core.Configurations;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.Core.Middlewares;
using MyShop.DL.PostgreSql;
using MyShop.DL.PostgreSql.Repositories;
using Newtonsoft.Json;
using System;
using System.Linq;


namespace MyShop.File
{
    public class Startup
    {
        private readonly string _webClientUrl;
        private readonly string _adminPanelClientUrl;
        private string[] _clientUrls;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _webClientUrl = Configuration["HttpClients:WebClientUrl"];
            _adminPanelClientUrl = Configuration["HttpClients:AdminPanelClientUrl"];
            _clientUrls = new[] { _webClientUrl, _adminPanelClientUrl };
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // JWT
            AddJwtAuthentication(services);

            // CORS
            services.AddCors();

            // NewtonJson
            services.AddControllers().AddNewtonsoftJson();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "MyShop.FileStore",
                    Description = "MyShop.FileStore Application",
                });
            });

            WebApiDIRegistration register = new WebApiDIRegistration(Configuration);
            register.RegisterAll(ref services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Global error handler
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            // HTTP error handler
            app.UseStatusCodePages(async context =>
            {
                string message = "One or more errors occurred";

                if (context.HttpContext.Request.Path.StartsWithSegments("/api") &&
                   (context.HttpContext.Response.StatusCode == 401 || context.HttpContext.Response.StatusCode == 403))
                {
                    message = "Unauthorized request";
                }
                else if (context.HttpContext.Response.StatusCode == 404)
                {
                    message = "Not Found";
                }

                var model = ResponseModelBuild(message);
                var json = JsonConvert.SerializeObject(model);
                context.HttpContext.Response.ContentType = "application/json";
                await context.HttpContext.Response.WriteAsync(json);
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            { 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            // CORS
            app.UseCors(builder =>
            {
                builder.WithOrigins(_clientUrls)
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // JWT
        private void AddJwtAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = AuthJwtConfig.Current.SymmetricSecurityKey,
                    ValidateIssuer = true,
                    ValidIssuer = AuthJwtConfig.Current.Issuer,
                    ValidateAudience = true,
                    ValidAudience = AuthJwtConfig.Current.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
                //options.Events = new JwtBearerEvents
                //{
                //    OnMessageReceived = context =>
                //    {
                //        var accessToken = context.Request.Query["access_token"];

                //        var path = context.HttpContext.Request.Path;
                //        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/signalr"))
                //        {
                //            context.Token = accessToken;
                //        }

                //        return Task.CompletedTask;
                //    }
                //};
            });
        }

        private static ErrorResponceApiModel<string> ResponseModelBuild(params string[] errors)
        {
            ErrorResponceApiModel<string> result = new ErrorResponceApiModel<string>()
            {
                Errors = errors.ToList(),
            };

            return result;
        }

        // Extra class container for register all DI
        internal class WebApiDIRegistration : DIRegistration
        {
            public WebApiDIRegistration(IConfiguration configuration) : base(configuration)
            {
            }

            public override void RegisterConfigs(ref IServiceCollection services)
            {
                services.AddSingleton(DataMapperConfig.GetMapper());
                services.AddTransient<IDataMapper, DataMapper>();
                services.AddTransient<IDatabaseSettings, PostgreSqlSettings>();
                services.AddTransient(typeof(IGenericRepository<,>), typeof(PostgreSqlRepository<,>));
                services.AddTransient<IDbContext, MyShopPostgreSqlContext>();                         
            }
        }
    }
}