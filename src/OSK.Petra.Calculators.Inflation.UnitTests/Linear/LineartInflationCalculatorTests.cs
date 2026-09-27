using OSK.Petra.Calculators.Inflation.Linear;

namespace BlankStudios.Games.TowerDefenseLabs.UnitTests.Libraries.OSK.Game.Mechanics.Calculators.Inflation.Internal.Services;

public class LineartInflationCalculatorTests
{
    #region Inflate

    [Theory]
    [InlineData(200, 2, 0, 0)]
    [InlineData(200, 2, 1, 400)]
    [InlineData(200, 2, 2, 800)]
    [InlineData(200, 2, 3, 1200)]
    [InlineData(400, 2, 2, 1600)]
    public void Inflate_LinearInflation_ReturnsExpectedOutput(double startingValue, double constant, int quantity, double expectedValue)
    {
        // Arrange
        var calculator = new LinearInflationCalculator(constant);

        // Act
        var result = calculator.Inflate(startingValue, quantity);

        // Assert
        Assert.Equal(expectedValue, result);
    }

    #endregion
}
