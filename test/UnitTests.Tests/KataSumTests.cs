using FluentAssertions;
using UnitTesting;

namespace UnitTests.Tests
{
    public class KataSumTests
    {
        [Test]
        [TestCase("1", "2", 3)]
        [TestCase("4", "2", 6)]
        [TestCase("10", "11", 21)]
        [TestCase("9999", "1", 10000)]
        public void Sum_WhenNumbersIsNatural_ShouldReturnResult(string num1, string num2, int expectedResult)
        {
            // Arrange
            var sum = new KataSum();

            // Act 
            var result = sum.Sum(num1, num2);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Test]
        [TestCase("452345.632", "2", 2)]
        [TestCase("4", "-88888888", 4)]
        [TestCase("-54325325", "435.43254", 0)]
        [TestCase("-1", "-1", 0)]
        [TestCase("", "2", 2)]
        [TestCase("4", "", 4)]
        [TestCase(null, "7", 7)]
        [TestCase("6", null, 6)]
        [TestCase(null, "", 0)]
        [TestCase("", null, 0)]
        [TestCase(null, null, 0)]
        public void Sum_WhenNumbersIsNotNatural_ShouldReturnResult(string num1, string num2, int expectedResult)
        {
            // Arrange
            var sum = new KataSum();

            // Act 
            var result = sum.Sum(num1, num2);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}