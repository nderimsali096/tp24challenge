using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TP24.Models;
using TP24.Repositories;

namespace TP24.Services
{
    public interface IReceivableService
    {
        Task AddReceivable(ReceivablePayload payload);
        Task<ReceivableSummary> GetReceivableSummary();
    }

    public class ReceivableService : IReceivableService
    {
        private readonly IReceivableRepository _receivableRepository;
        private readonly ValidationService _validationService;

        public ReceivableService(IReceivableRepository receivableRepository, ValidationService validationService)
        {
            _receivableRepository = receivableRepository;
            _validationService = validationService;
        }

        public async Task AddReceivable(ReceivablePayload payload)
        {
            if (!_validationService.ValidateReceivablePayload(payload))
            {
                throw new ArgumentException("Required properties are missing or invalid.");
            }

            var newReceivable = CreateNewReceivable(payload);

            await _receivableRepository.Add(newReceivable);
        }

        public async Task<ReceivableSummary> GetReceivableSummary()
        {
            var allReceivables = await _receivableRepository.GetAllReceivables();

            var openInvoicesValue = allReceivables
                .Where(r => !r.Cancelled && (r.ClosedDate == null || !IsDateClosed(r.ClosedDate)))
                .Sum(r => r.OpeningValue - r.PaidValue);

            var closedInvoicesValue = allReceivables
                .Where(r => r.ClosedDate != null && IsDateClosed(r.ClosedDate))
                .Sum(r => r.OpeningValue - r.PaidValue);

            return new ReceivableSummary
            {
                OpenInvoicesValue = openInvoicesValue,
                ClosedInvoicesValue = closedInvoicesValue
            };
        }

        private ReceivablePayload CreateNewReceivable(ReceivablePayload payload)
        {
            return new ReceivablePayload
            {
                Reference = payload.Reference,
                OpeningValue = payload.OpeningValue,
                PaidValue = payload.PaidValue,
                Cancelled = payload.Cancelled,
                CurrencyCode = payload.CurrencyCode,
                IssueDate = payload.IssueDate,
                DueDate = payload.DueDate,
                ClosedDate = payload.ClosedDate,
                DebtorName = payload.DebtorName,
                DebtorReference = payload.DebtorReference,
                DebtorAddress1 = payload.DebtorAddress1,
                DebtorAddress2 = payload.DebtorAddress2,
                DebtorTown = payload.DebtorTown,
                DebtorState = payload.DebtorState,
                DebtorZip = payload.DebtorZip,
                DebtorCountryCode = payload.DebtorCountryCode,
                DebtorRegistrationNumber = payload.DebtorRegistrationNumber
            };
        }

        private static bool IsDateClosed(string closedDate)
        {
            if (string.IsNullOrWhiteSpace(closedDate))
            {
                return false;
            }

            var currentDate = DateTime.Today;
            var parsedClosedDate = DateTime.ParseExact(closedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            return parsedClosedDate < currentDate;
        }
    }
}
