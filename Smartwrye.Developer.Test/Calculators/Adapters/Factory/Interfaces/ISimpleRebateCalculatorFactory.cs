using Smartwyre.DeveloperTest.Calculators.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators.Adapters.Factory.Interfaces
{
    public interface ISimpleRebateCalculatorFactory
    {
        IRebateCalculator GetCalculator(Rebate rebate);
    }
}
