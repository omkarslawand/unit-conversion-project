namespace UnitConverterApi.Services;

public interface IConversionService
{
    ApiResponse<double> Convert(string category, string fromUnit, string toUnit, double value);
    ApiResponse<List<string>> GetAllCategories();
    ApiResponse<List<string>> GetAllUnitsInCategory(string category);
}
