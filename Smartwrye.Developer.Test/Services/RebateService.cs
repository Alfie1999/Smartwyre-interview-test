using Smartwyre.DeveloperTest.Calculators.Adapters.Factory.Interfaces;
using Smartwyre.DeveloperTest.Data.Interfaces;
using Smartwyre.DeveloperTest.Smartwrye.Developer.Test.Types;
using Smartwyre.DeveloperTest.Types;
/// <summary>
/// used with stratergy
/// </summary>
public class RebateService
{
    /// <summary>
    /// null interfaces to test both processes - this is temp for testing
    /// </summary>
    private readonly ISimpleRebateCalculatorFactory? _simpleRebateCalculatorFactory;
    private readonly IStrategyRebateCalculatorFactory? _strategyRebateCalculatorFactory;
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IProductDataStore _productDataStore;
  

    public RebateService(
        IRebateDataStore rebateDataStore,
        IProductDataStore productDataStore,
        ISimpleRebateCalculatorFactory simpleRebateCalculatorFactory)
    {
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
        _simpleRebateCalculatorFactory = simpleRebateCalculatorFactory;
    }
    public RebateService(
       IRebateDataStore rebateDataStore,
       IProductDataStore productDataStore,
       IStrategyRebateCalculatorFactory strateryRebateCalculatorFactory)
    {
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
        _strategyRebateCalculatorFactory = strateryRebateCalculatorFactory;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        var product = _productDataStore.GetProduct(request.ProductIdentifier);

        // Get the appropriate calculator using the factory

        // Use strategy-based method
        //var calculator = _rebateCalculatorFactory.GetCalculator(rebate, product) 

        // Use simple factory method
        if( _simpleRebateCalculatorFactory != null)
        {
            var calculator = _simpleRebateCalculatorFactory.GetCalculator(rebate);
            var rebateAmount = calculator.CalculateRebateAmount(rebate, product, request);

            _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);

            return new CalculateRebateResult { Success = true };
        }
        else if (_strategyRebateCalculatorFactory != null)
        {
            var calculator = _strategyRebateCalculatorFactory.GetCalculator(rebate, product);
            var rebateAmount = calculator.CalculateRebateAmount(rebate, product, request);

            _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);

            return new CalculateRebateResult { Success = true };
        }

        return new CalculateRebateResult { Success = false };
    }
}

