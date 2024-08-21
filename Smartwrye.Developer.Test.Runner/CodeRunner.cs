using Smartwyre.DeveloperTest.Calculators.Adapters.Factory;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Smartwrye.Developer.Test.Types;
using Smartwyre.DeveloperTest.Types;
using System.Drawing;


namespace Smartwrye.Developer.Test.Runner
{
    public static class CodeRunner
    {
        public static void Run(bool useSimpleFactory = false)
        {
            // Set up the data stores
            IRebateDataStore rebateDataStore = new RebateDataStore();
            IProductDataStore productDataStore = new ProductDataStore();

            // Create the appropriate factory based on the flag
            var rebateCalculatorFactory = CreateRebateCalculatorFactory(useSimpleFactory);

            // Initialize the RebateService with the selected factory
            var rebateService = new RebateService(rebateDataStore, productDataStore, rebateCalculatorFactory);

            // Prepare the rebate request
            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "rebate1",
                ProductIdentifier = "product1",
                Volume = 10
            };

            // Calculate the rebate and display the result
            var result = rebateService.Calculate(request);
            DisplayResult(result, useSimpleFactory);
        }

        // Method to create the appropriate factory based on the flag
        private static dynamic CreateRebateCalculatorFactory(bool useSimpleFactory)
        {
            return useSimpleFactory
                ? new SimpleRebateCalculatorFactory()
                : new StrategyRebateCalculatorFactory(new RebateCalculatorTypeFactory());
        }

        // Method to display the result of the rebate calculation
        private static void DisplayResult(CalculateRebateResult result, bool useSimpleFactory)
        {
            if (result.Success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                if (useSimpleFactory)
                {
                    Console.WriteLine("SimpleRebateCalculatorFactory : Rebate calculation successful!");
                }
                else
                {
                    Console.WriteLine("StrategyRebateCalculatorFactory : Rebate calculation successful!");
                }
                Console.ResetColor();

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Rebate calculation failed.");
                Console.ResetColor();
            }
        }
    }
}


