using Autofac;
using Autofac.Extensions.DependencyInjection;
using Library.Api.Extension;
using Library.Api.Infrastructure;
using Library.Application;

namespace Library.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Bootstrap.LoadEnvironmentVariables();
            builder.Configuration.AddEnvironmentVariables();

            // Configuración CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowSpecificOrigins",
                    policy =>
                    {
                        var allowedOrigins = builder.Configuration["ORIGINS_CONFIGURATION"]
                            .Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(origin => origin.Trim())
                            .ToArray();

                        policy.WithOrigins(allowedOrigins)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            builder.Services.AddAuthenticationE(builder.Configuration);
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(container =>
            {
                container.RegisterModule(new ApplicationModule(
                    builder.Configuration["DB-CONNECTION"],
                    builder.Configuration["TIMEZONE"]));
                container.RegisterModule(new MediatorModule());
            });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowSpecificOrigins");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}