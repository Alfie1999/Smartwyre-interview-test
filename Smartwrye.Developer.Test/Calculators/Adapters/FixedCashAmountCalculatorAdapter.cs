using Smartwyre.DeveloperTest.Calculators.Interfaces; 
using Smartwyre.DeveloperTest.Types; 

namespace Smartwyre.DeveloperTest.Calculators.Adapters
{
    // Adapter class for IFixedCashAmountCalculator
    /// <summary>
    /// Passing unused parameters through an adapter is acceptable if done intentionally 
    /// and with clear documentation. Ensure that the design maintains consistency and adheres 
    /// to the expected contract of the interface.
    /// Proper validation and error handling should also be in place to ensure robustness
    /// This approach provides a way to unify different calculator interfaces under a common interface 
    /// while managing parameter discrepancies effectively.
    /// </summary>
    /// <param name="calculator"></param>
    // Adapter class for IAmountPerUomCalculator
    public class FixedCashAmountCalculatorAdapter(IFixedCashAmountCalculator calculator) 
        : IRebateCalculator
    {
        // Private field to hold an instance of IFixedCashAmountCalculator
        private readonly IFixedCashAmountCalculator _calculator = calculator ?? 
            throw new ArgumentNullException(nameof(calculator));

        // Implementation of IsApplicable from IRebateCalculator
        // Handle the fact that the underlying calculator does not use the request parameter
        public bool IsApplicable(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            // Validate that rebate and product are not null
            ArgumentNullException.ThrowIfNull(rebate);
            ArgumentNullException.ThrowIfNull(product);
            // `request` is not used by the underlying calculator, so it is not validated here
            return _calculator.IsApplicable(rebate, product);
        }

        // Implementation of CalculateRebateAmount from IRebateCalculator
        // Handle the fact that the underlying calculator does not use the request parameter
        public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            // Validate that rebate and product are not null
            ArgumentNullException.ThrowIfNull(rebate);
            ArgumentNullException.ThrowIfNull(product);
            // `request` is not used by the underlying calculator, so it is not validated here
            return _calculator.CalculateRebateAmount(rebate, product);
        }
    }
}
