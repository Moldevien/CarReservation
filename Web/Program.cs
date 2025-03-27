using Application.Services;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // підключення сервісу який дає можливість працювати з контролерами
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Реєстрація репозиторіїв
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<CarService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<ClientService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // перевірка процес у якому розробка знаходиться
            // якщо вибрано не debug то умова буде виконуватись і при помилка користувача буде перекидувати сайт з помилкою
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // підключення переадресацій
            app.UseHttpsRedirection();
            // підключення статичних файлів у файлі wwwroot
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // як ми будемо відслідковувати різні url адреса
            app.MapControllerRoute(
                name: "default",
                // при запуску буде викликатися контролер Home і метод Index
                pattern: "{controller=Car}/{action=Index}/{id?}");

            app.Run();

            // як працює показ форм при запуску сайту
            // подається команда на показання форми, іде в папку views
            // запускається файл _ViewStart, там указано основний головний файл з шаблоном сайту _Layout
            // у цьому файлі указано команду в яку епередається посилання на форму яка повинна бути показанна, тобто при запку це Index
        }
    }
}
