using TokenVault.Api;
using TokenVault.Application;
using TokenVault.Infrastructure;
using TokenVault.Api.RequestPipeline;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);    
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseGlobalErrorHandling();
    app.MapControllers();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TokenVault API v1");
    });

    app.Run();
}