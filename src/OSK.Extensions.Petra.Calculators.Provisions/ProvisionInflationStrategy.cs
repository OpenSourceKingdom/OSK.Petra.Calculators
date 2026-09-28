using OSK.Petra.Calculators.Inflation.Ports;
using System;

namespace OSK.Extensions.Petra.Calculators.Provisions;

/// <summary>
/// Provides information to an inflation strategy that will be used for a particular provision
/// </summary>
/// <param name="provisionDefinitionId">The provision definition the inflation is associated with</param>
/// <param name="calculator">The calculator to utilize with the provision</param>
public readonly struct ProvisionInflationStrategy
{
    #region Variables

    /// <summary>
    /// The provision definition the inflation is associated with
    /// </summary>
    /// <remarks>
    /// 💡Notes:
    /// <list type="bullet">
    /// <item>A null definition id indicates that the strategy is a default/fallback strategy</item>
    /// </list>
    /// </remarks>
    public Guid? ProvisionDefinitionId { get; }

    /// <summary>
    /// The calculator to utilize with the provision
    /// </summary>
    public IInflationCalculator Calculator { get; }

    #endregion

    #region Constructors

    /// <summary>
    /// Creates an inflation strategy that is a fallback/default strategy and can be applied to any provision
    /// </summary>
    /// <param name="calculator">The calculator to use</param>
    /// <exception cref="ArgumentNullException">If the calculator is null</exception>
    public ProvisionInflationStrategy(IInflationCalculator calculator)
    {
        Calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
    }

    /// <summary>
    /// Creates an inflation strategy that is targeted to the specific provision with the provided calculator
    /// </summary>
    /// <param name="provisionDefinitionId">The target provisiont to use this strategy for</param>
    /// <param name="calculator">The calculator to use</param>
    /// <exception cref="ArgumentNullException">IF the calculator is null</exception>
    public ProvisionInflationStrategy(Guid provisionDefinitionId, IInflationCalculator calculator)
    {
        ProvisionDefinitionId = provisionDefinitionId;
        Calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
    }

    #endregion
}
