using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP24.Models;

namespace TP24.UnitTests.Utilities
{
    public static class TestData
    {
        public static ReceivablePayload GenerateValidReceivablePayload()
        {
            return new ReceivablePayload
            {
                Reference = "TestReference",
                CurrencyCode = "USD",
                IssueDate = "2023-08-01",
                OpeningValue = 1000.00m,
                PaidValue = 500.00m,
                DueDate = "2023-09-01",
                DebtorName = "Test Debtor",
                DebtorReference = "DebtorRef123",
                DebtorCountryCode = "US"
            };
        }

        public static ReceivablePayload GenerateInvalidReceivablePayload()
        {
            return new ReceivablePayload
            {
                IssueDate = "2023-08-01",
                OpeningValue = 1000.00m,
                PaidValue = 500.00m,
                DueDate = "2023-09-01",
                DebtorName = "Test Debtor",
                DebtorReference = "DebtorRef123",
                DebtorCountryCode = "US"
            };
        }

        public static List<ReceivablePayload> GenerateListOfReceivablePayloads()
        {
            return new List<ReceivablePayload>
            {
                new ReceivablePayload
                {
                    Reference = "INV001",
                    CurrencyCode = "USD",
                    IssueDate = "2023-08-01",
                    OpeningValue = 200,
                    PaidValue = 150,
                    DueDate = "2023-08-15"
                },
                new ReceivablePayload
                {
                    Reference = "INV002",
                    CurrencyCode = "EUR",
                    IssueDate = "2023-08-02",
                    OpeningValue = 300,
                    PaidValue = 100,
                    DueDate = "2023-08-20"
                },
                new ReceivablePayload
                {
                    Reference = "INV003",
                    CurrencyCode = "USD",
                    IssueDate = "2023-08-01",
                    OpeningValue = 200,
                    PaidValue = 150,
                    DueDate = "2023-08-15"
                },
                new ReceivablePayload
                {
                    Reference = "INV004",
                    CurrencyCode = "EUR",
                    IssueDate = "2023-08-02",
                    OpeningValue = 300,
                    PaidValue = 100,
                    DueDate = "2023-08-20"
                }
            };
        }
    }
}
