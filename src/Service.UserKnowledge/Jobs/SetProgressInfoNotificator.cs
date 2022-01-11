using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.EducationProgress.Domain.Models;
using Service.UserKnowledge.Grpc;
using Service.UserKnowledge.Grpc.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Service.UserKnowledge.Jobs
{
	public class SetProgressInfoNotificator
	{
		private readonly IUserKnowledgeService _userKnowledgeService;
		private readonly ILogger<SetProgressInfoNotificator> _logger;

		public SetProgressInfoNotificator(ILogger<SetProgressInfoNotificator> logger,
			ISubscriber<IReadOnlyList<SetProgressInfoServiceBusModel>> subscriber, IUserKnowledgeService userKnowledgeService)
		{
			_logger = logger;
			_userKnowledgeService = userKnowledgeService;
			subscriber.Subscribe(HandleEvent);
		}

		private async ValueTask HandleEvent(IReadOnlyList<SetProgressInfoServiceBusModel> events)
		{
			foreach (SetProgressInfoServiceBusModel message in events)
			{
				Guid? user = message.UserId;
				_logger.LogInformation($"SetProgressInfoServiceBusModel handled from service bus: {JsonSerializer.Serialize(message)}");

				await _userKnowledgeService.SetKnowledgeAsync(new SetKnowledgeGrpcRequset
				{
					UserId = user,
					Tutorial = message.Tutorial,
					Unit = message.Unit,
					Task = message.Task
				});
			}
		}
	}
}