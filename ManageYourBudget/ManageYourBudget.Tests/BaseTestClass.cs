using AutoFixture;
using AutoMapper;
using ManageYourBudget.Configuration;

namespace ManageYourBudget.Tests
{
    public abstract class BaseTestClass
    {
        protected IMapper Mapper;
        protected  Fixture Fixture;

        protected BaseTestClass()
        {
            Fixture = new Fixture();
            Mapper = MappingConfiguration.CreateConfig().CreateMapper();
        }
    }
}
