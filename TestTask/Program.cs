using TestTask.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Custom configuration
builder.Services
    .AddServices()
    .AddMappingProfiles()
    .AddRepositories()
    .AddValidators()
    .AddDatabase();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await app.SetupDefaultData();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseWebSockets();
app.MapControllers();

app.Run();