namespace PaymentGateway.IntegrationTests
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading.Tasks;
    using global::PaymentGateway.Models;

    public class PaymentGatewayApi
    {
        private readonly HttpClient httpClient;

        public PaymentGatewayApi(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentNullException(nameof(address));
            }

            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri(address),
            };
        }

        public async Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await this.ExecutePostAsyncRestCall<PaymentRequest, PaymentResponse>(
                "api/v1/paymentgateway/processpayment",
                request);
        }

        private async Task<TResp> ExecutePostAsyncRestCall<TReq, TResp>(string relativeUrl, TReq request)
        {
            var response = await this.httpClient.PostAsJsonAsync(relativeUrl, request);
            try
            {
                return await response.Content.ReadFromJsonAsync<TResp>();
            }
            catch (JsonException)
            {
                return default;
            }
        }
    }
}
