var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IIconService, IconService>();


var app = builder.Build();

app.UseCors("AllowAll");

app.MapGet("/api/users", (IUserService userService) => Results.Ok(userService.GetAllUsers()));

app.MapPost("/api/users/reset-balance", (IUserService userService) =>
{
    userService.ResetBalances();
    return Results.Ok();
});

app.MapGet("/api/icons/{iconName}", (IIconService iconService, string iconName) =>
{
    var icon = iconService.GetIcon(iconName);
    return icon is not null ? Results.File(icon, "image/png") : Results.NotFound();
});

// Ensure app runs
app.Run();