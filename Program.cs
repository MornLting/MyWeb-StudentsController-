using Microsoft.AspNetCore.Identity;
using MyWeb.Data;
using MyWeb.Repositories;
using MyWeb.Service;

namespace MyWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        IServiceCollection services = builder.Services;
        services.AddControllersWithViews();
        services.AddDbContext<AppDbContext>();

        services.AddIdentity<IdentityUser, IdentityRole>().
            AddEntityFrameworkStores<AppDbContext>();

        //生命周期不同

        //项目启动开始，停止结束
        //services.AddSingleton<MajorRepo>();
        //最短，产生后，使命结束便结束
        //services.AddTransient<MajorRepo>();

        //访问控制器时使用，用户的一次请求内为生命周期
        services.AddScoped<MajorRepo>();
        services.AddScoped<ClassRepo>();
        services.AddScoped<GradeRepo>();
        services.AddScoped<StudentRepo>();
        //使用时注意，因为AddDbContext会将AppDbContext注册为Scoped类型服务。
        //而MajorRepo中依赖AppDbContext，因此MajorRepo的保质期不能设置的比Scoped长，只能保持一致或更短

        services.AddScoped<MajorService>();
        services.AddScoped<GradeService>();
        services.AddScoped<ClassService>();
        services.AddScoped<StudentService>();

        services.AddScoped<SearchService>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
        });
        
        services.AddTransient<IdentityDataSeeder>();
        
        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var identityDataSeeder = serviceProvider.GetRequiredService<IdentityDataSeeder>();
            identityDataSeeder.Seed().Wait();
        }
        app.UseDeveloperExceptionPage();
        app.UseStatusCodePagesWithReExecute("/error/{0}");
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapDefaultControllerRoute();

        app.Run();
    }
}
