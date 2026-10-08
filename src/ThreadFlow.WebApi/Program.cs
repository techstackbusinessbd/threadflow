using ThreadFlow.Application;
using ThreadFlow.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ThreadFlow ERP API", Version = "v1" });
});

// Clean Architecture Layer Services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ThreadFlow ERP API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseRouting();
app.UseAuthorization();

app.MapGet("/", () => Results.Ok(new
{
    System = "ThreadFlow ERP",
    Version = "1.0.0",
    Status = "Online",
    Environment = app.Environment.EnvironmentName,
    Timestamp = DateTimeOffset.UtcNow
}));

app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));

app.MapControllers();

app.Run();
