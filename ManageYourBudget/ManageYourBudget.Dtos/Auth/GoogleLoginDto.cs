using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManageYourBudget.Dtos.Auth
{
    public class GoogleLoginDto
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string State { get; set; }
    }
}
