using OSK.Petra.Calculators.Inflation.Constant;

namespace BlankStudios.Games.TowerDefenseLabs.UnitTests.Libraries.OSK.Game.Mechanics.Calculators.Inflation.Internal.Services;

public class ConstantInflationCalculatorTests
{
    #region Inflate

    [Theory]
    [InlineData(100, 0)]
    [InlineData(200, 1)]
    [InlineData(300, 2)]
    [InlineData(400, 3)]
    [InlineData(500, 4)]
    public void Inflate_ConstantInflation_ReturnsStartingValue(double startingValue, int quantity)
    {
        // Arrange
        var calculator = new ConstantInflationCalculator();

        // Act
        var result = calculator.Inflate(startingValue, quantity);

        // Assert
        Assert.Equal(startingValue, result);
    }

    #endregion
}
