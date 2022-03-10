using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Service.Core.Client.Models;
using Service.Education.Helpers;
using Service.Education.Structure;
using Service.Grpc;
using Service.ServerKeyValue.Grpc;
using Service.ServerKeyValue.Grpc.Models;
using Service.UserProgress.Models;

namespace Service.UserProgress.Services
{
	public class SkillProgressService : ISkillProgressService
	{
		private static readonly Func<string> KeySettings = Program.ReloadedSettings(model => model.KeyUserSkill);

		private readonly IGrpcServiceProxy<IServerKeyValueService> _serverKeyValueService;
		private readonly ILogger<SkillProgressService> _logger;

		public SkillProgressService(IGrpcServiceProxy<IServerKeyValueService> serverKeyValueService, ILogger<SkillProgressService> logger)
		{
			_serverKeyValueService = serverKeyValueService;
			_logger = logger;
		}

		public async ValueTask<SkillProgressDto> GetData(Guid? userId)
		{
			string value = (await _serverKeyValueService.Service.GetSingle(new ItemsGetSingleGrpcRequest
			{
				UserId = userId,
				Key = KeySettings.Invoke()
			}))?.Value;

			return value == null
				? new SkillProgressDto()
				: JsonSerializer.Deserialize<SkillProgressDto>(value);
		}

		public async ValueTask SetData(Guid? userId, EducationTutorial tutorial, int unit, int task)
		{
			EducationStructureTask structureTask = EducationHelper.GetTask(tutorial, unit, task);
			if (structureTask == null)
			{
				_logger.LogError("Cant get task by params: tutorial: {tutorial}, unit: {unit}, task: {task} for userId: {userId}.", tutorial, unit, task, userId);
				return;
			}

			SkillProgressDto dto = await GetData(userId);

			switch (structureTask.TaskType)
			{
				case EducationTaskType.Text:
					dto.ConcentrationCount++;
					break;
				case EducationTaskType.Video:
					dto.PerseveranceCount++;
					break;
				case EducationTaskType.Case:
					dto.ThoughtfulnessCount++;
					break;
				case EducationTaskType.Test:
					dto.MemoryCount++;
					break;
				case EducationTaskType.TrueFalse:
					dto.AdaptabilityCount++;
					break;
				case EducationTaskType.Game:
					dto.ActivityCount++;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			CommonGrpcResponse response = await SetData(userId, dto);
			if (!response.IsSuccess)
				_logger.LogError("Error while save skill user progress for user: {user}, dto: {dto}.", userId, dto);
		}

		private async ValueTask<CommonGrpcResponse> SetData(Guid? userId, SkillProgressDto dto) => await _serverKeyValueService.TryCall(service => service.Put(new ItemsPutGrpcRequest
		{
			UserId = userId,
			Items = new[]
			{
				new KeyValueGrpcModel
				{
					Key = KeySettings.Invoke(),
					Value = JsonSerializer.Serialize(dto)
				}
			}
		}));
	}
}