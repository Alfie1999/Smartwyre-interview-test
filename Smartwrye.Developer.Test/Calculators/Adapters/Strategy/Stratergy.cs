using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators.Adapters.Strategy
{
    public interface IRebateTypeStrategy
    {
        RebateCalculatorType DetermineCalculatorType(Rebate rebate, Product product);

    }

    public class FixedCashAmountStrategy : IRebateTypeStrategy
    {
        public RebateCalculatorType DetermineCalculatorType(Rebate rebate, Product product)
        {
            return RebateCalculatorType.FixedCashAmount;
        }
    }

    public class FixedRateStrategy : IRebateTypeStrategy
    {
        public RebateCalculatorType DetermineCalculatorType(Rebate rebate, Product product)
        {
            return RebateCalculatorType.FixedRate;
        }
    }

    public class AmountPerUomStrategy : IRebateTypeStrategy
    {
        public RebateCalculatorType DetermineCalculatorType(Rebate rebate, Product product)
        {
            return RebateCalculatorType.AmountPerUom;
        }
    }

}
