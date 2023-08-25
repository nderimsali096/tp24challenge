using TP24.Models;

namespace TP24.Services
{
    public class ValidationService
    {
        public bool ValidateReceivablePayload(ReceivablePayload payload)
        {
            if (string.IsNullOrWhiteSpace(payload.Reference) ||
                string.IsNullOrWhiteSpace(payload.CurrencyCode) ||
                string.IsNullOrWhiteSpace(payload.IssueDate) ||
                payload.OpeningValue == 0 ||
                payload.PaidValue == 0 ||
                string.IsNullOrWhiteSpace(payload.DueDate))
            {
                return false;
            }
            return true;
        }
    }
}
