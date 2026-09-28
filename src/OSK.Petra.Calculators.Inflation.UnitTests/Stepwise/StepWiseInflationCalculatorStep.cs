using OSK.Petra.Calculators.Inflation.Stepwise;

namespace BlankStudios.Games.TowerDefenseLabs.UnitTests.Libraries.OSK.Game.Mechanics.Calculators.Inflation.Internal.Services;

public class StepWiseInflationCalculatorStep
{
    #region Inflate

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Inflate_NullOrEmptySteps_ReturnsStartingValue(bool useNull)
    {
        // Arrange
        var calculator = new StepWiseInflationCalculator(useNull ? null! : []);

        // Act/Assert
        for (var i = 0; i < 10; i++)
        {
            var result = calculator.Inflate(200, i);

            Assert.Equal(200, result);
        }
    }

    [Fact]
    public void Inflate_ValidSteps_ReturnsValueMeetingStepCriteria()
    {
        // Arrange
        var steps = new InflationStep[]
        {
            new InflationStep(2, 2),
            new InflationStep(4, 5),
            new InflationStep(8, 6),
            new InflationStep(10, 8)
        };

        var calculator = new StepWiseInflationCalculator(steps);

        // Act/Assert
        for (var i = 0; i < 15; i++)
        {
            var result = calculator.Inflate(100, i);

            if (i < 2)
            {
                Assert.Equal(100, result);
            }
            else if (i < 4)
            {
                Assert.Equal(2, result);
            }
            else if (i < 8)
            {
                Assert.Equal(5, result);
            }
            else if (i < 10)
            {
                Assert.Equal(6, result);
            }
            else
            {
                Assert.Equal(8, result);
            }
        }
    }

    #endregion
}
