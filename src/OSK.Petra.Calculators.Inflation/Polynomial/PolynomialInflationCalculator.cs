using OSK.Petra.Calculators.Inflation.Models;
using OSK.Petra.Calculators.Inflation.Ports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Petra.Calculators.Inflation.Polynomial;

/// <summary>
/// Creates an inflation calculator that utilizes polynomial equations to inflate a value
/// </summary>
public class PolynomialInflationCalculator: ScaleFactorInflationCalculator
{
    #region Static

    /// <summary>
    /// Creates a polynomial inflation that only uses a base value and constant, using additive scaling
    /// </summary>
    /// <param name="constant">The constant to add</param>
    /// <returns>The calculator</returns>
    public static PolynomialInflationCalculator ConstantAdd(double constant = 0)
        => new([new PolynomialTerm(constant, 0)]);

    /// <summary>
    /// Creates a polynomial inflation that only uses a base value and constant, using multiplicative scaling
    /// </summary>
    /// <param name="constant">The constant to add</param>
    /// <returns>The calculator</returns>
    public static PolynomialInflationCalculator ConstantMultiply(double constant = 0)
        => new([new PolynomialTerm(constant, 0)])
        {
            ScaleFactorMode = ScaleFactorMode.Multiplicative
        };

    /// <summary>
    /// Creates a linear inflation that only uses a single coeffecient and constant, using additive scaling
    /// </summary>
    /// <param name="coeffecient">The coefficient for the linear term</param>
    /// <param name="constant">The constant to add</param>
    /// <returns>The calculator</returns>
    public static PolynomialInflationCalculator LinearAdd(double coeffecient, double constant = 0)
        => new([new PolynomialTerm(constant, 0), new PolynomialTerm(coeffecient, 1)]);

    /// <summary>
    /// Creates a linear inflation that only uses a single coeffecient and constant, using multiplicative scaling
    /// </summary>
    /// <param name="coeffecient">The coefficient for the linear term</param>
    /// <param name="constant">The constant to add</param>
    /// <returns>The calculator</returns>
    public static PolynomialInflationCalculator LinearMultiply(double coeffecient, double constant = 0)
        => new([new PolynomialTerm(constant, 0), new PolynomialTerm(coeffecient, 1)])
        {
            ScaleFactorMode = ScaleFactorMode.Multiplicative
        };

    /// <summary>
    /// Creates a polynomial inflation that uses the given coeffecients in the order they appear as index power terms with an additive constant, using additive scaling
    /// </summary>
    /// <param name="coeffecients">The coefficients of the terms</param>
    /// <returns>The calculator</returns>
    public static PolynomialInflationCalculator PolynomialAdd(params double[] coeffecients)
        => new(coeffecients.Select((coeffecient, index) => new PolynomialTerm(coeffecient, index)));

    /// <summary>
    /// Creates a polynomial inflation that uses the given coeffecients in the order they appear as index power terms with an multiplicative constant, using additive scaling
    /// </summary>
    /// <param name="coeffecients">The coefficients of the terms</param>
    /// <returns>The calculator</returns>
    public static PolynomialInflationCalculator PolynomialMultiply(params double[] coeffecients)
        => new(coeffecients.Select((coeffecient, index) => new PolynomialTerm(coeffecient, index)))
        {
            ScaleFactorMode = ScaleFactorMode.Multiplicative
        };

    #endregion

    #region Variables

    private readonly IEnumerable<PolynomialTerm> _terms;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a polynomial calculators using the specified terms and constant
    /// </summary>
    /// <param name="terms">The collection of polynomial terms</param>
    public PolynomialInflationCalculator(IEnumerable<PolynomialTerm> terms)
    {
        _terms = terms;
    }

    #endregion

    #region ScaleFactorInflationCalculator Overrides

    /// <inheritdoc/>
    protected override double InflateValue(double baseValue, int count)
        => _terms?.Sum(term => term.Coefficient * Math.Pow(count, term.Power)) ?? (ScaleFactorMode is ScaleFactorMode.Additive ? 0 : 1) ;

    #endregion
}
