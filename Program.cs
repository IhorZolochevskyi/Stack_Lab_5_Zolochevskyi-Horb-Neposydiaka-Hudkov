using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;      // ��� IApplicationBuilder
using Microsoft.AspNetCore.Hosting;      // ��� IWebHostEnvironment
using Microsoft.AspNetCore.Routing;
using lab5.Services;      // ��� MapControllerRoute

namespace lab5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // ������� ������� ��� ������ � ���������
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    // �������� �������� ���� ������ � �������������� ���
                    var context = services.GetRequiredService<ParksContext>();
                    SampleData.Initialize(context);
                }
                catch (Exception ex)
                {
                    // ����������� ������ ��� ������������� ���� ������
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            // ��������� ����������
            host.Run();
        }

        // ����� ��� �������� �����
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((context, services) =>
                    {
                        // ��������� ������ �����������
                        string connection = context.Configuration.GetConnectionString("DefaultConnection");

                        // ���������� ��������� ���� ������ � ������������ � ���������������
                        services.AddDbContext<ParksContext>(options => options.UseSqlServer(connection));
                        services.AddControllersWithViews();
                    })
                    .Configure(app =>
                    {
                        // ������������ ��������� HTTP-��������
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
