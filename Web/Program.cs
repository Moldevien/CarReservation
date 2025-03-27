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
            // ���������� ������ ���� �� ��������� ��������� � ������������
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ��������� ����������
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<CarService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<ClientService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // �������� ������ � ����� �������� �����������
            // ���� ������� �� debug �� ����� ���� ������������ � ��� ������� ����������� ���� ������������ ���� � ��������
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

            app.UseAuthorization();

            // �� �� ������ ������������� ��� url ������
            app.MapControllerRoute(
                name: "default",
                // ��� ������� ���� ����������� ��������� Home � ����� Index
                pattern: "{controller=Car}/{action=Index}/{id?}");

            app.Run();

            // �� ������ ����� ���� ��� ������� �����
            // �������� ������� �� ��������� �����, ��� � ����� views
            // ����������� ���� _ViewStart, ��� ������� �������� �������� ���� � �������� ����� _Layout
            // � ����� ���� ������� ������� � ��� ����������� ��������� �� ����� ��� ������� ���� ���������, ����� ��� ����� �� Index
        }
    }
}
