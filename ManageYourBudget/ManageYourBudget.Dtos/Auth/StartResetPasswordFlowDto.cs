using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManageYourBudget.Dtos.Auth
{
    public class StartResetPasswordFlowDto
    {
        [Required]
        public string Email { get; set; }
    }
}
