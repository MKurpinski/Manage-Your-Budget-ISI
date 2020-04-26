using System.ComponentModel.DataAnnotations;

namespace ManageYourBudget.Dtos.Search
{
    public class BaseSearchOptionsDto
    {
        public string SearchTerm { get; set; }
        [Range(1, 9999)]
        public int Batch { get; set; }
        [Range(0, 9999)]
        public int Page { get; set; }
    }
}
