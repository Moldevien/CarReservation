using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Web
{
    public class Program
    {
        public static async Task Main(string[] args)
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

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            // ��������� ����������
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<CarService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await SeedRoles(services);
            }

            /* 
            // Configure the HTTP request pipeline.
            // �������� ������ � ����� �������� �����������
            // ���� ������� �� debug �� ����� ���� ������������ � ��� ������� ����������� ���� ������������ ���� � ��������*/
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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

            app.Run();

            /*
            // �� ������ ����� ���� ��� ������� �����:
            // �������� ������� �� ��������� �����, ��� � ����� views
            // ����������� ���� _ViewStart, ��� ������� �������� �������� ���� � �������� ����� _Layout
            // � ����� ���� ������� ������� � ��� ����������� ��������� �� ����� ��� ������� ���� ���������, ����� ��� ����� �� Index*/
        }
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // (�������) �������� ������� ����� ������:
            string adminEmail = "admin@example.com";
            string adminPassword = "Admin123!";

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
            if (existingAdmin == null)
            {
                var adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
