using OSK.Petra.Calculators.Inflation.Exponential;
using OSK.Petra.Calculators.Inflation.Models;

namespace BlankStudios.Games.TowerDefenseLabs.UnitTests.Libraries.OSK.Game.Mechanics.Calculators.Inflation.Internal.Services;

public class ExponentialInflationCalculatorTests
{
    #region Inflate

    [Theory]
    [InlineData(ScaleFactorMode.Additive)]
    [InlineData(ScaleFactorMode.Multiplicative)]
    public void Inflate_AdditiveScaleFactor_ExponentiallyGrowsAsExpected(ScaleFactorMode scaleFactorMode)
    {
        // Arrange
        var exponent = 5;
        var calculator = new ExponentialInflationCalculator(exponent)
        {
            ScaleFactorMode = scaleFactorMode
        };

        // Act/Assert
        var result = calculator.Inflate(100, 2);

        var expected = scaleFactorMode is ScaleFactorMode.Additive
            ? Math.Pow(2, exponent) + 100
            : Math.Pow(2, exponent) * 100;
        Assert.Equal(expected, result);

        result = calculator.Inflate(5, 4);
        expected = scaleFactorMode is ScaleFactorMode.Additive
            ? Math.Pow(4, exponent) + 5
            : Math.Pow(4, exponent) * 5;
        Assert.Equal(expected, result);
    }

    #endregion
}
