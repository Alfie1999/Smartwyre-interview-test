using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up the data stores
            IRebateDataStore rebateDataStore = new RebateDataStore();
            IProductDataStore productDataStore = new ProductDataStore();

            // Set up the calculators
            var calculators = new List<IRebateCalculator>
            {
                new FixedCashAmountCalculator(),
                new FixedRateRebateCalculator(),
                new AmountPerUomCalculator()
            };

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
