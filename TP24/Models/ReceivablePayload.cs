using System.ComponentModel.DataAnnotations;

namespace TP24.Models
{
    public class ReceivablePayload
    {
        public int Id { get; set; }
        [Required]
        public string Reference { get; set; }

        [Required]
        public string CurrencyCode { get; set; }

        [Required]
        public string IssueDate { get; set; }

        [Required]
        public decimal OpeningValue { get; set; }

        [Required]
        public decimal PaidValue { get; set; }

        [Required]
        public string DueDate { get; set; }

        public string ClosedDate { get; set; }

        public bool Cancelled { get; set; }

        public string DebtorName { get; set; }

        public string DebtorReference { get; set; }

        public string DebtorAddress1 { get; set; }

        public string DebtorAddress2 { get; set; }

        public string DebtorTown { get; set; }

        public string DebtorState { get; set; }

        public string DebtorZip { get; set; }

        public string DebtorCountryCode { get; set; }

        public string DebtorRegistrationNumber { get; set; }
    }

}
