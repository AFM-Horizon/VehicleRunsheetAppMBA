using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VehicleRunsheetMBA.Areas.Identity;
using VehicleRunsheetMBA.Data;
using VehicleRunsheetMBAProj.Data;
using VehicleRunsheetMBAProj.Data.Repositories;
using VehicleRunsheetMBAProj.Utilities;

namespace VehicleRunsheetMBA
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var server = Configuration["DB:Server"] ?? "ms-sql-server";
            var port = Configuration["DB:Port"] ?? "1433";
            var user = Configuration["DB:User"] ?? "SA";
            var password = Configuration["DbPassword"] ?? "Pa55w0rd2019";
            var database = Configuration["DB:Database"] ?? "Runsheet";
            //$"Server={server},{port};Initial Catalog={database};User ID={user};Password={password}"
            //"Server=ms-sql-server,1433;Initial Catalog=Runsheet;User ID=SA;Password=Pa55w0rd2019"
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer("Server=tcp:mbarunsheet.database.windows.net,1433;Initial Catalog=MBARunsheetDB;Persist Security Info=False;User ID=afmhorizon;Password=Pa55w0rd2019;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),ServiceLifetime.Transient);
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            services.AddTransient<CsvRunsheetWriter>();
            services.AddTransient<PdfWriter>();
            services.AddTransient<ITripRepository, TripRepository>();
            services.AddTransient<IRunsheetRepository, RunsheetRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            ConfigureDb.InitialMigration(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
