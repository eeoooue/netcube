namespace LibCubeIntegration.PerformMoveStrategies;
using System.Net.Sockets;
using System.Text;
using LibNetCube;

class MoveViaSocketStrategy : IPerformMoveStrategy
{
    const string Hostname = "127.0.0.1";
    const int Port = 5000;

    public void PerformMove(string move)
    {
        SendMoveRequest(move);
    }

    public static CubeState? SendMoveRequest(string message)
    {
        try
        {
            var client = new TcpClient();

            client.Connect(Hostname, Port);
            var nStream = client.GetStream();

            var request = Serialize(message);
            nStream.Write(request, 0, request.Length);

            var received = ReadFromStream(nStream);

            client.Close();
            client.Dispose();

            return new CubeState(received);
        }
        catch
        {
            return null;
        }
    }

    public static byte[] ReadFromStream(NetworkStream stream)
    {
        var messageLength = stream.ReadByte();
        var messageBytes = new byte[messageLength];
        stream.ReadExactly(messageBytes, 0, messageLength);
        return messageBytes;
    }

    public static byte[] Serialize(string request)
    {
        var responseBytes = Encoding.ASCII.GetBytes(request);
        var responseLength = (byte)responseBytes.Length;

        var rawData = new byte[responseLength + 1];
        rawData[0] = responseLength;
        responseBytes.CopyTo(rawData, 1);
        return rawData;
    }
}
