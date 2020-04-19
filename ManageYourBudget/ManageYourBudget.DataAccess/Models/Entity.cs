using System;

namespace ManageYourBudget.DataAccess.Models
{
    public abstract class Entity: IEntity
    {
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
