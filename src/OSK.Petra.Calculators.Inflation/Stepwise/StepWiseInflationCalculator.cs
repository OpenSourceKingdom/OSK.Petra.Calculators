using OSK.Petra.Calculators.Inflation.Ports;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Petra.Calculators.Inflation.Stepwise;

public class StepWiseInflationCalculator : IInflationCalculator
{
    #region Variables

    private readonly InflationStep[] _steps;

    private int _stepIndex = 0;

    #endregion

    #region Constructors

    public StepWiseInflationCalculator(IEnumerable<InflationStep> steps)
    {
        _steps = steps?.OrderBy(step => step.Quantity).ToArray() ?? [];
    }

    public StepWiseInflationCalculator(params InflationStep[] steps)
    {
        _steps = steps?.OrderBy(step => step.Quantity).ToArray() ?? [];
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
        if (step.Quantity > quantity)
        {
            while (_stepIndex > 0 && _steps[_stepIndex - 1].Quantity >= quantity)
            {
                _stepIndex--;
                step = _steps[_stepIndex];
            }
        }
        else if (step.Quantity < quantity)
        {
            while (_stepIndex < _steps.Length - 1 && _steps[_stepIndex + 1].Quantity <= quantity)
            {
                _stepIndex++;
                step = _steps[_stepIndex];
            }
        }

        return step.Quantity > quantity
            ? baseValue
            : step.Value;
    }

    #endregion
}
