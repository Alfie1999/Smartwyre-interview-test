using FluentAssertions;
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Types;

namespace Smartwrye.Developer.Test.Tests.Tests
{
    public class PaymentServiceTestsTheory
    {
        /// <summary>
        /// Tests for FixedCashAmountCalculator when conditions are met.
        /// </summary>
        /// <param name="rebateAmount">Rebate amount.</param>
        /// <param name="expectedAmount">Expected rebate amount.</param>

        public static IEnumerable<object[]> GetDiscountTestData1()
        {
            yield return new object[] { 100.00m, 100.00m };
            yield return new object[] { 200.00m, 200.00m };
        }
        [Theory]
        //[MemberData(nameof(GetDiscountTestData1))]
        [InlineData(100.00, 100.0)]
        [InlineData(200.00, 200.0)]
        public void Theory_FixedCashAmountCalculator_ShouldReturnCorrectAmount_WhenConditionsAreMet
            (decimal rebateAmount, decimal expectedAmount)
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
            var isApplicable = calculator.IsApplicable(rebate, product);
            var calculatedAmount = calculator.CalculateRebateAmount(rebate, product);

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
        public static IEnumerable<object[]> GetDiscountTestData2()
        {
            yield return new object[] { 200.00m, 0.10m, 10.00m, 200.00m };
            yield return new object[] { 300.00m, 0.20m, 5.00m, 300.00m };
        }
        [Theory]
        //[MemberData(nameof(GetDiscountTestData2))]
        [InlineData(200.00, 0.10, 10.00, 200.00)]
        [InlineData(300.00, 0.20, 5.00, 300.00)]
        public void FixedRateRebateCalculator_ShouldReturnCorrectAmount_WhenConditionsAreMet
            (decimal price, decimal percentage, decimal volume, decimal expectedAmount)
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

        public static IEnumerable<object[]> GetDiscountTestData3()
        {
            yield return new object[] { 5.00m, 10.00m, 50.00m };
            yield return new object[] { 7.00m, 15.00m, 105.00m };
        }
        [Theory]
        //[MemberData(nameof(GetDiscountTestData3))]
        [InlineData(5.00, 10.00, 50.00)]
        [InlineData(7.00, 15.00, 105.00)]
        public void AmountPerUomCalculator_ShouldReturnCorrectAmount_WhenConditionsAreMet
            (decimal amount, decimal volume, decimal expectedAmount)
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
            var calculatedAmount = calculator.CalculateRebateAmount(rebate, request);

            // Assert
            isApplicable.Should().BeTrue();
            calculatedAmount.Should().Be(expectedAmount);
        }

        /// <summary>
        /// Tests for FixedCashAmountCalculator when the rebate amount is zero or negative.
        /// </summary>
        /// <param name="rebateAmount">Rebate amount.</param>

        public static IEnumerable<object[]> GetDiscountTestData4()
        {
            yield return new object[] { 0.00m };
            yield return new object[] { -100.00m };
        }
        [Theory]
        //[MemberData(nameof(GetDiscountTestData4))]
        [InlineData(0.00)]
        [InlineData(-100.00)]
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
            var isApplicable = calculator.IsApplicable(rebate, product);

            // Assert
            isApplicable.Should().BeFalse();
        }

        /// <summary>
        /// Tests for FixedRateRebateCalculator when the percentage is zero or negative.
        /// </summary>
        /// <param name="percentage">Rebate percentage.</param>

        public static IEnumerable<object[]> GetDiscountTestData5()
        {
            yield return new object[] { 0.00m };
            yield return new object[] { -0.10m };
        }
        [Theory]
        //[MemberData(nameof(GetDiscountTestData5))]
        [InlineData(0.00)]
        [InlineData(-0.10)]
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

        public static IEnumerable<object[]> GetDiscountTestData6()
        {
            yield return new object[] { 0.00m };
            yield return new object[] { -1.00m };
        }
        [Theory]
        // [MemberData(nameof(GetDiscountTestData6))]
        [InlineData(0.00)]
        [InlineData(-1.00)]
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
