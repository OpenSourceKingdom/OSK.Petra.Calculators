namespace OSK.Petra.Calculators.Inflation.Models;

public enum ScaleFactorMode
{
    /// <summary>
    /// The general case: baseValue + (coefficient * log).
    /// </summary>
    Additive,

    /// <summary>
    /// The multiplicative case: scales the base value by the logarithmic factor.
    /// </summary>
    Multiplicative
}