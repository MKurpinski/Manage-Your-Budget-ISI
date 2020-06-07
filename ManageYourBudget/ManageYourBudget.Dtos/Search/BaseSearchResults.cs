using System.Collections.Generic;

namespace ManageYourBudget.Dtos.Search
{
    public class BaseSearchResults<T>
    {
        public BaseSearchResults()
        {
            Results = new List<T>();
        }
        public ICollection<T> Results { get; set; }
    }
}
