using OSK.Petra.Calculators.Inflation.Models;
using OSK.Petra.Calculators.Inflation.Ports;

namespace OSK.Petra.Calculators.Inflation;

/// <summary>
/// A calculator that is able to perform scaling inflation with respect to the base in varying ways, depending on the <see cref="ScaleFactorMode"/>
/// </summary>
public abstract class ScaleFactorInflationCalculator : IInflationCalculator
{
    #region Variables

    /// <summary>
    /// Determines how the calculator scales inflation with thw base value 
    /// </summary>
    public ScaleFactorMode ScaleFactorMode { get; init; } = ScaleFactorMode.Additive;

    #endregion

    #region IInflationCalculator

    /// <inheritdoc/>
    public double Inflate(double baseValue, int count)
        => ScaleFactorMode switch
        {
            ScaleFactorMode.Multiplicative => baseValue * InflateValue(baseValue, count),
            _ => baseValue + InflateValue(baseValue, count)
        };

    #endregion

    #region Helpers

    /// <summary>
    /// Inflates the value using the internal calculator forumula
    /// </summary>
    /// <param name="baseValue">The initial base value to inflate</param>
    /// <param name="count">The count of inflation to calculate</param>
    /// <returns>The inflated amount</returns>
    protected abstract double InflateValue(double baseValue, int count);

    #endregion
}
