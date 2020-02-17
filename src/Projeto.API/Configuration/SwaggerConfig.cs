using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Projeto.API.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", CreateInfoForApi());

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            return app;
        }

        static Microsoft.OpenApi.Models.OpenApiInfo CreateInfoForApi()
        {
            var info = new Microsoft.OpenApi.Models.OpenApiInfo()
            {
                Title = "API - CRUD Usuário",
                Version = "v1",
                Description = "API desenvolvida em .Net Core 3 para operações: Create, Read, Update e Delete persistindo em banco NoSql MongoDB.",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact() { Name = "Anderson Comar", Email = "andersoncomar@gmail.com" },
                TermsOfService = new Uri("https://opensource.org/licenses/MIT"),
                License = new Microsoft.OpenApi.Models.OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            };

            return info;
        }
    }
}