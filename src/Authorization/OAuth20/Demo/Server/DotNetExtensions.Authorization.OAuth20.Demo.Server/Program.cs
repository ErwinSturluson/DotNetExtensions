// Developed and maintained by Erwin Sturluson.
// Erwin Sturluson licenses this file to you under the MIT license.

using DotNetExtensions.Authorization.OAuth20.Server;
using DotNetExtensions.Authorization.OAuth20.Server.Abstractions.Flows;
using DotNetExtensions.Authorization.OAuth20.Server.Flows.AuthorizationCode;

namespace DotNetExtensions.Authorization.OAuth20.Demo.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddOAuth20Server().SetOAuth20Flow<IFlow, DefaultAuthorizationCodeFlow>(null, null, null); // TODO

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseOAuth20Server();
        app.MapControllers();

        app.Run();
    }
}
