using OSK.Petra.Calculators.Inflation.Ports;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Petra.Calculators.Inflation.Stepwise;

/// <summary>
/// Provides inflation through a step wise function
/// </summary>
public class StepWiseInflationCalculator : IInflationCalculator
{
    #region Variables

    private readonly InflationStep[] _steps;

    private int _stepIndex = 0;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates an inflation calculator using a collection of <see cref="InflationStep"/>
    /// </summary>
    /// <param name="steps">The steps for the stepwise inflation</param>
    public StepWiseInflationCalculator(IEnumerable<InflationStep> steps)
    {
        _steps = steps?.OrderBy(step => step.Count).ToArray() ?? [];
    }

    /// <summary>
    /// Creates an inflation calculator using a parameter list of <see cref="InflationStep"/>
    /// </summary>
    /// <param name="steps">The steps for the stepwise inflation</param>
    public StepWiseInflationCalculator(params InflationStep[] steps)
    {
        _steps = steps?.OrderBy(step => step.Count).ToArray() ?? [];
    }

    #endregion

    #region IInflationCalculator

    public double Inflate(double baseValue, int quantity)
    {
        if (_steps is null || _steps.Length is 0)
        {
            return baseValue;
        }

        var step = _steps[_stepIndex];
        if (step.Count > quantity)
        {
            while (_stepIndex > 0 && _steps[_stepIndex - 1].Count >= quantity)
            {
                _stepIndex--;
                step = _steps[_stepIndex];
            }
        }
        else if (step.Count < quantity)
        {
            while (_stepIndex < _steps.Length - 1 && _steps[_stepIndex + 1].Count <= quantity)
            {
                _stepIndex++;
                step = _steps[_stepIndex];
            }
        }

        return step.Count > quantity
            ? baseValue
            : step.Value;
    }

    #endregion
}
