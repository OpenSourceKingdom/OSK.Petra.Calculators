using OSK.Petra.Calculators.Inflation.Ports;
using OSK.Petra.Provisions.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Extensions.Petra.Calculators.Provisions;

/// <summary>
/// A special inflation calculator for collections of provisions
/// </summary>
public class ProvisionInflationCalculator
{
    #region Variables

    private readonly IInflationCalculator? _defaultCalculator;
    private readonly Dictionary<Guid, IInflationCalculator> _provisionInflationCalculators = [];

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a provision inflation calculator that uses a standard inflation calculation for all provisions
    /// </summary>
    /// <param name="calculator">The standard calculator to use</param>
    /// <exception cref="ArgumentNullException">If the calculator is null</exception>
    public ProvisionInflationCalculator(IInflationCalculator calculator)
    {
        _defaultCalculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
    }

    /// <summary>
    /// Creates a provision inflation calculator that uses a collection of inflation strategies
    /// </summary>
    /// <param name="inflationStrategies">The inflation strategies to use</param>
    /// <exception cref="ArgumentNullException">If strategies is null</exception>
    public ProvisionInflationCalculator(IEnumerable<ProvisionInflationStrategy> inflationStrategies)
    {
        if (inflationStrategies is null)
        {
            throw new ArgumentNullException(nameof(inflationStrategies));
        }

        foreach (var inflationMeasure in inflationStrategies)
        {
            if (inflationMeasure.ProvisionDefinitionId is null)
            {
                _defaultCalculator = inflationMeasure.Calculator;
            }
            else
            {
                _provisionInflationCalculators[inflationMeasure.ProvisionDefinitionId.Value] = inflationMeasure.Calculator;
            }
        }
    }

    #endregion

    #region IInflationCalculator

    /// <summary>
    /// Inflates the provided provisions based on the
    /// </summary>
    /// <param name="baseProvisions">The initial base provisions to be inflated</param>
    /// <param name="count">The count/iteration for the inflation</param>
    /// <returns>The inflated provisions</returns>
    public IEnumerable<Provision> Inflate(IEnumerable<Provision> baseProvisions, int count)
    {
        if (baseProvisions is null || !baseProvisions.Any())
        {
            yield break;
        }

        foreach (var baseProvision in baseProvisions)
        {
            var inflationCalculator = _provisionInflationCalculators.TryGetValue(baseProvision.Id, out var calculator)
                ? calculator
                : _defaultCalculator;

            yield return inflationCalculator is null
                ? baseProvision
                : new(baseProvision.Id, (float)inflationCalculator.Inflate(baseProvision.Amount, count));
        }
    }

    #endregion
}
