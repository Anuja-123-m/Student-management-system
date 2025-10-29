namespace StudentRecordManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            builder.Services.AddAuthorization();
            builder.Services.AddScoped<StudentRecordManagementSystem.Repository.IStudentRepository, StudentRecordManagementSystem.Repository.StudentRepository>();
            builder.Services.AddScoped<StudentRecordManagementSystem.Repository.IUserRepository, StudentRecordManagementSystem.Repository.UserRepository>();
            builder.Services.AddScoped<StudentRecordManagementSystem.Repository.IRoleRepository, StudentRecordManagementSystem.Repository.RoleRepository>();
            builder.Services.AddScoped<StudentRecordManagementSystem.Service.IUserService, StudentRecordManagementSystem.Service.UserService>();
            builder.Services.AddScoped<StudentRecordManagementSystem.Service.IStudentService, StudentRecordManagementSystem.Service.StudentService>();
            builder.Services.AddScoped<StudentRecordManagementSystem.Service.IRoleService, StudentRecordManagementSystem.Service.RoleService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
