using System.Text.Json;
using UnitConverterApi.Models;

namespace UnitConverterApi.Services;

public class UnitDataProvider
{
    public Dictionary<string, UnitCategory> Categories { get; }

    public UnitDataProvider()
    {
        string json = File.ReadAllText("Data/units.json");

        var categories =
            JsonSerializer.Deserialize<Dictionary<string, UnitCategory>>(json)
            ?? new Dictionary<string, UnitCategory>();

        Categories = categories.ToDictionary(
            x => x.Key,
            x => x.Value,
            StringComparer.OrdinalIgnoreCase
        );

        foreach (var category in Categories.Values)
        {
            category.Units = category.Units.ToDictionary(
                x => x.Key,
                x => x.Value,
                StringComparer.OrdinalIgnoreCase
            );
        }
    }
}
