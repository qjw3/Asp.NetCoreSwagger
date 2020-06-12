using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApplication1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //services.AddMvc(c => c.Conventions.Add(new ApiExplorerGroupPerVersionConvention()));

            var sc = SwaggerClass.Get();
            services.AddSwaggerGen(c =>
            {
                foreach(var each in sc)
                {
                    c.SwaggerDoc(each.Key, each.Value);
                    c.IncludeXmlComments(System.IO.Path.Combine(System.AppContext.BaseDirectory, each.Key+".xml"));
                }
                c.DocInclusionPredicate((x, y) =>
                {
                    if (!y.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var name = methodInfo.DeclaringType.Assembly.GetName().Name;

                    return name == x;

                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            var sc = SwaggerClass.Get();


            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
                });
            });


            app.UseSwaggerUI(c =>
            {
                foreach (var each in sc)
                {
                    c.SwaggerEndpoint($"/swagger/{each.Key}/swagger.json", each.Value.Title);
                }
                //c.SwaggerEndpoint("/swagger/WebApplication1/swagger.json", "My API V1");
                //c.SwaggerEndpoint("/swagger/ClassLibrary1/swagger.json", "My API2 V2");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }

        public class ApiExplorerGroupPerVersionConvention : IControllerModelConvention
        {
            public void Apply(ControllerModel controller)
            {
                var name = controller.ControllerType.Assembly.GetName().Name; // e.g. "Controllers.V1"
                //var apiVersion = controllerNamespace.Split('.').Last().ToLower();

                controller.ApiExplorer.GroupName = name;
            }
        }

        public class MyDocumentFilter : IDocumentFilter
        {
            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {
                var a = swaggerDoc.Paths;
            }
        }
    }
}
