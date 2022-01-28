using Microsoft.Extensions.Logging;
using Service.Core.Client.Education;
using Service.ServerKeyValue.Grpc;

namespace Service.UserProgress.Services
{
	public class SkillProgressService : DtoRepositoryBase
	{
		public SkillProgressService(IServerKeyValueService serverKeyValueService, ILogger<SkillProgressService> logger)
			: base(Program.ReloadedSettings(model => model.KeyUserHabit), serverKeyValueService, logger)
		{
		}

		protected override EducationTaskType[] AllowedTaskTypes => new[] {EducationTaskType.Case, EducationTaskType.TrueFalse};
	}
}