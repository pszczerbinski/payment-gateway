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
Payment Gateway controller has been implemented to validate the payment requests and fail fast if any fields fail validation.

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
### Supported currencies
| Name | Code |
|:----:|:----:|
| Pound Sterling | GBP |
| Euro | EUR |

## Areas for improvement
- Support for AMEX cards, which have a 4 digit CVV
- Addition of a billing address in the payment request
- Validation in the bank stub layer
- Better type for transaction amount to avoid double precision issues