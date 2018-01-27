using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TinyPermissionsLib.Sample.Data;
using TinyPermissionsLib.EFCoreProvider;
using TinyPermissionsLib.Sample.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System.Threading;

namespace TinyPermissionsLib.Sample.WebApi
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
            services.AddMvc();

            services.AddDbContextWithPermissions<DuckContext>((obj) => obj.UseSqlite(
                Configuration.GetConnectionString("SampleDb"),
                b => b.MigrationsAssembly("TinyPermissions.Sample.WebApi")));
       }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DuckContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // do magic
            BasicPermissionService.SetBasicPermissions(context);

            app.UseMvc();
        }
    }
}
