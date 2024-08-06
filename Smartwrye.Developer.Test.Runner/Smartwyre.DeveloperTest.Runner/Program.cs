
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Calculators.Adapters;
using Smartwyre.DeveloperTest.Calculators.Interfaces;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up the data stores
            IRebateDataStore rebateDataStore = new RebateDataStore();
            IProductDataStore productDataStore = new ProductDataStore();

            // Set up the calculators with adapters
            List<IRebateCalculator> calculators =
            [
                new FixedCashAmountCalculatorAdapter(new FixedCashAmountCalculator()),
                new FixedRateRebateCalculatorAdapter(new FixedRateRebateCalculator()),
                new AmountPerUomCalculatorAdapter(new AmountPerUomCalculator())
            ];

            // Create the rebate service
            var rebateService = new RebateService(rebateDataStore, productDataStore, calculators);

            // Create a sample request
            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "rebate1",
                ProductIdentifier = "product1",
                Volume = 10
            };

            // Calculate the rebate
            var result = rebateService.Calculate(request);

            // Display the result
            if (result.Success)
            {
                Console.WriteLine("Rebate calculation successful!");
            }
            else
            {
                Console.WriteLine("Rebate calculation failed.");
            }
        }
    }
}
