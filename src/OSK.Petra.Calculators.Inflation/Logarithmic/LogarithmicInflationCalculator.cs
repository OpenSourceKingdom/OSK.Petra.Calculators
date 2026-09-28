using OSK.Petra.Calculators.Inflation.Models;
using System;

namespace OSK.Petra.Calculators.Inflation.Logarithmic;

/// <summary>
/// Provide a logarithmec inflation
/// </summary>
/// <param name="coefficient">The coefficient</param>
/// <param name="logBase">The base log</param>
public class LogarithmicInflationCalculator(double coefficient, double logBase = 10) : ScaleFactorInflationCalculator
{
    #region Variables

    private readonly double _base = logBase <= 1 ? throw new InvalidOperationException("Log base must be positive and greater than 1.") : logBase;

    #endregion

    #region ScaleFactorInflationCalculator Overrides

    /// <inheritdoc/>
    protected override double InflateValue(double baseValue, int count)
        => ScaleFactorMode switch
        {
            ScaleFactorMode.Multiplicative => count <= 1 ? 1 : coefficient * Math.Log(count, _base),
            _ => count <= 1 ? 0 : coefficient * Math.Log(count, _base)
        };

    #endregion
}
