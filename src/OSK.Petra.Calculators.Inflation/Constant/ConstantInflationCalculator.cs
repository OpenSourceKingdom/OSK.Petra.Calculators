using OSK.Petra.Calculators.Inflation.Ports;

namespace OSK.Petra.Calculators.Inflation.Constant;

public class ConstantInflationCalculator: IInflationCalculator
{
    #region IInflationCalculator

    public double Inflate(double baseValue, int quantity)
        => baseValue;

    #endregion
}
