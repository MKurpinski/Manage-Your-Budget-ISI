using System;
using System.Collections.Generic;
using System.Text;

namespace ManageYourBudget.DataAccess.Models
{
    public interface IEntity
    {
        DateTime Created { get; set; }
        DateTime Modified { get; set; }
    }
}
