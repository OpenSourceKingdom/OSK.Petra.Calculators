using OSK.Petra.Calculators.Inflation.Models;
using OSK.Petra.Calculators.Inflation.Polynomial;

namespace BlankStudios.Games.TowerDefenseLabs.UnitTests.Libraries.OSK.Game.Mechanics.Calculators.Inflation.Internal.Services;

public class PolynomialInflationInflationTests
{
    #region Inflate

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Inflate_NullOrEmptyTerms_ReturnsBaseValue(bool useNull)
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

    [Theory]
    [InlineData(ScaleFactorMode.Multiplicative)]
    [InlineData(ScaleFactorMode.Additive)]
    public void Inflate_SeveralTerms_AppliesScaleFactorMode_SubstitueCountWithCoeffecients(ScaleFactorMode scaleFactorMode)
    {
        // Arrange
        PolynomialTerm[] terms = [new PolynomialTerm(2, 0), new PolynomialTerm(4, 2)];
        var calculator = new PolynomialInflationCalculator(terms)
        {
            ScaleFactorMode = scaleFactorMode
        };

        // Act/Assert
        for (var i = 0; i < 10; i++)
        {
            var result = calculator.Inflate(100, i);

            var expected = scaleFactorMode is ScaleFactorMode.Additive
                ? 100 + terms.Sum(term => term.Coefficient * Math.Pow(i, term.Power))
                : 100 * terms.Sum(term => term.Coefficient * Math.Pow(i, term.Power));
            Assert.Equal(expected, result);
        }
    }

    [Theory]
    [InlineData(200, 2, 0, 200)]
    [InlineData(200, 2, 1, 202)]
    [InlineData(200, 2, 2, 204)]
    [InlineData(200, 2, 3, 206)]
    [InlineData(400, 2, 2, 404)]
    public void Inflate_LinearInflation_AdditiveScaling_ReturnsExpectedOutput(double startingValue, double coefficient, int count, double expectedValue)
    {
        // Arrange
        var calculator = PolynomialInflationCalculator.LinearAdd(coefficient);

        // Act
        var result = calculator.Inflate(startingValue, count);

        // Assert
        Assert.Equal(expectedValue, result);
    }

    [Theory]
    [InlineData(200, 2, 0, 0)]
    [InlineData(200, 2, 1, 400)]
    [InlineData(200, 2, 2, 800)]
    [InlineData(200, 2, 3, 1200)]
    [InlineData(400, 2, 2, 1600)]
    public void Inflate_LinearInflation_MultiplicativeScaling_ReturnsExpectedOutput(double startingValue, double coefficient, int count, double expectedValue)
    {
        // Arrange
        var calculator = PolynomialInflationCalculator.LinearMultiply(coefficient);

        // Act
        var result = calculator.Inflate(startingValue, count);

        // Assert
        Assert.Equal(expectedValue, result);
    }

    [Theory]
    [InlineData(100, 0)]
    [InlineData(200, 1)]
    [InlineData(300, 2)]
    [InlineData(400, 3)]
    [InlineData(500, 4)]
    public void Inflate_ConstantInflation_AdditiveScaling_ReturnsStartingValue(double startingValue, int count)
    {
        // Arrange
        var calculator = PolynomialInflationCalculator.ConstantAdd();

        // Act
        var result = calculator.Inflate(startingValue, count);

        // Assert
        Assert.Equal(startingValue, result);
    }

    [Theory]
    [InlineData(100, 0)]
    [InlineData(200, 1)]
    [InlineData(300, 2)]
    [InlineData(400, 3)]
    [InlineData(500, 4)]
    public void Inflate_ConstantInflation_MultiplicativeScaling_ReturnsStartingValue(double startingValue, int count)
    {
        // Arrange
        var calculator = PolynomialInflationCalculator.ConstantMultiply(1);

        // Act
        var result = calculator.Inflate(startingValue, count);

        // Assert
        Assert.Equal(startingValue, result);
    }

    #endregion
}
