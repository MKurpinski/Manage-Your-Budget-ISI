using System;
using System.Collections.Generic;
using System.Text;

namespace ManageYourBudget.Dtos.Auth
{
    public class ExternalRegisterUserDto: RegisterUserDto
    {
        public string PictureSrc { get; set; }
        public string AccessToken { get; set; }
    }
}
