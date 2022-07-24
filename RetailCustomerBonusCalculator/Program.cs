using BusinessService;
using DataAccessLayerService;
using Serilog;
using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseSerilog((ctx,lc) => {
    lc.MinimumLevel.Information().WriteTo.File("logs/RetailCustomerBonusCalculator--.txt", rollingInterval: RollingInterval.Minute);
});


// Gets Connection String
var conString = builder.Configuration.GetConnectionString("MyConnection");
SqlHelper.connectionString = conString;



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddTransient<ICustomerDataService, CustomerDataService>();
builder.Services.AddTransient<ICustomerTransactionDataRepository, CustomerTransactionDataRepository>();

var app = builder.Build();

//health check endpoint
app.MapHealthChecks("/health");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = "RetailCustomerBonusCalculator";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RetailCustomerBonusCalculator Application V1 ");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
