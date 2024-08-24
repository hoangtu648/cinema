using System;
using System.Threading;
using System.Threading.Tasks;
using cinema;
using cinema.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class MyBackgroundService : BackgroundService
{
    private readonly ILogger<MyBackgroundService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public MyBackgroundService(ILogger<MyBackgroundService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Background Service is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Background Service is doing work.");

            // Tạo scope mới để sử dụng các dịch vụ scoped
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var followService = scope.ServiceProvider.GetRequiredService<FollowService>();
                var showTimeService = scope.ServiceProvider.GetRequiredService<ShowTimeService>();
                var mailhelper = scope.ServiceProvider.GetRequiredService<MailHelper>();
                
                var follows = followService.findAll();
               
              
                foreach (var follow in follows)
                {
                    var showTimeHTML = "";
                    foreach (var showTime in showTimeService.listMovieTomorrow())
                    {
                       
                        showTimeHTML += " <tr><td style='padding: 10px; vertical-align: top; text-align: left;'>  <h3 style='margin: 0; font-size: 18px; color: #555;'>" + showTime.Title + "</h3></td><td style='padding: 10px; text-align: center;'><img src='" + showTime.Photo + "' height='200' width='200' alt='Superman Poster' style='border: 1px solid #ddd; border-radius: 5px;'></td><td style='padding: 10px; text-align: center;'> Suất chiếu: " + showTime.ShowDate + " " + showTime.ShowTime + " </td></tr>";

                    }
                    var body = "<div style='border: 1px solid #ccc; padding: 20px; max-width: 600px; margin: auto; text-align: center; font-family: Arial, sans-serif;'><h2 style=\"color: #333;\">Các bộ phim sẽ chiếu vào ngày mai</h2>\r\n    <hr style='border: none; border-top: 1px solid #eee; margin: 20px 0;'>    <table style='width: 100%; border-collapse: collapse; margin-top: 20px;'> " + showTimeHTML + "</table>    <a href='http://localhost:4200/cinema' style='display: inline-block; margin-top: 20px; padding: 10px 20px; font-size: 16px; color: #fff; background-color: #007BFF; text-decoration: none; border-radius: 5px;'>Đặt vé ngay</a></div>";

                    mailhelper.Send("atun123456789cu@gmail.com", follow.Email, "Thông báo lịch chiếu phim ngày mai", body);
                 
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(24*60*60), stoppingToken);
        }

        _logger.LogInformation("Background Service is stopping.");
    }


    public override Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Background Service is stopping.");
        return base.StopAsync(stoppingToken);
    }
}
