using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ManageYourBudget.Common;
using ManageYourBudget.Dtos.Auth;

namespace ManageYourBudget.BussinessLogic.Interfaces
{
    public interface IPasswordResetService: IService
    {
        Task StartResetPasswordFlow(StartResetPasswordFlowDto startResetPasswordFlowDto);
        Task<Result> ResetPassword(ResetPasswordDto resetPasswordDto);
        Task<Result> ValidatePasswordResetHash(string hash);
    }
}
