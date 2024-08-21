using Smartwyre.DeveloperTest.Calculators.Adapters.Factory.Interfaces;
using Smartwyre.DeveloperTest.Calculators.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators.Adapters.Factory
{
    /// <summary>
    ///   SimpleRebateCalculatorFactory
    /// </summary>
    public class SimpleRebateCalculatorFactory : ISimpleRebateCalculatorFactory
    {
        private readonly Dictionary<RebateCalculatorType, IRebateCalculator> _calculators;

        public SimpleRebateCalculatorFactory()
        {
            _calculators = new Dictionary<RebateCalculatorType, IRebateCalculator>
            {
                { RebateCalculatorType.FixedCashAmount, new FixedCashAmountCalculatorAdapter(new FixedCashAmountCalculator()) },
                { RebateCalculatorType.FixedRate, new FixedRateRebateCalculatorAdapter(new FixedRateRebateCalculator()) },
                { RebateCalculatorType.AmountPerUom, new AmountPerUomCalculatorAdapter(new AmountPerUomCalculator()) }
                // Add more calculators as needed
            };
        }


        public IRebateCalculator GetCalculator(Rebate rebate)
        {
            // Attempt to get the calculator based on the rebate type
            if (_calculators.TryGetValue(rebate.RebateCalculatorType, out var calculator))
            {
                return calculator;
            }

            // If no matching calculator is found, throw an exception
            throw new InvalidOperationException($"No calculator found for rebate type {rebate.RebateCalculatorType}.");
        }
    }
}

