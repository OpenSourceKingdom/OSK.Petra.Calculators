using OSK.Petra.Calculators.Inflation.Logarithmic;
using OSK.Petra.Calculators.Inflation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Petra.Calculators.Inflation.UnitTests.Logarithmic;

public class LogarithmicInflationCalculatorTests
{
    #region Constructor Tests

    [Theory]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(-5.0)]
    public void Constructor_InvalidLogBase_ThrowsInvalidOperationException(double invalidBase)
    {
        Assert.Throws<InvalidOperationException>(() => new LogarithmicInflationCalculator(2.0, invalidBase));
    }

    #endregion

    #region Inflate

    [Theory]
    // count <= 1 should yield baseValue
    [InlineData(100.0, 2.0, 10.0, 0, 100.0)]
    [InlineData(100.0, 2.0, 10.0, 1, 100.0)]
    // count > 1: baseValue + (coefficient * Math.Log(count, base))
    // e.g., count = 10, base = 10 -> Math.Log(10, 10) = 1 -> 100 + (2 * 1) = 102
    [InlineData(100.0, 2.0, 10.0, 10, 102.0)]
    // e.g., count = 100, base = 10 -> Math.Log(100, 10) = 2 -> 50 + (3 * 2) = 56
    [InlineData(50.0, 3.0, 10.0, 100, 56.0)]
    public void Inflate_AdditiveMode_ReturnsExpectedResult(double baseValue, double coefficient, double logBase, int count, double expected)
    {
        // Arrange
        var calculator = new LogarithmicInflationCalculator(coefficient, logBase)
        {
            ScaleFactorMode = ScaleFactorMode.Additive
        };

        // Act
        double result = calculator.Inflate(baseValue, count);

        // Assert
        Assert.Equal(expected, result, precision: 5);
    }

    [Theory]
    // count <= 1 should yield baseValue
    [InlineData(100.0, 2.0, 10.0, 0, 100.0)]
    [InlineData(100.0, 2.0, 10.0, 1, 100.0)]
    // count > 1: baseValue * (coefficient * Math.Log(count, base))
    // e.g., count = 10, base = 10 -> Math.Log(10, 10) = 1 -> 100 * (2 * 1) = 200
    [InlineData(100.0, 2.0, 10.0, 10, 200.0)]
    // e.g., count = 100, base = 10 -> Math.Log(100, 10) = 2 -> 50 * (3 * 2) = 300
    [InlineData(50.0, 3.0, 10.0, 100, 300.0)]
    public void Inflate_MultiplicativeMode_ReturnsExpectedResult(double baseValue, double coefficient, double logBase, int count, double expected)
    {
        // Arrange
        var calculator = new LogarithmicInflationCalculator(coefficient, logBase)
        {
            ScaleFactorMode = ScaleFactorMode.Multiplicative
        };

        // Act
        double result = calculator.Inflate(baseValue, count);

        // Assert
        Assert.Equal(expected, result, precision: 5);
    }

    #endregion
}
