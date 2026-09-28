using OSK.Petra.Provisions.Models;
using System.Collections.Generic;

namespace OSK.Extensions.Petra.Calculators.Provisions;

/// <summary>
/// A calculator that is able to perform inflation calculations for a variety of provisions
/// </summary>
public interface IProvisionInflationCalculator
{
    /// <summary>
    /// Inflates the provided provisions based on the
    /// </summary>
    /// <param name="baseProvisions">The initial base provisions to be inflated</param>
    /// <param name="count">The count/iteration for the inflation</param>
    /// <returns>The inflated provisions</returns>
    IEnumerable<Provision> Inflate(IEnumerable<Provision> baseProvisions, int count);
}
