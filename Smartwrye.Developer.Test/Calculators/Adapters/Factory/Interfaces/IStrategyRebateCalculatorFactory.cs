using Smartwyre.DeveloperTest.Calculators.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators.Adapters.Factory.Interfaces
{
    public interface IStrategyRebateCalculatorFactory
    {
        IRebateCalculator GetCalculator(Rebate rebate, Product product);
    }
}
