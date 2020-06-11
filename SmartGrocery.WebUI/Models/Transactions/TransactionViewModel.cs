using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartGrocery.WebUI.Models.Transactions
{
    public class TransactionViewModel
    {
        public Guid TransactionId { get; set; }

        [Required]
        [DisplayName("Transaction Number")]
        public string TransactionNumber { get; set; }

        [Required]
        [DisplayName("Total Money")]
        public string Amount { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        [DisplayName("Created At")]
        public DateTime? CreatedAt { get; set; }

        [DisplayName("Last Updated By")]
        public string LastUpdatedBy { get; set; }

        [DisplayName("Last Updated At")]
        public DateTime? LastUpdatedAt { get; set; }
    }
}