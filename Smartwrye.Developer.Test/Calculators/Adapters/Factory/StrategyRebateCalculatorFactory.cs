using Smartwyre.DeveloperTest.Calculators.Adapters.Factory.Interfaces;
using Smartwyre.DeveloperTest.Calculators.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators.Adapters.Factory
{
    /// <summary>
    /// Stratergy RebateCalculatorFactory
    /// </summary>
    public class StrategyRebateCalculatorFactory : IStrategyRebateCalculatorFactory
    {
        private readonly Dictionary<RebateCalculatorType, IRebateCalculator> _calculators;
        private readonly IRebateCalculatorTypeFactory _typeFactory;

        public StrategyRebateCalculatorFactory(IRebateCalculatorTypeFactory typeFactory)
        {
            _typeFactory = typeFactory ?? throw new ArgumentNullException(nameof(typeFactory));

            _calculators = new Dictionary<RebateCalculatorType, IRebateCalculator>
            {
                 { RebateCalculatorType.FixedCashAmount, new FixedCashAmountCalculatorAdapter(new FixedCashAmountCalculator()) },
                { RebateCalculatorType.FixedRate, new FixedRateRebateCalculatorAdapter(new FixedRateRebateCalculator()) },
                { RebateCalculatorType.AmountPerUom, new AmountPerUomCalculatorAdapter(new AmountPerUomCalculator()) }
                // Add more calculators as needed
            };
        }

        public IRebateCalculator GetCalculator(Rebate rebate, Product product)
        {
            // Determine the calculator type using the strategy factory
            var calculatorType = _typeFactory.DetermineCalculatorType(rebate, product);

            // Attempt to get the calculator based on the determined type
            if (_calculators.TryGetValue(calculatorType, out var calculator))
            {
                return calculator;
            }

            // If no matching calculator is found, throw an exception
            throw new InvalidOperationException($"No calculator found for rebate type {calculatorType}.");
        }
    }
}

