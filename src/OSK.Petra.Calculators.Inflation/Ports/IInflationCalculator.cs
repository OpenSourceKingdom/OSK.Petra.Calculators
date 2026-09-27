namespace OSK.Petra.Calculators.Inflation.Ports;

public interface IInflationCalculator
{
    double Inflate(double baseValue, int quantity);
}
