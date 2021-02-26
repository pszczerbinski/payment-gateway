namespace PaymentGateway.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PaymentGateway.Models;

    [Route("api/v1/[controller]")]
    public class PaymentGatewayController : Controller
    {
        [HttpPost]
        [Route("processpayment")]
        public IActionResult ProcessPayment([FromBody]PaymentRequest request)
        {
            return Ok();
        }
    }
}
