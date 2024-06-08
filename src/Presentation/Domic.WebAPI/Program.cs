using Domic.Core.Infrastructure.Extensions;
using Domic.Core.WebAPI.Extensions;
using Domic.Persistence.Contexts;
using Domic.WebAPI.Frameworks.Extensions;

/*-------------------------------------------------------------------*/

WebApplicationBuilder builder = WebApplication.CreateBuilder();

#region Configs

builder.WebHost.ConfigureAppConfiguration((context, builder) => builder.AddJsonFiles(context.HostingEnvironment));

#endregion

/*-------------------------------------------------------------------*/

#region ServiceContainer

builder.RegisterHelpers();
builder.RegisterELK();
builder.RegisterEntityFrameworkCoreCommand<SQLContext, string>();
builder.RegisterCommandQueryUseCases();
builder.RegisterJsonWebToken();
builder.RegisterMessageBroker();
builder.RegisterDistributedCaching();
builder.RegisterServicesOfGrpcClientWebRequest();
builder.RegisterServices();
builder.RegisterServiceDiscovery();

builder.Services.AddMvc();
builder.Services.AddApiVersioning();
builder.Services.AddHttpContextAccessor();
builder.Services.AddGrpc();
builder.Services.AddCustomSwagger();

#endregion

/*-------------------------------------------------------------------*/

WebApplication application = builder.Build();

/*-------------------------------------------------------------------*/

#region Middleware

application.UsePreFlightCors();

application.UseCoreExceptionHandler(application.Configuration);

if (application.Environment.IsProduction())
{
    application.UseHsts();
    application.UseHttpsRedirection();
}

application.UseCustomSwagger(application.Environment);

application.UseRouting();

application.UseCors("CORS");

application.UseAuthentication();

application.UseAuthorization();

application.UseEndpoints(endpoints => {
    
    endpoints.HealthCheck(application.Services);

    endpoints.MapControllers();

});

#endregion

/*-------------------------------------------------------------------*/

application.Run();

/*-------------------------------------------------------------------*/

//For Integration Test

public partial class Program;