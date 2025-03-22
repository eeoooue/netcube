namespace LibCubeIntegration.GetCubeStrategies;
using LibNetCube;
using PerformMoveStrategies;

class GetCubeViaSocketStrategy : IGetCubeStrategy
{
    public CubeState? GetCube()
    {
        var result = MoveViaSocketStrategy.SendMoveRequest("X");

        return result;
    }
}
