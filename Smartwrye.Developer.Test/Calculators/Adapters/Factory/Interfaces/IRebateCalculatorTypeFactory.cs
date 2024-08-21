using Smartwyre.DeveloperTest.Calculators.Adapters.Strategy;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators.Adapters.Factory.Interfaces
{
    public interface IRebateCalculatorTypeFactory
    {
        RebateCalculatorType DetermineCalculatorType(Rebate rebate, Product product);
    }
}