namespace ManageYourBudget.DataAccess.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly BudgetContext Context;

        protected BaseRepository(BudgetContext context)
        {
            Context = context;
        }
    }
}
