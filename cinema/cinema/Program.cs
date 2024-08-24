using cinema;
using cinema.Models;
using cinema.Services;
using DemoSession1_WebAPI.Converters;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using cinema.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
});

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' is not found.");
}

builder.Services.AddDbContext<MyDbContext>(option =>
    option.UseLazyLoadingProxies().UseMySQL(connectionString));

builder.Services.AddScoped<AccountService, AccountServiceImpl>();
builder.Services.AddScoped<MovieService, MovieServiceImpl>();
builder.Services.AddScoped<ShowTimeService, ShowTimeServiceImpl>();
builder.Services.AddScoped<ComboService, ComboServiceImpl>();
builder.Services.AddScoped<BookingService, BookingServiceImpl>();
builder.Services.AddScoped<PaymentService, PaymentServiceImpl>();
builder.Services.AddScoped<CinemaService, CinemaServiceImpl>();
builder.Services.AddScoped<ChatService, ChatServiceImpl>();
builder.Services.AddScoped<RatingService, RatingServiceImpl>();
builder.Services.AddScoped<RoomService, RoomServiceImpl>();
builder.Services.AddScoped<FollowService, FollowServiceImpl>();
builder.Services.AddScoped<SeatService, SeatServiceImpl>();
builder.Services.AddScoped<SubService, SubServiceImpl>();
builder.Services.AddScoped<MailHelper>();
builder.Services.AddScoped<SMSHelper>();
builder.Services.AddHostedService<MyBackgroundService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();

app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );

app.UseStaticFiles();
app.UseSession();
app.UseWebSockets();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await HandleWebSocketAsync(webSocket);
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    }
    else
    {
        await next();
    }
});

app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action}"
    );

await app.RunAsync();

static async Task HandleWebSocketAsync(WebSocket webSocket)
{
    var buffer = new byte[1024 * 4];
    WebSocketManager.AddClient(webSocket);

    try
    {
        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

            // Log the received message
            Console.WriteLine($"Received message: {message}");

            // Broadcast message to all connected clients except the sender
            foreach (var client in WebSocketManager.GetAllClients())
            {
                if (client != webSocket && client.State == WebSocketState.Open)
                {
                    await client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"WebSocket exception: {ex.Message}");
    }
    finally
    {
        WebSocketManager.RemoveClient(webSocket);
    }
}
