using Autofac;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;
using Service.EducationProgress.Domain.Models;
using Service.ServerKeyValue.Client;
using Service.UserKnowledge.Jobs;

namespace Service.UserKnowledge.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterKeyValueClient(Program.Settings.ServerKeyValueServiceUrl);

			MyServiceBusTcpClient serviceBusClient = builder.RegisterMyServiceBusTcpClient(Program.ReloadedSettings(e => e.ServiceBusReader), Program.LogFactory);

			const string queueName = "MyJetEducation-UserKnowledge";
			builder.RegisterMyServiceBusSubscriberBatch<SetProgressInfoServiceBusModel>(serviceBusClient, SetProgressInfoServiceBusModel.TopicName, queueName, TopicQueueType.Permanent);

			builder
				.RegisterType<SetProgressInfoNotificator>()
				.AutoActivate()
				.SingleInstance();
		}
	}
}