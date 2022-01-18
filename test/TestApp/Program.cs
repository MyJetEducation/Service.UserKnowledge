using System;
using System.Threading.Tasks;
using ProtoBuf.Grpc.Client;
using Service.UserProgress.Client;
using Service.UserProgress.Grpc;

namespace TestApp
{
	public class Program
	{
		private static async Task Main(string[] args)
		{
			GrpcClientFactory.AllowUnencryptedHttp2 = true;

			Console.Write("Press enter to start");
			Console.ReadLine();

			var factory = new UserProgressClientFactory("http://localhost:5001");
			IUserProgressService client = factory.GetUserProgressService();

			Console.WriteLine("End");
			Console.ReadLine();
		}
	}
}