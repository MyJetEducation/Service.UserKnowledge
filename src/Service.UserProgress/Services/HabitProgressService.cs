using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.ServiceBus;
using Service.Core.Client.Education;
using Service.ServerKeyValue.Grpc;
using Service.ServiceBus.Models;
using Service.UserProgress.Models;

namespace Service.UserProgress.Services
{
	public class HabitProgressService : DtoRepositoryBase
	{
		private readonly IServiceBusPublisher<UserProgressUpdatedServiceBusModel> _publisher;

		public HabitProgressService(IServerKeyValueService serverKeyValueService, IServiceBusPublisher<UserProgressUpdatedServiceBusModel> publisher, ILogger<HabitProgressService> logger)
			: base(Program.ReloadedSettings(model => model.KeyUserHabit), serverKeyValueService, logger) =>
				_publisher = publisher;

		protected override EducationTaskType[] AllowedTaskTypes => new[] {EducationTaskType.Test, EducationTaskType.Game};

		//Publish for UserReward serice
		protected override async ValueTask ProgressSaved(Guid? userId, IEnumerable<ProgressDto> progressDtos) => await _publisher.PublishAsync(new UserProgressUpdatedServiceBusModel
		{
			UserId = userId,
			HabitCount = progressDtos.Count()
		});
	}
}