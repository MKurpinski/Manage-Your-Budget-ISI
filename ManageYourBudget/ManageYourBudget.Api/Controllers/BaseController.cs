using Microsoft.AspNetCore.Mvc;

namespace ManageYourBudget.Api.Controllers
{
    public class BaseController: Controller
    {
        protected string UserId => "02292f92-cae1-4b60-a2d8-f8023fa4214d"; //User.UserId();
    }
}
