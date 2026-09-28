using OSK.Hexagonal.MetaData;

namespace OSK.Petra.Calculators.Inflation.Ports;

/// <summary>
/// Represents a calculator designed to handle inflationary math
/// </summary>
[HexagonalIntegration(HexagonalIntegrationType.LibraryProvided)]
public interface IInflationCalculator
{
    /// <summary>
    /// Inflates a base value by the count
    /// </summary>
    /// <param name="baseValue">The initial base value to inflate</param>
    /// <param name="count">The count of inflation to calculate</param>
    /// <returns>The inflated amount</returns>
    double Inflate(double baseValue, int count);
}
