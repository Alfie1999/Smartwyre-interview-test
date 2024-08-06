using Smartwyre.DeveloperTest.Calculators.Interfaces;
using Smartwyre.DeveloperTest.Types; 

namespace Smartwyre.DeveloperTest.Calculators.Adapters
{
    // Adapter class for IFixedRateRebateCalculator
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
    public class FixedRateRebateCalculatorAdapter(IFixedRateRebateCalculator calculator) 
        : IRebateCalculator
    {
        // Private field to hold an instance of IFixedRateRebateCalculator
        private readonly IFixedRateRebateCalculator _calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));

        // Implementation of IsApplicable from IRebateCalculator
        // Ensures all parameters are non-null before proceeding
        public bool IsApplicable(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            // Validate that none of the arguments are null
            ArgumentNullException.ThrowIfNull(rebate);
            ArgumentNullException.ThrowIfNull(product);
            ArgumentNullException.ThrowIfNull(request);

            // Delegate the call to the adapted IFixedRateRebateCalculator
            return _calculator.IsApplicable(rebate, product, request);
        }

        // Implementation of CalculateRebateAmount from IRebateCalculator
        // Ensures all parameters are non-null before proceeding
        public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            // Validate that none of the arguments are null
            ArgumentNullException.ThrowIfNull(rebate);
            ArgumentNullException.ThrowIfNull(product);
            ArgumentNullException.ThrowIfNull(request);

            // Delegate the call to the adapted IFixedRateRebateCalculator
            return _calculator.CalculateRebateAmount(rebate, product, request);
        }
    }
}
