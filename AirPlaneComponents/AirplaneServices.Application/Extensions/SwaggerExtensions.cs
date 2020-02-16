using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AirplaneServices.Application.Extensions
{
    public static class SwaggerExtensions
    {
        private static string ApiName => $"{""} - {""}";

        public static void SetSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(config =>
            {
                config.TagActionsBy(api => api.GroupBySwaggerGroupAttribute());
                config.SetXmlDocumentation();
            });
        }

        public static void SetSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider versionProvider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.ConfigureSwaggerUI(versionProvider);
            });
        }

        private static void SetXmlDocumentation(this SwaggerGenOptions options)
        {
            var xmlDocumentPath = GetXmlDocumentPath();
            var existsXmlDocument = File.Exists(xmlDocumentPath);
            if (existsXmlDocument)
            {
                options.IncludeXmlComments(xmlDocumentPath);
            }
        }

        private static string GetXmlDocumentPath()
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            return Path.Combine(AppContext.BaseDirectory, xmlFile);
        }

        private static IList<string> GroupBySwaggerGroupAttribute(this ApiDescription api)
        {
            var groupName = string.Empty;

            if (api.TryGetMethodInfo(out MethodInfo methodInfo))
            {
                var attribute = methodInfo.DeclaringType.CustomAttributes.SingleOrDefault(o => o.AttributeType.Name.Equals("SwaggerGroupAttribute"));
                groupName = attribute?.ConstructorArguments?.FirstOrDefault().Value?.ToString();
            }

            if (!string.IsNullOrEmpty(groupName))
            {
                return new List<string> { groupName };
            }
            else
            {
                var actionDescriptor = api.GetProperty<ControllerActionDescriptor>();
                if (actionDescriptor == null)
                {
                    actionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                    api.SetProperty(actionDescriptor);
                }
                return new List<string> { actionDescriptor?.ControllerName };
            }
        }

        private static void ConfigureSwaggerUI(this SwaggerUIOptions swaggerUIOptions, IApiVersionDescriptionProvider versionProvider)
        {
            var swaggerJsonBasePath = string.IsNullOrWhiteSpace(swaggerUIOptions.RoutePrefix) ? "." : "..";

            foreach (var apiVersion in versionProvider.ApiVersionDescriptions)
            {
                swaggerUIOptions.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v{apiVersion.ApiVersion}/swagger.json", $"{ApiName} - v{apiVersion.ApiVersion}");
            }

            swaggerUIOptions.RoutePrefix = "swagger";
            swaggerUIOptions.DocExpansion(DocExpansion.List);
        }
    }

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        public static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var build = Assembly.GetEntryAssembly().GetName().Version.ToString();
            var apiName = $"{""} - {""}";
            var info = new OpenApiInfo()
            {
                Title = apiName,
                Version = description.ApiVersion.ToString(),
                Description = $"{apiName} - build: {build}",
            };

            info.Description += description.IsDeprecated ? $" - <strong> {""} </strong>" : string.Empty;

            return info;
        }
    }

    public sealed class SwaggerGroupAttribute : Attribute
    {
        public string GroupName { get; }

        public SwaggerGroupAttribute(string groupName) => GroupName = groupName;
    }
}
