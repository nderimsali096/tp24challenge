# Receivables Management API
The Receivables Management API is a .NET Web API that allows you to manage and analyze receivables data. It accepts payloads containing receivables data and provides summary statistics about the stored data, including the value of open and closed invoices.

## I completed this challenge within a timeframe of 3 hours. That includes 15 minutes for planning, 1 hour and 45 minutes for implementation, and 1 hour for testing.

# Installation
To set up the Receivables Management API on your local machine, follow these steps:

1. Clone the repository from GitHub: git clone https://github.com/nderimsali096/tp24challenge.git
2. Navigate to the project directory: cd tp24
3. Install the required dependencies: dotnet restore
4. Start the API: dotnet run

# Usage
The Receivables Management API allows you to submit receivables data and retrieve summary statistics. Here's how to use it:

## Submitting Receivables Data
Send a POST request with a payload containing receivables data to the "/receivables/add" endpoint. Example payload:
```json
{
"reference": "string",
"currencyCode": "string",
"issueDate": "string",
"openingValue": 1234.56,
"paidValue": 1234.56,
"dueDate": "string",
"closedDate": "string", //optional
"cancelled": true|false, // optional
"debtorName": "string",
"debtorReference": "string",
"debtorAddress1": "string", //optional
"debtorAddress2": "string", //optional
"debtorTown": "string", //optional
"debtorState": "string", //optional
"debtorZip": "string", //optional
"debtorCountryCode": "string",
"debtorRegistrationNumber": "string" //optional
}
```

## Retrieving Summary Statistics
Send a GET request to the "/receivables/summary" endpoint to get summary statistics about the stored receivables data.

# Testing
This project includes unit tests to ensure its functionality. You can run the tests using the following command:
```bash
dotnet test
```
or you can run the tests from the IDE (prefered).

# Authors
Nderim Sali (@nderimsali096)



