using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace ManageYourBudget.BussinessLogic.Services
{
    public abstract class BaseService
    {
        protected readonly IMapper Mapper;
        protected BaseService(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
