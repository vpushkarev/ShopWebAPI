using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopWebAPI.Data;
using ShopWebAPI.Data.Interfaces;
using ShopWebAPI.Data.Interfaces.ShopWebAPI.Data.Interfaces;
using ShopWebAPI.Data.Models;
using ShopWebAPI.Data.Repository;

namespace ShopWebAPI
{
    public class Startup
    {
        private readonly IConfigurationRoot _confString;

        public Startup(IHostEnvironment hostEnvironment)
        {
            _confString = new ConfigurationBuilder().SetBasePath(hostEnvironment.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Регистрация контекста БД
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")
                //,b=>b.MigrationsAssembly((typeof(AppDbContext).Assembly.FullName))
            ));

            services.AddTransient<ICars, CarRepository>();
            services.AddTransient<ICategory, CategoryRepository>();
            services.AddTransient<IOrders, OrdersRepository>();
            services.AddTransient<IShopCart, ShopCartRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(ShopCart.GetCart);

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddControllers()
	            .AddNewtonsoftJson(options =>
		            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
	            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
	            app.UseHsts();
            }

            app.UseStatusCodePages();
            app.UseSession();
            //app.UseStaticFiles();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{Id?}");
            //    routes.MapRoute(name: "categoryFilter", template: "Cars/{action}/{category?}", defaults: new {action = "List"});
            //});

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                DbObjects.Initial(context);
            }
        }
    }
}