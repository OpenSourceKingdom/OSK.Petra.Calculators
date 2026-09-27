using OSK.Petra.Calculators.Inflation.Ports;
using System;

namespace OSK.Petra.Calculators.Inflation.Exponential;

public class ExponentialInflationCalculator(double coeffecient) : IInflationCalculator
{
    #region IInflationCalculator

    public double Inflate(double baseValue, int quantity)
        => baseValue * Math.Pow(coeffecient, quantity);

    #endregion
}
