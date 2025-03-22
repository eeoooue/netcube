namespace LibCubeIntegration.PerformMoveStrategies;
public class MoveViaApiStrategy : IPerformMoveStrategy
{
    static readonly HttpClient Client = new();
    const string ServerAddress = "http://localhost:5295";

    public void PerformMove(string move)
    {
        var task = PerformMoveAsync(move);
        task.Wait();
        Console.WriteLine($"verdict: {task.Result}");
    }

    static async Task<bool> PerformMoveAsync(string move)
    {
        var requestUri = $"{ServerAddress}/api/Cube/PerformMove?move={move}";
        var response = await Client.PostAsync(requestUri, null);
        return response.IsSuccessStatusCode;
    }
}
