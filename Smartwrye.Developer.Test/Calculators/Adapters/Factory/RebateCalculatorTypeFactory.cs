using Smartwyre.DeveloperTest.Calculators.Adapters.Factory.Interfaces;
using Smartwyre.DeveloperTest.Calculators.Adapters.Strategy;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators.Adapters.Factory
{
    public class RebateCalculatorTypeFactory : IRebateCalculatorTypeFactory
    {
        private readonly Dictionary<RebateCalculatorType, IRebateTypeStrategy> _strategies;

        public RebateCalculatorTypeFactory()
        {
            _strategies = new Dictionary<RebateCalculatorType, IRebateTypeStrategy>
            {
                { RebateCalculatorType.FixedCashAmount, new FixedCashAmountStrategy() },
                { RebateCalculatorType.FixedRate, new FixedRateStrategy() },
                { RebateCalculatorType.AmountPerUom, new AmountPerUomStrategy() }
            };
        }

        public RebateCalculatorType DetermineCalculatorType(Rebate rebate, Product product)
        {
            if (_strategies.TryGetValue(rebate.RebateCalculatorType, out var strategy))
            {
                return strategy.DetermineCalculatorType(rebate, product);
            }

            throw new InvalidOperationException("No valid calculator type found.");
        }

    }

}
