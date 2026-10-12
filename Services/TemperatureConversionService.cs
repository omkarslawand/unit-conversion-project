namespace UnitConverterApi.Services;

public class TemperatureConversionService : ITemperatureConversionService
{
    public double Convert(string fromUnit, string toUnit, double value)
    {
        if (fromUnit == toUnit)
        {
            return value;
        }

        // Step 1:
        // Convert source to Celsius

        double celsiusValue = ConvertToCelsius(fromUnit, value);

        // Step 2:
        // Convert Celsius to target

        return ConvertFromCelsius(toUnit, celsiusValue);
    }

    private double ConvertToCelsius(string fromUnit, double value)
    {
        return fromUnit switch
        {
            "Celsius" => value,

            "Fahrenheit" => (value - 32) * 5 / 9,

            "Kelvin" => value - 273.15,

            _ => throw new Exception($"Unsupported unit '{fromUnit}'."),
        };
    }

    private double ConvertFromCelsius(string toUnit, double value)
    {
        return toUnit switch
        {
            "Celsius" => value,

            "Fahrenheit" => (value * 9 / 5) + 32,

            "Kelvin" => value + 273.15,

            _ => throw new Exception($"Unsupported unit '{toUnit}'."),
        };
    }
}
