using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Isitar.PlantLogBook.Api.Installers;
using Isitar.PlantLogBook.Core.Behaviors;
using Isitar.PlantLogBook.Core.Commands;
using Isitar.PlantLogBook.Core.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Isitar.PlantLogBook.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<PlantLogBookContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });
            
            SwaggerSetup.ConfigureService(services);
            
            services.AddMediatR(typeof(CreatePlantSpeciesCommand).Assembly);
            services.AddValidatorsFromAssembly(typeof(CreatePlantSpeciesCommand).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddCors(options =>
                options.AddPolicy("AllowAll", builder => { builder.AllowAnyOrigin().AllowAnyMethod(); })
            );

        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PlantLogBookContext dbContext)
        {
            dbContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SwaggerSetup.ConfigureApplication(app);
                app.UseCors("AllowAll");
            }

            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}