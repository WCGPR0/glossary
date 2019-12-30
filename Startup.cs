using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace glossary
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            /**  <!-- Start of Setting GlossaryDataBaseSettings Binding -->
                <summary>
                 Uses Microsoft.Extensions.Options, 
                 registering appsettings.json's GlossaryDataBaseSettings in DI Container
                 and registering GlossaryService with MongoClient in DI with singleton service lifetime
                 </summary>
             */
            services.Configure<glossary.Models.GlossaryDatabaseSettings>(
                Configuration.GetSection(nameof(glossary.Models.GlossaryDatabaseSettings)));

            services.AddSingleton<glossary.Models.IGlossaryDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<glossary.Models.GlossaryDatabaseSettings>>().Value);

            services.AddSingleton<glossary.Services.GlossaryService>();

            /** <!-- End of Setting GlossaryDatabaseSettings Binding --> */

            services.AddCors();
            services.AddControllersWithViews();
            services.AddTransient<glossary.Controllers.GlossaryController>(); //< This allows Razor Pages / Controllers to use DI with GlossaryController
            services.AddRazorPages();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}"
                );
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
                }
            });
        }
    }
}
