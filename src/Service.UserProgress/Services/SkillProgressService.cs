using Service.Core.Domain.Models.Education;
using Service.ServerKeyValue.Grpc;

namespace Service.UserProgress.Services
{
	public class SkillProgressService : DtoRepositoryBase
	{
		public SkillProgressService(IServerKeyValueService serverKeyValueService)
			: base(Program.ReloadedSettings(model => model.KeyUserHabit), serverKeyValueService)
		{
		}

		protected override EducationTaskType[] AllowedTaskTypes => new[] {EducationTaskType.Case, EducationTaskType.TrueFalse};
	}
}