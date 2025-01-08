using Application.Extensions;
using Infrastructure.Extensions;
using Persistence.Extensions;
using WebApi.Auth;
using WebApi.Emails;
using WebApi.Posts;
using WebApi.Search;
using WebApi.Tags;
using WebApi.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPesistenceServices(builder.Configuration);

// CORS
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(options =>
{
    options.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:5173");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Endpoints
app.AddAuthEnpointsExtension();
app.AddUserEnpointsExtension();
app.AddPostEndpointsExtension();
app.AddTagEndpointsExtension();
app.AddEmailEndpointsExtension();
app.AddSearchEndpointsExtension();

app.Run();
