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

        public async Task<PaymentDetails> GetPaymentDetailsAsync(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new ArgumentNullException(nameof(identifier));
            }

            return await this.ExecuteGetAsyncRestCall<PaymentDetails>(
                $"api/v1/paymentgateway/retrievepayment/{identifier}");
        }

        private async Task<TResp> ExecutePostAsyncRestCall<TReq, TResp>(string relativeUrl, TReq request)
        {
            var response = await this.httpClient.PostAsJsonAsync(relativeUrl, request);
            return await this.GetTypeFromContent<TResp>(response.Content);
        }

        private async Task<T> ExecuteGetAsyncRestCall<T>(string relativeUrl)
        {
            var response = await this.httpClient.GetAsync(relativeUrl);
            return await this.GetTypeFromContent<T>(response.Content);
        }

        private async Task<T> GetTypeFromContent<T>(HttpContent content)
        {
            try
            {
                return await content.ReadFromJsonAsync<T>();
            }
            catch (JsonException)
            {
                return default;
            }
        }
    }
}
