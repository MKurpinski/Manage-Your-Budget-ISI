using System;

namespace ManageYourBudget.Dtos.Expense
{
    public class BaseExpenseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public DateTime Created { get; set; }
    }
}
