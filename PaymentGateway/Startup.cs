namespace PaymentGateway
{
    using Bank;
    using global::PaymentGateway.Storage;
    using global::PaymentGateway.Stubs;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var dataBaseSettings = new DatabaseSettings();
            this.configuration.GetSection("DatabaseSettings").Bind(dataBaseSettings);
            dataBaseSettings.Validate();

            services.AddSingleton<IDatabaseSettings>(dataBaseSettings);
            services.AddSingleton<IMongoPaymentStorageContext, MongoPaymentStorageContext>();

            // Here we can switch storage implementations.
            // DEV - services.AddSingleton<IPaymentStorage, PaymentStorage>();
            services.AddSingleton<IPaymentStorage, MongoPaymentStorage>();

            // Here we can switch for real bank.
            services.AddScoped<IBank, BankStub>();
            services.AddScoped<IPaymentGateway, PaymentGateway>();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    var rootApiString =
                        "# Payment Gateway #\n" +
                        "Available APIs:\n" +
                        "POST - api/v1/paymentgateway/processpayment - Processes a payment request\n" +
                        "GET - api/v1/paymentgateway/retrievepayment/{identifier} - Retrieve details of previous payment using the payment identifier";

                    await context.Response.WriteAsync(rootApiString);
                });
            });
        }
    }
}