using Application.Services;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // підключення сервісу який дає можливість працювати з контролерами
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

            // Реєстрація репозиторіїв
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<CarService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<ClientService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            /* 
            // Configure the HTTP request pipeline.
            // перевірка процес у якому розробка знаходиться
            // якщо вибрано не debug то умова буде виконуватись і при помилка користувача буде перекидувати сайт з помилкою*/
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // підключення переадресацій
            app.UseHttpsRedirection();
            // підключення статичних файлів у файлі wwwroot
            app.UseStaticFiles();

            app.UseRouting();

            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseAuthentication();

            // як ми будемо відслідковувати різні url адреса
            // при запуску буде викликатися контролер Home і метод Index
            app.MapControllerRoute(
                name: "default",
                pattern: "{culture=en-US}/{controller=Car}/{action=Index}/{id?}");

            app.Run();

            /*
            // як працює показ форм при запуску сайту:
            // подається команда на показання форми, іде в папку views
            // запускається файл _ViewStart, там указано основний головний файл з шаблоном сайту _Layout
            // у цьому файлі указано команду в яку епередається посилання на форму яка повинна бути показанна, тобто при запку це Index*/
        }
    }
}
