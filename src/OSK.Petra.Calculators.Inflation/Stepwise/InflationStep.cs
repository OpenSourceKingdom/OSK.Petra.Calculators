namespace OSK.Petra.Calculators.Inflation.Stepwise;

/// <summary>
/// Represents a step tier in a stepwise function
/// </summary>
/// <param name="Count">The step count that this tier is for</param>
/// <param name="Value">The value for the step</param>
public readonly record struct InflationStep(int Count, double Value);
