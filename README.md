# Sample - Create an order with Coinbase Commerce payment gateway
The following project is just a boilerplater/sample on how to create an order and use Coinbase Commerce as a payment gateway.

## Environment varilables  appsettings.json
```
{ 
  "Coinbase": {
    "API_KEY": "",
    "WEBHOOK_SECRET": ""
  }
}

```

## Instructions
 
Add Coinbase Commerce from NuGet or via terminal
```
Install-Package Coinbase.Commerce
```

## Method
 
* Endpoint: https://69e0ede72c29.ngrok.io/api/order
* Method: GET (Just for testing)
* Body - Empty 

## Response <string>
```
https://commerce.coinbase.com/charges/ZEANAWBK
```