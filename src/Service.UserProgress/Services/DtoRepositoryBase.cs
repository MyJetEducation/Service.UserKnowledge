using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Service.Core.Client.Education;
using Service.Core.Client.Models;
using Service.ServerKeyValue.Grpc;
using Service.ServerKeyValue.Grpc.Models;
using Service.UserProgress.Models;

namespace Service.UserProgress.Services
{
	public abstract class DtoRepositoryBase : IDtoRepository
	{
		private readonly Func<string> _settingsKeyFunc;
		private readonly IServerKeyValueService _serverKeyValueService;
		private readonly ILogger _logger;

		protected DtoRepositoryBase(Func<string> settingsKeyFunc, IServerKeyValueService serverKeyValueService, ILogger logger)
		{
			_settingsKeyFunc = settingsKeyFunc;
			_serverKeyValueService = serverKeyValueService;
			_logger = logger;
		}

		protected abstract EducationTaskType[] AllowedTaskTypes { get; }

		public async ValueTask<ProgressDto> GetData(Guid? userId, EducationTutorial tutorial)
		{
			ProgressDto[] dtos = await GetDataAll(userId);

			ProgressDto progressDto = dtos.FirstOrDefault(dto => dto.Tutorial == tutorial);

			return progressDto ?? new ProgressDto();
		}

		public async ValueTask<ProgressDto[]> GetDataAll(Guid? userId)
		{
			string value = (await _serverKeyValueService.GetSingle(new ItemsGetSingleGrpcRequest
			{
				UserId = userId,
				Key = _settingsKeyFunc.Invoke()
			}))?.Value;

			return value == null
				? Array.Empty<ProgressDto>()
				: JsonSerializer.Deserialize<ProgressDto[]>(value);
		}

		public async ValueTask SetData(Guid? userId, EducationTutorial tutorial, int unit, int task)
		{
			EducationStructureTask structureTask = EducationHelper.GetTask(tutorial, unit, task);
			if (!AllowedTaskTypes.Contains(structureTask.TaskType))
				return;

			List<ProgressDto> dtos = (await GetDataAll(userId)).ToList();

			ProgressDto progressDto = dtos.FirstOrDefault(dto => dto.Tutorial == tutorial);
			if (progressDto == null)
			{
				progressDto = new ProgressDto {Tutorial = tutorial};
				dtos.Add(progressDto);
			}

			progressDto.TaskCount++;
			CountProgress(progressDto);

			CommonGrpcResponse response = await SetData(userId, dtos.ToArray());
			if (!response.IsSuccess)
			{
				_logger.LogError("Error while save user progress for user: {user}, dto: {dto}.", userId, progressDto);
				return;
			}

			await ProgressSaved(userId, dtos);
		}

		protected virtual ValueTask ProgressSaved(Guid? userId, IEnumerable<ProgressDto> progressDtos) => ValueTask.CompletedTask;

		private async ValueTask<CommonGrpcResponse> SetData(Guid? userId, ProgressDto[] dtos) => await _serverKeyValueService.Put(new ItemsPutGrpcRequest
		{
			UserId = userId,
			Items = new[]
			{
				new KeyValueGrpcModel
				{
					Key = _settingsKeyFunc.Invoke(),
					Value = JsonSerializer.Serialize(dtos)
				}
			}
		});

		private void CountProgress(ProgressDto dto)
		{
			int totalCount = EducationStructure.Tutorials[dto.Tutorial].Units
				.SelectMany(pair => pair.Value.Tasks)
				.Count(task => AllowedTaskTypes.Contains(task.Value.TaskType));

			dto.Progress = (int) Math.Round(dto.TaskCount * 100 / (float) totalCount);
		}
	}
}