using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Ecommerce.BLL;
using Ecommerce.DAL;
using Serilog;
using System.Text;
namespace CompanySystem.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {


            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", Serilog.Events.LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameWorkCore", Serilog.Events.LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateBootstrapLogger();
            /*------------------------------------------------------------------*/
            try
            {
                /*------------------------------------------------------------------*/
                Log.Information("App Started");
                Log.Warning("App Started");
                Log.Error("App Started");
                /*------------------------------------------------------------------*/
                var builder = WebApplication.CreateBuilder(args);
                /*------------------------------------------------------------------*/
                // Add services to the container.
                builder.Services.AddControllers();
                builder.Services.AddDALServices(builder.Configuration);
                builder.Services.AddBLLServices();
                builder.Services.AddScoped<TokenService>();

                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

                /*------------------------------------------------------------------*/
                // Serilog
                builder.Host.UseSerilog((context, services, configuration) =>
                {
                    configuration
                    // Load settings from appsettings.json or other configs
                    .ReadFrom.Configuration(context.Configuration)
                    // Allow logging configuration to use registered services (DI)
                    .ReadFrom.Services(services)
                    // Include contextual information in logs (like HTTP request IDs)
                    .Enrich.FromLogContext()
                    // Output logs to console
                    .WriteTo.Console()
                    .WriteTo.File(
                         "logs/log.txt",
                         // Create a new log file daily
                         rollingInterval: RollingInterval.Day,
                         // Keep logs for 30 days before automatic deletion
                         retainedFileTimeLimit: TimeSpan.FromDays(30)
                    );
                });
                /*------------------------------------------------------------------*/
                // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
                builder.Services.AddOpenApi();
                /*------------------------------------------------------------------*/
                // Static Files
                var rootPath = builder.Environment.ContentRootPath;
                var staticFilepath = Path.Combine(rootPath, "Files");
                if (!Directory.Exists(staticFilepath))
                {
                    Directory.CreateDirectory(staticFilepath);
                }
                builder.Services.Configure<StaticFileOptions>(cfg =>
                {
                    cfg.FileProvider = new PhysicalFileProvider(staticFilepath);
                    cfg.RequestPath = "/Files";
                });
                /*------------------------------------------------------------------*/
                //version
                builder.Services.AddApiVersioning(options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;

                    options.DefaultApiVersion = new ApiVersion(1, 0); // V1.1

                    options.ReportApiVersions = true;

                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                })
                 .AddApiExplorer(options =>
                 {
                     options.GroupNameFormat = "'v'VVV";

                     options.SubstituteApiVersionInUrl = true;
                 });
                /*------------------------------------------------------------------*/
                // CORS
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll", policy =>
                    {
                        policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
                });
                /*------------------------------------------------------------------*/
                 
                var app = builder.Build();

 
                /*------------------------------------------------------------------*/
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                }

                app.UseCors("AllowAll");

                app.UseStaticFiles();

                app.UseHttpsRedirection();

                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
    }
}