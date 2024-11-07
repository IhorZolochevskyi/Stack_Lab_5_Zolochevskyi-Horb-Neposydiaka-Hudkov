using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;      // Для IApplicationBuilder
using Microsoft.AspNetCore.Hosting;      // Для IWebHostEnvironment
using Microsoft.AspNetCore.Routing;
using lab5.Services;      // Для MapControllerRoute

namespace lab5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Создаем область для работы с сервисами
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    // Получаем контекст базы данных и инициализируем его
                    var context = services.GetRequiredService<ParksContext>();
                    SampleData.Initialize(context);
                }
                catch (Exception ex)
                {
                    // Логирование ошибки при инициализации базы данных
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            // Запускаем приложение
            host.Run();
        }

        // Метод для создания хоста
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((context, services) =>
                    {
                        // Получение строки подключения
                        string connection = context.Configuration.GetConnectionString("DefaultConnection");

                        // Добавление контекста базы данных и контроллеров с представлениями
                        services.AddDbContext<ParksContext>(options => options.UseSqlServer(connection));
                        services.AddControllersWithViews();
                    })
                    .Configure(app =>
                    {
                        // Конфигурация пайплайна HTTP-запросов
                        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

                        if (!env.IsDevelopment())
                        {
                            app.UseExceptionHandler("/Home/Error");
                            app.UseHsts();
                        }

                        app.UseHttpsRedirection();
                        app.UseStaticFiles();
                        app.UseRouting();
                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllerRoute(
                                name: "default",
                                pattern: "{controller=Home}/{action=Index}/{id?}");
                        });
                    });
                });
    }
}
