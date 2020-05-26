using System.Collections.Generic;

namespace ManageYourBudget.Dtos.Search
{
    public class BaseSearchResults<T>
    {
        public ICollection<T> Results { get; set; }
    }
}
