# payment-gateway

Payment processing gateway implementation.

## Getting started
To start the application in a Docker container run the following command from the root directory:
```
docker-compose up
```
When the docker container is running, the API will be available on *http://localhost:5000/*.

*Note:* The implemented bank stub will decline all EUR payments and any GBP payments over £1000.

### APIs
Payment Gateway controller has been implemented to validate the payment requests and fail fast if any fields in the payment request fail validation.

The follwing APIs have been implemented:

__POST__ - *api/v1/paymentgateway/processpayment*

This API should be called with a JSON body containing the following fields (with example values):
```json
{
    "cardNumber": "1234123412341234",
    "cardExpirationDate": "12/2021",
    "cardVerificationValue": "123",
    "cardHolderName": "Patryk Szczerbinski",
    "amount": 500,
    "currency": "GBP"
}
```
__GET__ - *api/v1/paymentgateway/retrievepayment/{identifier}*

This API retrieves details of payment process requests made using the `processpayment` API. The *`identifier`* should be the payment identifier from the response of the `processpayment` API.

### Supported currencies
| Name | Code |
|:----:|:----:|
| Pound Sterling | GBP |
| Euro | EUR |

## Testing
### Unit tests
Unit test have been included in the *PaymentGateway.Tests* project and can be run from the solution directory using the following command or from within Visual Studio.
```
dotnet test PaymentGateway.Tests/PaymentGateway.Tests.csproj
```
### Integration tests
The integration tests have been implemented to simulate the user using the API. The tests are using a simple `PaymentGatewayApi` class that uses the `HttpClient` to call the REST API of the Payment Gateway.

Once the docker container is running, the integration tests can be run from the solution directory using the following command.
```
dotnet test PaymentGateway.IntegrationTests/PaymentGateway.IntegrationTests.csproj --settings PaymentGateway.IntegrationTests/IntegrationTestSettings.runsettings
```
The *IntegrationTestSettings.runsettings* file contains the address of where the gateway is located so it can be changed if the container is running in the cloud.

## MongoDB storage
For this solution, I have implemented MongoDB storage for the payments. The storage will persist for the lifetime of the MongoDB container.

### Mongo Express
Database administration tools are available via Mongo Express at *http://localhost:8081/*.
- Username: dev
- Password: devpassword

## Areas for improvement
- Support for AMEX cards, which have a 4 digit CVV
- Addition of a billing address in the payment request
- Validation in the bank stub layer
- Better type for transaction amount to avoid double precision issues
- Implement tests around the Mongo implementation
- Improve code security by hiding sensitive data in a vault