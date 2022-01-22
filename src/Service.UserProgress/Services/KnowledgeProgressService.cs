﻿using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Core.Domain.Models.Education;
using Service.ServerKeyValue.Grpc;
using Service.UserProgress.Domain.Models;

namespace Service.UserProgress.Services
{
	public class KnowledgeProgressService : DtoRepositoryBase
	{
		public KnowledgeProgressService(IServerKeyValueService serverKeyValueService, IPublisher<UserProgressUpdatedServiceBusModel> publisher, ILogger<KnowledgeProgressService> logger)
			: base(Program.ReloadedSettings(model => model.KeyUserKnowledge), serverKeyValueService, publisher, logger)
		{
		}

		protected override EducationTaskType[] AllowedTaskTypes => new[] {EducationTaskType.Text, EducationTaskType.Video};
	}
}