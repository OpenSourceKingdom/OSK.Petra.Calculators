using OSK.Petra.Calculators.Inflation.Polynomial;

namespace BlankStudios.Games.TowerDefenseLabs.UnitTests.Libraries.OSK.Game.Mechanics.Calculators.Inflation.Internal.Services;

public class PolynomialInflationInflationTests
{
    #region Inflate

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Inflate_NullOrEmptyCoeffecients_ReturnsBaseValue(bool useNull)
    {
        // Arrange
        var calculator = new PolynomialInflationCalculator(useNull ? null! : []);

        // Act/Assert
        for (var i = 0; i < 10; i++)
        {
            var result = calculator.Inflate(100, i);
            Assert.Equal(100, result);
        }
    }

    [Fact]
    public void Inflate_SeveralCoeffecients_ReturnsBaseValueAddedToPolynomial_SubstitueQuantityWithCoeffecients()
    {
        // Arrange
        PolynomialCoeffecient[] coeffecients = [new PolynomialCoeffecient(2, 0), new PolynomialCoeffecient(4, 2)];
        var calculator = new PolynomialInflationCalculator(coeffecients);

        // Act/Assert
        for (var i = 0; i < 10; i++)
        {
            var result = calculator.Inflate(100, i);

            var expected = 100 + coeffecients.Sum(c => c.Value * Math.Pow(i, c.Power));
            Assert.Equal(expected, result);
        }
    }

    #endregion
}
