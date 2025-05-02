using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // ���������� ������ ���� �� ��������� ��������� � ������������
            builder.Services
                .AddLocalization(options => options.ResourcesPath = "Resources")
                .AddControllersWithViews()
                .AddDataAnnotationsLocalization()
                .AddViewLocalization();

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                /*List<CultureInfo> locales = new List<CultureInfo>()
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("uk-UA")
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = locales;
                options.SupportedUICultures = locales;*/

                var locales = new[] { "en-US", "uk-UA" };

                options.SetDefaultCulture("en-US")
                    .AddSupportedCultures(locales)
                    .AddSupportedUICultures(locales);

                options.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider());
            });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDefaultIdentity<IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedAccount = true;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // ��������� ����������
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<CarService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<ClientService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            /* 
            // Configure the HTTP request pipeline.
            // �������� ������ � ����� �������� �����������
            // ���� ������� �� debug �� ����� ���� ������������ � ��� ������� ����������� ���� ������������ ���� � ��������*/
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // ���������� �������������
            app.UseHttpsRedirection();
            // ���������� ��������� ����� � ���� wwwroot
            app.UseStaticFiles();

            app.UseRouting();

            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseAuthentication();
            app.UseAuthorization();

            // �� �� ������ ������������� ��� url ������
            // ��� ������� ���� ����������� ��������� Home � ����� Index
            app.MapControllerRoute(
                name: "default",
                pattern: "{culture=en-US}/{controller=Car}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();

            /*
            // �� ������ ����� ���� ��� ������� �����
            // �������� ������� �� ��������� �����, ��� � ����� views
            // ����������� ���� _ViewStart, ��� ������� �������� �������� ���� � �������� ����� _Layout
            // � ����� ���� ������� ������� � ��� ����������� ��������� �� ����� ��� ������� ���� ���������, ����� ��� ����� �� Index*/
        }
    }
}
