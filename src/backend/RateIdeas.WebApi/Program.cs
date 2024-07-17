using MedX.Service.Extensions;
using RateIdeas.Application;
using RateIdeas.Infrastructure;
using RateIdeas.WebApi.Middlewares;

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

var app = builder.Build();

// Dark mode for Swagger    ------- Manual { Custom }
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RateIdeas");
    c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
});

// Init Accessor            ------- Custom
app.InitAccessor();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Access for static files  ------- Manual
app.UseStaticFiles();

// Exceptopn middleware     ------- Manual<Custom>
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
