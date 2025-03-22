var builder = WebApplication.CreateBuilder(args);

//Adds a singleton for the cube puzzle, ensures only one is given
builder.Services.AddSingleton<LibNetCube.CubePuzzle>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
