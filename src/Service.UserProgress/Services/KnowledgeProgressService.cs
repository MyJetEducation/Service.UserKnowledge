using Microsoft.Extensions.Logging;
using Service.Education.Structure;
using Service.Grpc;
using Service.ServerKeyValue.Grpc;

namespace Service.UserProgress.Services
{
	public class KnowledgeProgressService : DtoRepositoryBase
	{
		public KnowledgeProgressService(IGrpcServiceProxy<IServerKeyValueService> serverKeyValueService, ILogger<KnowledgeProgressService> logger)
			: base(Program.ReloadedSettings(model => model.KeyUserKnowledge), serverKeyValueService, logger)
		{
		}

		protected override EducationTaskType[] AllowedTaskTypes => new[] {EducationTaskType.Text, EducationTaskType.Video};
	}
}