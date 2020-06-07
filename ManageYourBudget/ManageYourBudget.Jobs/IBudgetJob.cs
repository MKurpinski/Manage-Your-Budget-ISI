using System.Threading.Tasks;

namespace ManageYourBudget.Jobs
{
    public interface IBudgetJob
    {
        Task Execute();
    }
}
