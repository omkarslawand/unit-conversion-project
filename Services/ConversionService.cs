namespace UnitConverterApi.Services;

public class ConversionService : IConversionService
{
    private readonly UnitDataProvider _provider;
    private readonly ITemperatureConversionService _temperatureConversionService;

    public ConversionService(
        UnitDataProvider provider,
        ITemperatureConversionService temperatureConversionService
    )
    {
        _provider = provider;
        _temperatureConversionService = temperatureConversionService;
    }

    public ApiResponse<double> Convert(
        string category,
        string fromUnit,
        string toUnit,
        double value
    )
    {
        if (!_provider.Categories.TryGetValue(category, out var categoryData))
        {
            return new ApiResponse<double>
            {
                Success = false,
                Message = $"Invalid category '{category}'.",
                Data = 0
            };
        }

        if (!categoryData.Units.ContainsKey(fromUnit))
        {
            return new ApiResponse<double>
            {
                Success = false,
                Message = $"Unit '{fromUnit}' does not exist in category '{category}",
            };
        }

        if (!categoryData.Units.ContainsKey(toUnit))
        {
            return new ApiResponse<double>
            {
                Success = false,
                Message = $"Unit '{toUnit}' does not exist in category '{category}",
            };
        }

        if (category.Equals("Temperature", StringComparison.OrdinalIgnoreCase))
        {
            var convertedTempreture = _temperatureConversionService.Convert(
                fromUnit,
                toUnit,
                value
            );

            return new ApiResponse<double>
            {
                Success = true,
                Message = $"Converted successfully from '{fromUnit}' to '{toUnit}'.",
                Data = convertedTempreture,
            };
        }
        double valueInBaseUnit = value * categoryData.Units[fromUnit];

        double result = valueInBaseUnit / categoryData.Units[toUnit];

        return new ApiResponse<double>
        {
            Success = true,
            Message = $"Converted successfully from '{fromUnit}' to '{toUnit}'.",
            Data = result,
        };
    }

    public ApiResponse<List<string>> GetAllCategories()
    {
        return new ApiResponse<List<string>>
        {
            Success = true,
            Message = $"Categories fetched successfully.",
            Data = _provider.Categories.Keys.ToList(),
        };
    }

    public ApiResponse<List<string>> GetAllUnitsInCategory(string category)
    {
        if (!_provider.Categories.TryGetValue(category, out var categoryData))
        {
            return new ApiResponse<List<string>>
            {
                Success = false,
                Message = $"Category '{category}' does not exist.",
            };
        }

        var units = categoryData.Units.Keys.ToList();

        return new ApiResponse<List<string>>
        {
            Success = true,
            Message = $"Units fetched successfully from category '{category}'.",
            Data = units,
        };
    }
}
