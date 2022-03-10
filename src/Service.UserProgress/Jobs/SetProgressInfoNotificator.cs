using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Education.Structure;
using Service.ServiceBus.Models;
using Service.UserProgress.Services;

namespace Service.UserProgress.Jobs
{
	public class SetProgressInfoNotificator
	{
		private readonly ILogger<SetProgressInfoNotificator> _logger;
		private readonly IEnumerable<IProgressDtoRepository> _dtoRepositories;
		private readonly ISkillProgressService _skillProgressService;

		public SetProgressInfoNotificator(ILogger<SetProgressInfoNotificator> logger,
			IEnumerable<IProgressDtoRepository> dtoRepositories,
			ISubscriber<IReadOnlyList<SetProgressInfoServiceBusModel>> subscriber,
			ISkillProgressService dtoSkillRepository)
		{
			_logger = logger;
			_dtoRepositories = dtoRepositories;
			_skillProgressService = dtoSkillRepository;

			subscriber.Subscribe(HandleEvent);
		}

		private async ValueTask HandleEvent(IReadOnlyList<SetProgressInfoServiceBusModel> events)
		{
			foreach (SetProgressInfoServiceBusModel message in events)
			{
				Guid? userId = message.UserId;
				_logger.LogInformation("SetProgressInfoServiceBusModel handled from service bus: {user}", userId);

				EducationTutorial tutorial = message.Tutorial;
				int unit = message.Unit;
				int task = message.Task;

				foreach (IProgressDtoRepository repository in _dtoRepositories)
					await repository.SetData(userId, tutorial, unit, task);

				await _skillProgressService.SetData(userId, tutorial, unit, task);
			}
		}
	}
}