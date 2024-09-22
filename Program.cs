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

        //�������ڲ�ͬ

        //��Ŀ������ʼ��ֹͣ����
        //services.AddSingleton<MajorRepo>();
        //��̣�������ʹ�����������
        //services.AddTransient<MajorRepo>();

        //���ʿ�����ʱʹ�ã��û���һ��������Ϊ��������
        services.AddScoped<MajorRepo>();
        services.AddScoped<ClassRepo>();
        services.AddScoped<GradeRepo>();
        services.AddScoped<StudentRepo>();
        //ʹ��ʱע�⣬��ΪAddDbContext�ὫAppDbContextע��ΪScoped���ͷ���
        //��MajorRepo������AppDbContext�����MajorRepo�ı����ڲ������õı�Scoped����ֻ�ܱ���һ�»����

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
