namespace PaymentGateway
{
    using Bank;
    using global::PaymentGateway.Stubs;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup()
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Here we can switch for real bank.
            services.AddScoped<IBank, BankStub>();
            services.AddScoped<IPaymentGateway, PaymentGateway>();
            services.AddSingleton<IPaymentStorage, PaymentStorage>();

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