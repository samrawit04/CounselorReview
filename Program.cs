using counselorReview.Services;
using counselorReview.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using counselorReview.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));


builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});


builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});


builder.Services.AddScoped<IClientService,ClientService>();
builder.Services.AddScoped<ICounselorService,CounselorService>();
builder.Services.AddScoped<IFeedbackService,FeedbackService>();

builder.Services.AddAuthorization();


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
