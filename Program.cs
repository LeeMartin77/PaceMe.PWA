using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaceMe.BlazorApp.Services;
using PaceMe.BlazorApp.Properties;
using Microsoft.AspNetCore.Components;

namespace PaceMe.BlazorApp
{
    public class PaceMeAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public PaceMeAuthorizationMessageHandler(IAccessTokenProvider provider,
            NavigationManager navigationManager, 
            IConfiguration configuration)
            : base(provider, navigationManager)
        {
            ConfigureHandler(
                authorizedUrls: new[] { configuration[PaceMeFunctionAppConfiguration.PaceMeFunctionAppUrl] },
                scopes: new[] { configuration[PaceMeFunctionAppConfiguration.PaceMeFunctionAppAccessScope] }
                );
        }
    }   
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            string functionApi = builder.Configuration.GetValue<string>(PaceMeFunctionAppConfiguration.PaceMeFunctionAppUrl);
            string functionApiScope = builder.Configuration.GetValue<string>(PaceMeFunctionAppConfiguration.PaceMeFunctionAppAccessScope);
            // https://docs.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/additional-scenarios?view=aspnetcore-5.0
            builder.Services.AddScoped<PaceMeAuthorizationMessageHandler>();

            builder.Services.AddHttpClient("FunctionApiAnonymous", 
                client => {client.BaseAddress = new Uri(functionApi);});

            builder.Services.AddHttpClient("FunctionApi", 
                client => {client.BaseAddress = new Uri(functionApi);})
               .AddHttpMessageHandler<PaceMeAuthorizationMessageHandler>();

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
               .CreateClient("FunctionApiAnonymous"));

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
               .CreateClient("FunctionApi"));

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add(functionApiScope);
                options.ProviderOptions.LoginMode = "redirect";
            });
        
            builder.Services.AddScoped<ITrainingPlanService, TrainingPlanService>();
            builder.Services.AddScoped<ITrainingPlanActivityService, TrainingPlanActivityService>();
            builder.Services.AddScoped<ITrainingPlanActivitySegmentService, TrainingPlanActivitySegmentService>();

            await builder.Build().RunAsync();
        }
    }

    public class FunctionApiAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public FunctionApiAuthorizationMessageHandler(IAccessTokenProvider provider, 
            NavigationManager navigationManager, IConfiguration configuration)
            : base(provider, navigationManager)
        {
            ConfigureHandler(
                authorizedUrls: new[] { configuration[PaceMeFunctionAppConfiguration.PaceMeFunctionAppUrl] },
                scopes: new[] { configuration[PaceMeFunctionAppConfiguration.PaceMeFunctionAppAccessScope] }
                );
        }
    }
}
