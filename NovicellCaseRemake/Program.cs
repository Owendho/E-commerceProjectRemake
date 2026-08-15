using Microsoft.EntityFrameworkCore;
using NovicellCaseRemake;

using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath).AddOcelot();
builder.Services.AddOcelot(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var baseUrl = builder.Configuration["ErpSettings:BaseUrl"];

builder.Services.AddDbContext<NovicellAppDBContext>(options => options.UseSqlServer(connectionString)); //dependency injects the database context

builder.Services.AddHttpClient("ERPClient", client =>
{
    client.BaseAddress = new Uri(baseUrl);
    //client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
});

builder.Services.AddHostedService<ERPData>(); //singleton.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy => policy.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader());
}); 

var app = builder.Build();

app.UseCors("AllowReactApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.UseOcelot();

app.Run();
