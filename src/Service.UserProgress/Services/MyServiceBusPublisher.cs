using System.Threading.Tasks;
using DotNetCoreDecorators;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.TcpClient;
using Service.UserProgress.Domain.Models;

namespace Service.UserProgress.Services
{
	public class MyServiceBusPublisher : IPublisher<UserProgressUpdatedServiceBusModel>
	{
		private readonly MyServiceBusTcpClient _client;

		public MyServiceBusPublisher(MyServiceBusTcpClient client)
		{
			_client = client;
			_client.CreateTopicIfNotExists(UserProgressUpdatedServiceBusModel.TopicName);
		}

		public ValueTask PublishAsync(UserProgressUpdatedServiceBusModel valueToPublish)
		{
			byte[] bytesToSend = valueToPublish.ServiceBusContractToByteArray();

			Task task = _client.PublishAsync(UserProgressUpdatedServiceBusModel.TopicName, bytesToSend, false);

			return new ValueTask(task);
		}
	}
}