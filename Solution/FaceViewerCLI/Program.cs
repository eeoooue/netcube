namespace FaceViewerCLI;
using LibCubeIntegration.GetCubeStrategies;
using LibCubeIntegration.PerformMoveStrategies;
using LibNetCube;

class Program
{
    static readonly FacePresenter Presenter = new();
    static CubeState _cubeState = new(new CubePuzzle().GetState());

    static readonly IPerformMoveStrategy PerformMoveStrategy = new MoveViaApiStrategy();
    static readonly IGetCubeStrategy GetCubeStrategy = new GetCubeViaApiStrategy();

    static void Main()
    {
        while (true)
        {
            if (GetCubeStrategy.GetCube() is { } state)
            {
                _cubeState = state;
            }
            else
            {
                Console.WriteLine("Lost connection to server.");
                Thread.Sleep(500);
                Console.WriteLine("Attempting to reconnect...");
                continue;
            }

            Presenter.PresentCube(_cubeState);

            Console.WriteLine("Enter a move to be performed...");
            if (Console.ReadLine() is { } message) PerformMoveStrategy.PerformMove(message);
        }
    }
}
