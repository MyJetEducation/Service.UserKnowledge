using Microsoft.Extensions.Logging;
using Service.Education.Structure;
using Service.Grpc;
using Service.ServerKeyValue.Grpc;

namespace Service.UserProgress.Services
{
	public class SkillProgressService : DtoRepositoryBase
	{
		public SkillProgressService(IGrpcServiceProxy<IServerKeyValueService> serverKeyValueService, ILogger<SkillProgressService> logger)
			: base(Program.ReloadedSettings(model => model.KeyUserSkill), serverKeyValueService, logger)
		{
		}

		protected override EducationTaskType[] AllowedTaskTypes => new[] {EducationTaskType.Case, EducationTaskType.TrueFalse};
	}
}