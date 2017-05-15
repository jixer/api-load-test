using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebAPIApplication
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //app.UseMvc();
            app.Run(async (context) => {
                var req = context.Request;
                var reqBody = req.Body;
                var resp = context.Response;

                if (req.Method == "POST")
                {
                    byte[] bData = new byte[2048];
                    int len = await reqBody.ReadAsync(bData, 0, bData.Length);
                    string sData = ASCIIEncoding.ASCII.GetString(bData, 0, len);
                    Contact c = JsonConvert.DeserializeObject<Contact>(sData);

                    string sRespMessage = $"Hello {c.FirstName} {c.LastName} from .NET Core!";
                    var oResponse = new SimpleResult{
                        Result = sRespMessage
                    };
                    string sResp = JsonConvert.SerializeObject(oResponse);
                    byte[] bResp = ASCIIEncoding.ASCII.GetBytes(sResp);

                    resp.ContentType = "application/json";
                    resp.StatusCode = 201;
                    await resp.Body.WriteAsync(bResp, 0, bResp.Length);
                }
                else 
                {
                    string sResp = "Hello from .NET Core!";
                    byte[] bResp = ASCIIEncoding.ASCII.GetBytes(sResp);
                    await context.Response.Body.WriteAsync(bResp, 0, bResp.Length);
                }
            });
        }
    }
}
