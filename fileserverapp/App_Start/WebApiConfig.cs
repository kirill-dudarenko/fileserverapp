using Autofac;
using Autofac.Integration.WebApi;
using Common.Interfaces;
using fileserverapp.Services;
using FileSystem;
using Newtonsoft.Json;
using Swashbuckle.Application;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;

namespace fileserverapp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*","*","*");
            config.EnableCors(cors);

            // swagger
            config.EnableSwagger(c => {
                c.SingleApiVersion("v1", "fileserverapp");
                c.IncludeXmlComments(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), "fileserverapp.xml"));
            }).EnableSwaggerUi();

            //autofac config
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(WebApiConfig).Assembly);
            builder.RegisterType<FileServerService>().As<IFileServerService>();
            builder.RegisterType<FileSystemResourceFactory>().As<IResourceFactory>();
            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
