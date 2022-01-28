using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Core.Client.Education;
using Service.ServerKeyValue.Grpc;
using Service.ServiceBus.Models;

namespace Service.UserProgress.Services
{
	public class KnowledgeProgressService : DtoRepositoryBase
	{
		public KnowledgeProgressService(IServerKeyValueService serverKeyValueService, IPublisher<UserProgressUpdatedServiceBusModel> publisher, ILogger<KnowledgeProgressService> logger)
			: base(Program.ReloadedSettings(model => model.KeyUserKnowledge), serverKeyValueService, logger)
		{
		}

		protected override EducationTaskType[] AllowedTaskTypes => new[] {EducationTaskType.Text, EducationTaskType.Video};
	}
}