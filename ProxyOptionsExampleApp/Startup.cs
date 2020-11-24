namespace ProxyOptionsExampleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpClient<MyExampleHttpClient>()
                .ConfigurePrimaryHttpMessageHandler(provider =>
                {
                    var options = provider.GetRequiredService<IOptionsSnapshot<MyExampleOptions>>().Value;

                    var httpMessageHandler = new HttpClientHandler();

                    //
                    // თუ პროქსი დაკონფიგურებულია:
                    //
                    if (!string.IsNullOrWhiteSpace(options.ProxyHost))
                    {
                        httpMessageHandler.UseProxy = true;
                        httpMessageHandler.Proxy = new WebProxy(options.ProxyHost, options.ProxyPort)
                        {
                            BypassProxyOnLocal    = true,
                            Credentials           = options.ProxyCredentials,
                            UseDefaultCredentials = options.ProxyCredentials == null,
                        };
                    }
                    else
                    {
                        httpMessageHandler.UseProxy = false;
                    }

                    return httpMessageHandler;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
