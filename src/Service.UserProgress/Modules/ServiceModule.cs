using System.Collections.Generic;
using Autofac;
using DotNetCoreDecorators;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;
using Service.ServerKeyValue.Client;
using Service.ServiceBus.Models;
using Service.UserProgress.Jobs;
using Service.UserProgress.Services;
using SetProgressInfoServiceBusModel = Service.EducationProgress.Grpc.ServiceBusModels.SetProgressInfoServiceBusModel;

namespace Service.UserProgress.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterKeyValueClient(Program.Settings.ServerKeyValueServiceUrl);

			builder.RegisterType<UserProgressService>().AsImplementedInterfaces().SingleInstance();

			MyServiceBusTcpClient serviceBusClient = builder.RegisterMyServiceBusTcpClient(Program.ReloadedSettings(e => e.ServiceBusReader), Program.LogFactory);

			const string queueName = "MyJetEducation-UserProgress";
			builder.RegisterMyServiceBusSubscriberBatch<SetProgressInfoServiceBusModel>(serviceBusClient, SetProgressInfoServiceBusModel.TopicName, queueName, TopicQueueType.Permanent);

			builder.RegisterType<SetProgressInfoNotificator>().AutoActivate().SingleInstance();

			builder.RegisterType<KnowledgeProgressService>().AsSelf().SingleInstance();
			builder.RegisterType<HabitProgressService>().AsSelf().SingleInstance();
			builder.RegisterType<SkillProgressService>().AsSelf().SingleInstance();

			builder
				.Register(c => new List<IDtoRepository>
				{
					c.Resolve<KnowledgeProgressService>(),
					c.Resolve<HabitProgressService>(),
					c.Resolve<SkillProgressService>()
				})
				.As<IEnumerable<IDtoRepository>>();

			var tcpServiceBus = new MyServiceBusTcpClient(() => Program.Settings.ServiceBusWriter, "MyJetEducation Service.UserProgress");
			IPublisher<UserProgressUpdatedServiceBusModel> clientRegisterPublisher = new MyServiceBusPublisher(tcpServiceBus);
			builder.Register(context => clientRegisterPublisher);
			tcpServiceBus.Start();
		}
	}
}