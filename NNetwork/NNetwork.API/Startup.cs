using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NNetwork.API.Hubs;
using NNetwork.Application.Services;
using NNetwork.Domain.Models;
using NNetwork.Domain.Repositories;
using NNetwork.Domain.Services;
using NNetwork.Infrastructure.Repositories;
using NNetwork.KerasApplication.Datasets;
using NNetwork.KerasApplication.Networks;

namespace NNetwork
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
            services.AddControllers(options =>{})
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins("http://localhost:3000")
                .WithOrigins("https://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
            services.AddCors();

            services.AddScoped<INetworkService, NetworkService>();
            services.AddScoped<INetworkRepository, NetworkRepository>();
            services.AddSingleton<INetworkStore, NetworkStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");
            /*
            app.UseSignalR(routes =>
            {
                routes.MapHub<NetworkHub>("/network");
            });
            */
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NetworkHub>("/network");
            });
            InitializeNetwork(app.ApplicationServices);
        }

        private void InitializeNetwork(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var networkStore = provider.GetRequiredService<INetworkStore>();

                var jsonModel = File.ReadAllText("cifar10_relu_model.json");
                var weights = File.ReadAllBytes("cifar10_relu_model_weights.h5");
                Task.Run(()=>
                {
                    var network = new Network();
                    network.InitializeNetwork(jsonModel, weights);

                    var networkContainer = new NetworkContainer()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = "Default Network",
                        Description = "CIFAR10 Default Network",
                        Network = network,
                        ModelConfiguration = jsonModel,
                        ModelWeights = weights,
                        PlotImage = Convert.ToBase64String(network.GetPlot())
                    };
                    networkStore.Networks.TryAdd(networkContainer.Id, networkContainer);
                });
            }
        }
    }
}
