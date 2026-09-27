using OSK.Petra.Calculators.Inflation.Ports;

namespace OSK.Petra.Calculators.Inflation.Linear;

public class LinearInflationCalculator(double constant) : IInflationCalculator
{
    #region IInflationCalculator

    public double Inflate(double baseValue, int quantity)
        => quantity * baseValue * constant;

    #endregion
}
