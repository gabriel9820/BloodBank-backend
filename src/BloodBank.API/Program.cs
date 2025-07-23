using BloodBank.API;
using BloodBank.Application;
using BloodBank.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApi(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors(options => options
    .WithOrigins([builder.Configuration["Jwt:Audience"]!])
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
