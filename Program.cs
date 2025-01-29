using counselorReview.Services;
using counselorReview.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using counselorReview.Models;

var builder = WebApplication.CreateBuilder(args);

// Register MongoDB settings from appsettings.json
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));

// Inject MongoDB client
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// Register MongoDB database
builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});

// Register Services
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<CounselorService>();
builder.Services.AddScoped<FeedbackService>();

// Enable Authorization
builder.Services.AddAuthorization();

// Add controllers service
builder.Services.AddControllers();

// Set up Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable Authorization middleware
app.UseAuthorization();

// Map controllers to the app
app.MapControllers();

// Run the application
app.Run();
