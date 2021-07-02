using Authentication.Areas.Identity;
using RewatchIt.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RewatchIt.Services;
using Syncfusion.Blazor;
using Syncfusion.Licensing; //using Radzen;

namespace RewatchIt
{
    public class Startup
    {
        #region Constructors

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region Properties

        public IConfiguration Configuration { get; }

        #endregion

        #region Methods

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //.Net Services
            services.AddRazorPages();
            services.AddServerSideBlazor();

            // My Services
            services.AddSingleton<TmdbService>();

            //// Radzen Services
            //services.AddScoped<DialogService>();
            //services.AddScoped<NotificationService>();
            //services.AddScoped<TooltipService>();
            //services.AddScoped<ContextMenuService>();

            // Syncfusion Services
            services.AddSyncfusionBlazor();

            // Database & EF.            
            services.AddDbContextFactory<WatchedMovieContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MasterConnection")));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AuthenticationConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            // Authentication & Authorization
            services.AddIdentityCore<IdentityUser>();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            SyncfusionLicenseProvider.RegisterLicense("NDYzNzcyQDMxMzkyZTMxMmUzMGk4VTFXR3N1dCs2UW03T1A3NzZxblhXaFNTQTgzamU5cWUzVmZVOGhCaGs9");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        #endregion
    }
}