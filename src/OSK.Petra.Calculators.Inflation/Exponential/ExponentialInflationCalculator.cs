using OSK.Petra.Calculators.Inflation.Ports;
using System;

namespace OSK.Petra.Calculators.Inflation.Exponential;

/// <summary>
/// Provides an exponential rate inflation
/// </summary>
/// <param name="exponent">The exponent power</param>
public class ExponentialInflationCalculator(double exponent) : ScaleFactorInflationCalculator
{
    #region ScaleFactorInflationCalculator Overrides

    /// <inheritdoc/>
    protected override double InflateValue(double baseValue, int count)
        => Math.Pow(count, exponent);

    #endregion
}
