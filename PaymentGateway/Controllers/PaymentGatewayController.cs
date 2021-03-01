namespace PaymentGateway.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using global::PaymentGateway.Models;
    using global::PaymentGateway.Extensions;

    [Route("api/v1/[controller]")]
    public class PaymentGatewayController : Controller
    {
        private readonly IPaymentGateway paymentGateway;

        public PaymentGatewayController(IPaymentGateway paymentGateway)
        {
            this.paymentGateway = paymentGateway ??
                throw new ArgumentNullException(nameof(paymentGateway));
        }

        [HttpPost]
        [Route("processpayment")]
        public async Task<ActionResult<PaymentResponse>> PostProcessPayment([FromBody]PaymentRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var result = request.Validate();
            if (result != PaymentRequestValidationResult.Success)
            {
                return BadRequest(new PaymentResponse { Error = result.ToString() });
            }

            var response = await this.paymentGateway.ProcessPaymentRequestAsync(request);

            return Ok(response);
        }

        [HttpGet]
        [Route("retrievepayment/{identifier}")]
        public ActionResult<PaymentDetails> GetPaymentDetails(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                return BadRequest("Payment identifier must be provided");
            }

            var response = this.paymentGateway.RetrievePaymentDetails(identifier);
            return Ok(response);
        }
    }
}
