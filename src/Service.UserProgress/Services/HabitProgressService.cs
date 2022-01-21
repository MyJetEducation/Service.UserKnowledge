using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Core.Domain.Models.Education;
using Service.ServerKeyValue.Grpc;
using Service.UserProgress.Domain.Models;

namespace Service.UserProgress.Services
{
	public class HabitProgressService : DtoRepositoryBase
	{
		public HabitProgressService(IServerKeyValueService serverKeyValueService, IPublisher<UserProgressUpdatedServiceBusModel> publisher, ILogger<HabitProgressService> logger)
			: base(Program.ReloadedSettings(model => model.KeyUserHabit), serverKeyValueService, publisher, logger)
		{
		}

		protected override EducationTaskType[] AllowedTaskTypes => new[] {EducationTaskType.Test, EducationTaskType.Game};
	}
}