var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Register your app's services (Dependency Injection)
builder.Services.AddScoped<TaskManager.Api.Repositories.UserRepository>();
builder.Services.AddScoped<TaskManager.Api.Services.UserService>();

builder.Services.AddScoped<TaskManager.Api.Repositories.TaskRepository>();
builder.Services.AddScoped<TaskManager.Api.Services.TaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();