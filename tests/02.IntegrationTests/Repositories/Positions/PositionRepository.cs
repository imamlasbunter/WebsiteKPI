using Newtonsoft.Json;
using Pertamina.Website_KPI.IntegrationTests.Repositories.Positions.Models;

namespace Pertamina.Website_KPI.IntegrationTests.Repositories.Positions;
public static class PositionRepository
{
    private static readonly string _filePath = Path.Combine(AppContext.BaseDirectory, nameof(Repositories), nameof(Positions), "positions.json");

    public static List<Position> Positions => JsonConvert.DeserializeObject<List<Position>>(File.ReadAllText(_filePath))!;
}
