using Service.Core.Domain.Models.Education;
using Service.ServerKeyValue.Grpc;

namespace Service.UserProgress.Services
{
	public class KnowledgeProgressService : DtoRepositoryBase
	{
		public KnowledgeProgressService(IServerKeyValueService serverKeyValueService)
			: base(Program.ReloadedSettings(model => model.KeyUserKnowledge), serverKeyValueService)
		{
		}

		protected override EducationTaskType[] AllowedTaskTypes => new[] {EducationTaskType.Text, EducationTaskType.Video};
	}
}