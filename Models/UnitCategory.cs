namespace UnitConverterApi.Models;

public class UnitCategory
{
    public string BaseUnit { get; set; } = string.Empty;

    public Dictionary<string, double> Units { get; set; } = new();
}
