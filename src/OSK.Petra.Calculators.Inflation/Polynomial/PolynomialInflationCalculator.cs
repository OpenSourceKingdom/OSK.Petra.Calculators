using OSK.Petra.Calculators.Inflation.Ports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Petra.Calculators.Inflation.Polynomial;

public class PolynomialInflationCalculator: IInflationCalculator
{
    #region Variables

    private readonly IEnumerable<PolynomialCoeffecient> _coeffecients;

    #endregion

    #region Constructors

    public PolynomialInflationCalculator(IEnumerable<PolynomialCoeffecient> coefficients)
    {
        _coeffecients = coefficients;
    }

    public PolynomialInflationCalculator(params PolynomialCoeffecient[] coefficients)
    {
        _coeffecients = coefficients;
    }

    #endregion

    #region IInflationCalculator

    public double Inflate(double baseValue, int quantity)
        => baseValue + (_coeffecients?.Sum(coeffecient => coeffecient.Value * Math.Pow(quantity, coeffecient.Power)) ?? 0);

    #endregion
}
