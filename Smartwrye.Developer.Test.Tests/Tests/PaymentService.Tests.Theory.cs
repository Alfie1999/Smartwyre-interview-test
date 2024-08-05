using FluentAssertions;
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwrye.Developer.Test.Tests.Tests
{
    public class PaymentServiceTestsTheory
    {
        /// <summary>
        /// Tests for FixedCashAmountCalculator when conditions are met.
        /// </summary>
        /// <param name="rebateAmount">Rebate amount.</param>
        /// <param name="expectedAmount">Expected rebate amount.</param>
        [Theory]
        [InlineData(100, 100)]
        [InlineData(200, 200)]
        public void FixedCashAmountCalculator_ShouldReturnCorrectAmount_WhenConditionsAreMet(decimal rebateAmount, decimal expectedAmount)
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedCashAmount,
                Amount = rebateAmount
            };
            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount
            };
            var request = new CalculateRebateRequest();

            var calculator = new FixedCashAmountCalculator();

            // Act
            var isApplicable = calculator.IsApplicable(rebate, product, request);
            var calculatedAmount = calculator.CalculateRebateAmount(rebate, product, request);

            // Assert
            isApplicable.Should().BeTrue();
            calculatedAmount.Should().Be(expectedAmount);
        }

        /// <summary>
        /// Tests for FixedRateRebateCalculator when conditions are met.
        /// </summary>
        /// <param name="price">Product price.</param>
        /// <param name="percentage">Rebate percentage.</param>
        /// <param name="volume">Product volume.</param>
        /// <param name="expectedAmount">Expected rebate amount.</param>
        [Theory]
        [InlineData(200, 0.1, 10, 200)]
        [InlineData(300, 0.2, 5, 300)]
        public void FixedRateRebateCalculator_ShouldReturnCorrectAmount_WhenConditionsAreMet(decimal price, decimal percentage, decimal volume, decimal expectedAmount)
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedRateRebate,
                Percentage = percentage
            };
            var product = new Product
            {
                Price = price,
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate
            };
            var request = new CalculateRebateRequest
            {
                Volume = volume
            };

            var calculator = new FixedRateRebateCalculator();

            // Act
            var isApplicable = calculator.IsApplicable(rebate, product, request);
            var calculatedAmount = calculator.CalculateRebateAmount(rebate, product, request);

            // Assert
            isApplicable.Should().BeTrue();
            calculatedAmount.Should().Be(expectedAmount * percentage * volume);
        }

        /// <summary>
        /// Tests for AmountPerUomCalculator when conditions are met.
        /// </summary>
        /// <param name="amount">Rebate amount per unit of measure.</param>
        /// <param name="volume">Product volume.</param>
        /// <param name="expectedAmount">Expected rebate amount.</param>
        [Theory]
        [InlineData(5, 10, 50)]
        [InlineData(7, 15, 105)]
        public void AmountPerUomCalculator_ShouldReturnCorrectAmount_WhenConditionsAreMet(decimal amount, decimal volume, decimal expectedAmount)
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.AmountPerUom,
                Amount = amount
            };
            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.AmountPerUom
            };
            var request = new CalculateRebateRequest
            {
                Volume = volume
            };

            var calculator = new AmountPerUomCalculator();

            // Act
            var isApplicable = calculator.IsApplicable(rebate, product, request);
            var calculatedAmount = calculator.CalculateRebateAmount(rebate, product, request);

            // Assert
            isApplicable.Should().BeTrue();
            calculatedAmount.Should().Be(expectedAmount);
        }

        /// <summary>
        /// Tests for FixedCashAmountCalculator when the rebate amount is zero or negative.
        /// </summary>
        /// <param name="rebateAmount">Rebate amount.</param>
        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void FixedCashAmountCalculator_ShouldNotBeApplicable_WhenAmountIsInvalid(decimal rebateAmount)
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedCashAmount,
                Amount = rebateAmount
            };
            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount
            };
            var request = new CalculateRebateRequest();

            var calculator = new FixedCashAmountCalculator();

            // Act
            var isApplicable = calculator.IsApplicable(rebate, product, request);

            // Assert
            isApplicable.Should().BeFalse();
        }

        /// <summary>
        /// Tests for FixedRateRebateCalculator when the percentage is zero or negative.
        /// </summary>
        /// <param name="percentage">Rebate percentage.</param>
        [Theory]
        [InlineData(0.0)]
        [InlineData(-0.1)]
        public void FixedRateRebateCalculator_ShouldNotBeApplicable_WhenPercentageIsInvalid(decimal percentage)
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedRateRebate,
                Percentage = percentage
            };
            var product = new Product
            {
                Price = 100,
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate
            };
            var request = new CalculateRebateRequest
            {
                Volume = 10
            };

            var calculator = new FixedRateRebateCalculator();

            // Act
            var isApplicable = calculator.IsApplicable(rebate, product, request);

            // Assert
            isApplicable.Should().BeFalse();
        }

        /// <summary>
        /// Tests for AmountPerUomCalculator when the amount is zero or negative.
        /// </summary>
        /// <param name="amount">Rebate amount per unit of measure.</param>
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void AmountPerUomCalculator_ShouldNotBeApplicable_WhenAmountIsInvalid(decimal amount)
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.AmountPerUom,
                Amount = amount
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

            // Assert
            isApplicable.Should().BeFalse();
        }
    }
}
