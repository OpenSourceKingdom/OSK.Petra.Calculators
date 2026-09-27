using OSK.Petra.Calculators.Inflation.Exponential;

namespace BlankStudios.Games.TowerDefenseLabs.UnitTests.Libraries.OSK.Game.Mechanics.Calculators.Inflation.Internal.Services;

public class ExponentialInflationCalculatorTests
{
    #region Inflate

    [Fact]
    public void Inflate_ExponentiallyGrowsAsExpected()
    {
        // Arrange
        var calculator = new ExponentialInflationCalculator(5);

        // Act/Assert
        var result = calculator.Inflate(100, 2);
        Assert.Equal(25 * 100, result);

        result = calculator.Inflate(5, 4);
        Assert.Equal(3125, result);
    }

    #endregion
}
