using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ManageYourBudget.Common.Enums;

namespace ManageYourBudget.Dtos.Search
{
    public class ExpenseSearchOptionsDto: BaseSearchOptionsDto
    {
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; } 
        [Required]
        public int Page { get; set; }
        [Required]
        public int BatchSize { get; set; }
        [Required]
        public ExpenseCategory Category { get; set; }
        [Required]
        public BalanceType Type { get; set; }
        [Required]
        public string WalletId { get; set; }
    }
}
