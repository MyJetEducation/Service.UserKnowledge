using Service.Core.Domain.Models.Education;
using Service.ServerKeyValue.Grpc;

namespace Service.UserProgress.Services
{
	public class HabitProgressService : DtoRepositoryBase
	{
		public HabitProgressService(IServerKeyValueService serverKeyValueService)
			: base(Program.ReloadedSettings(model => model.KeyUserHabit), serverKeyValueService)
		{
		}

		protected override EducationTaskType[] AllowedTaskTypes => new[] {EducationTaskType.Test, EducationTaskType.Game};
	}
}