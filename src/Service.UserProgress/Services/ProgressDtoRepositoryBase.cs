using System;
using System.Collections.Generic;
using System.Linq;
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
	public abstract class ProgressDtoRepositoryBase : IProgressDtoRepository
	{
		private readonly Func<string> _settingsKeyFunc;
		private readonly IGrpcServiceProxy<IServerKeyValueService> _serverKeyValueService;
		private readonly ILogger _logger;

		protected ProgressDtoRepositoryBase(Func<string> settingsKeyFunc, IGrpcServiceProxy<IServerKeyValueService> serverKeyValueService, ILogger logger)
		{
			_settingsKeyFunc = settingsKeyFunc;
			_serverKeyValueService = serverKeyValueService;
			_logger = logger;
		}

		protected abstract EducationTaskType[] AllowedTaskTypes { get; }

		public async ValueTask<ProgressDto> GetData(string userId)
		{
			ProgressDto[] dtos = await GetDataAll(userId);

			ProgressDto progressDto = dtos
				.OrderByDescending(dto => dto.Tutorial)
				.FirstOrDefault();

			return progressDto ?? new ProgressDto(EducationTutorial.PersonalFinance);
		}

		public async ValueTask<ProgressDto[]> GetDataAll(string userId)
		{
			string value = (await _serverKeyValueService.Service.GetSingle(new ItemsGetSingleGrpcRequest
			{
				UserId = userId,
				Key = _settingsKeyFunc.Invoke()
			}))?.Value;

			return value == null
				? Array.Empty<ProgressDto>()
				: JsonSerializer.Deserialize<ProgressDto[]>(value);
		}

		public async ValueTask SetData(string userId, EducationTutorial tutorial, int unit, int task, int progress)
		{
			EducationStructureTask structureTask = EducationHelper.GetTask(tutorial, unit, task);
			if (!AllowedTaskTypes.Contains(structureTask.TaskType))
				return;

			List<ProgressDto> dtos = (await GetDataAll(userId)).ToList();

			ProgressDto progressDto = dtos.FirstOrDefault(dto => dto.Tutorial == tutorial);
			if (progressDto == null)
			{
				progressDto = new ProgressDto(tutorial);
				dtos.Add(progressDto);
			}

			progressDto.TaskProgress.Add(progress);
			CountProgress(progressDto);

			CommonGrpcResponse response = await SetData(userId, dtos.ToArray());
			if (!response.IsSuccess)
			{
				_logger.LogError("Error while save user progress for user: {user}, dto: {dto}.", userId, progressDto);
				return;
			}

			await ProgressSaved(userId, dtos);
		}

		protected virtual ValueTask ProgressSaved(string userId, IEnumerable<ProgressDto> progressDtos) => ValueTask.CompletedTask;

		private async ValueTask<CommonGrpcResponse> SetData(string userId, ProgressDto[] dtos) => await _serverKeyValueService.TryCall(service => service.Put(new ItemsPutGrpcRequest
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
		}));

		private void CountProgress(ProgressDto dto)
		{
			int totalCount = EducationStructure.Tutorials[dto.Tutorial].Units
				.SelectMany(pair => pair.Value.Tasks)
				.Count(task => AllowedTaskTypes.Contains(task.Value.TaskType));

			dto.Progress = dto.TaskProgress.Sum() / totalCount;
		}
	}
}