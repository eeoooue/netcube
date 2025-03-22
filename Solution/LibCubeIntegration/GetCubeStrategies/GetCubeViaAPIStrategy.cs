namespace LibCubeIntegration.GetCubeStrategies;
using LibNetCube;

public class GetCubeViaApiStrategy : IGetCubeStrategy
{
    static readonly HttpClient Client = new();
    const string ServerAddress = "http://localhost:5295";

    public CubeState? GetCube()
    {
        var task = GetCubeAsync();
        task.Wait();

        return task.Result;
    }

    static async Task<CubeState?> GetCubeAsync()
    {
        List<string> faces = ["Back", "Bottom", "Front", "Left", "Right", "Top"];
        var faceMatrices = new List<int[,]>();

        foreach (var face in faces)
        {
            var result = await GetFaceAsync(face);
            if (result != null)
                faceMatrices.Add(result);
            else
                return null;
        }

        return faceMatrices.Count == 6 ? new CubeState(faceMatrices) : null;
    }

    static async Task<int[,]?> GetFaceAsync(string face)
    {
        var requestUri = $"{ServerAddress}/api/Cube/Face?face={face}";
        var response = await Client.GetAsync(requestUri);
        var responseString = await response.Content.ReadAsStringAsync();
        return ExtractFaceMatrix(responseString);
    }

    static int[,]? ExtractFaceMatrix(string response)
    {
        try
        {
            response = response.Substring(1, response.Length - 2);
            var values = response.Split(',');
            var result = new int[3, 3];

            for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
            {
                var p = i * 3 + j;
                result[i, j] = int.Parse(values[p]);
            }

            return result;
        }
        catch
        {
            return null;
        }
    }
}
