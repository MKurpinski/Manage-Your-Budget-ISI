using System;
using ManageYourBudget.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace ManageYourBudget.DataAccess.Models
{
    public class User: IdentityUser, IEntity
    {
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureSrc { get; set; }
        public string ResetPasswordHash { get; set; }
        public DateTime? ResetPasswordHashExpirationTime { get; set; }
        public LoginProvider RegisteredWith { get; set; }

        public bool HasLocalAccount() => PasswordHash != null;
    }
}
