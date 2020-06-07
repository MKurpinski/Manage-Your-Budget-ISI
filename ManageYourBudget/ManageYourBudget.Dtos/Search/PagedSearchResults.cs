namespace ManageYourBudget.Dtos.Search
{
    public class PagedSearchResults<T>: BaseSearchResults<T>
    {
        public int Total { get; set; }
    }
}
