using RateIdeas.Application;
using RateIdeas.Infrastructure;
using RateIdeas.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services             ------- Manual, Custom
builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebApiServices(builder.Configuration);

var app = builder.Build();

//                          ------- Custom
app.AddCustomizationFeatures();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
