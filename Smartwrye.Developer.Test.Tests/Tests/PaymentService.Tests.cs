using FluentAssertions;
using Moq;
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Calculators.Adapters;
using Smartwyre.DeveloperTest.Calculators.Interfaces;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwrye.Developer.Test.Tests.Tests
{
    public class PaymentServiceTestsFact
    {
        /// <summary>
        /// Tests if the calculator correctly applies and calculates the rebate amount 
        /// when conditions are met for FixedCashAmount.
        /// </summary>
        [Fact]
        public void Calculate_FixedCashAmount_RebateSuccessful()
        {
            // Arrange
            var rebateDataStore = new Mock<IRebateDataStore>();
            var productDataStore = new Mock<IProductDataStore>();
            var calculators = new List<IRebateCalculator> { new FixedCashAmountCalculatorAdapter( new FixedCashAmountCalculator()) };

            var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 100 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

            rebateDataStore.Setup(r => r.GetRebate(It.IsAny<string>())).Returns(rebate);
            productDataStore.Setup(p => p.GetProduct(It.IsAny<string>())).Returns(product);

            var service = new RebateService(rebateDataStore.Object, productDataStore.Object, calculators);

            var request = new CalculateRebateRequest { RebateIdentifier = "rebate1", ProductIdentifier = "product1" };

            // Act
            var result = service.Calculate(request);

            // Assert
            Assert.True(result.Success);
            rebateDataStore.Verify(r => r.StoreCalculationResult(rebate, 100), Times.Once);
        }

        /// <summary>
        /// Tests if the FixedCashAmountCalculator returns the correct rebate amount 
        /// when conditions are met.
        /// </summary>
        [Fact]
        public void FixedCashAmountCalculator_ShouldReturnCorrectAmount_WhenConditionsAreMet()
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 100
            };
            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount
            };
            var request = new CalculateRebateRequest();

            var calculator = new FixedCashAmountCalculator();

            // Act
            var isApplicable = calculator.IsApplicable(rebate, product);
            var rebateAmount = calculator.CalculateRebateAmount(rebate, product);

            // Assert
            isApplicable.Should().BeTrue();
            rebateAmount.Should().Be(100);
        }

        /// <summary>
        /// Tests if the FixedRateRebateCalculator returns the correct rebate amount 
        /// when conditions are met.
        /// </summary>
        [Fact]
        public void FixedRateRebateCalculator_ShouldReturnCorrectAmount_WhenConditionsAreMet()
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedRateRebate,
                Percentage = 0.1m
            };
            var product = new Product
            {
                Price = 200,
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate
            };
            var request = new CalculateRebateRequest
            {
                Volume = 10
            };

            var calculator = new FixedRateRebateCalculator();

            // Act
            var isApplicable = calculator.IsApplicable(rebate, product, request);
            var rebateAmount = calculator.CalculateRebateAmount(rebate, product, request);

            // Assert
            isApplicable.Should().BeTrue();
            rebateAmount.Should().Be(200 * 0.1m * 10);
        }

        /// <summary>
        /// Tests if the AmountPerUomCalculator returns the correct rebate amount 
        /// when conditions are met.
        /// </summary>
        [Fact]
        public void AmountPerUomCalculator_ShouldReturnCorrectAmount_WhenConditionsAreMet()
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.AmountPerUom,
                Amount = 5
            };
            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.AmountPerUom
            };
            var request = new CalculateRebateRequest
            {
                Volume = 10
            };

            var calculator = new AmountPerUomCalculator();

            // Act
            var isApplicable = calculator.IsApplicable(rebate, product, request);
            var rebateAmount = calculator.CalculateRebateAmount(rebate, request);

            // Assert
            isApplicable.Should().BeTrue();
            rebateAmount.Should().Be(5 * 10);
        }

        /// <summary>
        /// Tests if the FixedCashAmountCalculator correctly identifies when it should not be applicable.
        /// </summary>
        [Fact]
        public void FixedCashAmountCalculator_ShouldNotBeApplicable_WhenConditionsAreNotMet()
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 0
            };
            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount
            };
            var request = new CalculateRebateRequest();

            var calculator = new FixedCashAmountCalculator();

            // Act
            var isApplicable = calculator.IsApplicable(rebate, product);

            // Assert
            isApplicable.Should().BeFalse();
        }

        /// <summary>
        /// Tests if the FixedRateRebateCalculator correctly identifies when it should not be applicable.
        /// </summary>
        [Fact]
        public void FixedRateRebateCalculator_ShouldNotBeApplicable_WhenConditionsAreNotMet()
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedRateRebate,
                Percentage = 0
            };
            var product = new Product
            {
                Price = 0,
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate
            };
            var request = new CalculateRebateRequest
            {
                Volume = 0
            };

            var calculator = new FixedRateRebateCalculator();

            // Act
            var isApplicable = calculator.IsApplicable(rebate, product, request);

            // Assert
            isApplicable.Should().BeFalse();
        }

        /// <summary>
        /// Tests if the AmountPerUomCalculator correctly identifies when it should not be applicable.
        /// </summary>
        [Fact]
        public void AmountPerUomCalculator_ShouldNotBeApplicable_WhenConditionsAreNotMet()
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.AmountPerUom,
                Amount = 0
            };
            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.AmountPerUom
            };
            var request = new CalculateRebateRequest
            {
                Volume = 0
            };

            var calculator = new AmountPerUomCalculator();

            // Act
            var isApplicable = calculator.IsApplicable(rebate, product, request);

            // Assert
            isApplicable.Should().BeFalse();
        }
    }
}
