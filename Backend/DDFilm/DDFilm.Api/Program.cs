using DDFilm.Api;
using DDFilm.Api.Infrastructure;
using DDFilm.Application;
using DDFilm.Infrastructure;
using DDFilm.Infrastructure.EF.Context;
using DDFilm.Infrastructure.Hubs;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructure(builder.Configuration)
                .AddPresentation()
                .AddApplication();

builder.Host.AddSerilogLogging();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policyBuilder =>
    {
        policyBuilder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("https://ddfilmclient-adbcbwega3chb4ca.polandcentral-01.azurewebsites.net");
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();
app.UseSwaggerUI();

using var serviceScope = app.Services.CreateScope();
using var dbContext = serviceScope.ServiceProvider.GetService<WriteDbContext>();
dbContext?.Database.Migrate();

app.UseHttpsRedirection();

app.MapHub<SessionHub>("/sessions/session-hub");

app.MapHub<RatingHub>("/sessions/rating-hub");

app.MapHub<WatchingHub>("/sessions/watching-hub");

app.UseMiddleware<RequestLogContextMiddleware>();
app.UseCors("CorsPolicy");
app.UseSerilogRequestLogging();

app.UseAuthentication();

app.UseAuthorization();

app.UseExceptionHandler(_ => { });

app.MapControllers();

app.Run();