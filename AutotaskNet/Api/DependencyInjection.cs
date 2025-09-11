using AutotaskNet.Domain.Requests;
using AutotaskNet.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace AutotaskNet.Api;

public static class DependencyInjection
{
    /// <summary>
    /// Registers the dependencies for the AutotaskNet library.
    /// </summary>
    public static void AddAutotask(this IServiceCollection services, Action<AutotaskCredentials> credentialsAction)
    {
        services.AddSingleton<AutotaskCredentials>(_ =>
        {
            var credentials = new AutotaskCredentials();
            credentialsAction(credentials);
            credentials.AssertValidState();
            return credentials;
        });

        services.AddScoped<IProxy, IProxy.Imp>();
        services.AddScoped<IAutotaskProxy, IAutotaskProxy.Imp>();
        services.AddScoped<IAutotaskService, AutotaskService>();
    }
}