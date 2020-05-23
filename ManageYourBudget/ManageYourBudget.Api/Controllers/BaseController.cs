using ManageYourBudget.Common.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ManageYourBudget.Api.Controllers
{
    public class BaseController: Controller
    {
        protected string UserId => User.UserId();
    }
}
