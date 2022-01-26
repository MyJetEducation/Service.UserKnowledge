using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.EducationProgress.Grpc.ServiceBusModels;
using Service.UserProgress.Services;

namespace Service.UserProgress.Jobs
{
	public class SetProgressInfoNotificator
	{
		private readonly ILogger<SetProgressInfoNotificator> _logger;
		private readonly IEnumerable<IDtoRepository> _dtoRepositories;

		public SetProgressInfoNotificator(ILogger<SetProgressInfoNotificator> logger,
			IEnumerable<IDtoRepository> dtoRepositories,
			ISubscriber<IReadOnlyList<SetProgressInfoServiceBusModel>> subscriber)
		{
			_logger = logger;
			_dtoRepositories = dtoRepositories;
			subscriber.Subscribe(HandleEvent);
		}

		private async ValueTask HandleEvent(IReadOnlyList<SetProgressInfoServiceBusModel> events)
		{
			foreach (SetProgressInfoServiceBusModel message in events)
			{
				Guid? userId = message.UserId;
				_logger.LogInformation("SetProgressInfoServiceBusModel handled from service bus: {user}", userId);

				foreach (IDtoRepository repository in _dtoRepositories)
					await repository.SetData(userId, message.Tutorial, message.Unit, message.Task);
			}
		}
	}
}