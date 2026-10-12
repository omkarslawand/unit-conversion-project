using UnitConverterApi.Models;

namespace UnitConverterApi.Services;

public interface ITemperatureConversionService
{
    double Convert(string fromUnit, string toUnit, double value);
}
