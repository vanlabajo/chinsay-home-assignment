using GoGreen.API.Commands;
using GoGreen.API.Queries;
using GoGreen.Domain.Aggregates.VegetableAggregate;
using GoGreen.Infra.EFCore;
using GoGreen.Infra.EFCore.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace GoGreen.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowedOrigins",
                    builder =>
                    {
                        builder.WithOrigins(Configuration.GetSection("AllowedOrigins")
                            .GetChildren().Select(s => s.Value).ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });

            });

            services.AddDbContext<GoGreenContext>
                (options => options.UseSqlite(Configuration.GetConnectionString("GoGreenContext")));

            services.AddScoped<IVegetableRepository, VegetableRepository>();
            services.AddScoped<IVegetableQueries, VegetableQueries>();
            services.AddScoped<IVegetableCommands, VegetableCommands>();

            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors("AllowedOrigins");
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
