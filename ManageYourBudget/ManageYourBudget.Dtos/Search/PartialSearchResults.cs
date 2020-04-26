using System.Collections.Generic;

namespace ManageYourBudget.Dtos.Search
{
    public class PartialSearchResults<T>
    {
        public ICollection<T> Results { get; set; }
        public int Page { get; set; }
        public bool IsMore { get; set; }
    }
}
