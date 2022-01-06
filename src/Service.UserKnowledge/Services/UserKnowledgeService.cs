using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Service.Core.Domain.Models.Education;
using Service.Core.Grpc.Models;
using Service.ServerKeyValue.Grpc;
using Service.ServerKeyValue.Grpc.Models;
using Service.UserKnowledge.Domain.Models;
using Service.UserKnowledge.Grpc;
using Service.UserKnowledge.Grpc.Models;

namespace Service.UserKnowledge.Services
{
	public class UserKnowledgeService : IUserKnowledgeService
	{
		private static readonly EducationTaskType[] AllowedTaskTypes =
		{
			EducationTaskType.Text,
			EducationTaskType.Video
		};

		private readonly IServerKeyValueService _serverKeyValueService;
		private readonly ILogger<UserKnowledgeService> _logger;

		private static string KeyKnowledgeLevel => Program.ReloadedSettings(model => model.KeyUserKnowledge).Invoke();

		public UserKnowledgeService(IServerKeyValueService serverKeyValueService, ILogger<UserKnowledgeService> logger)
		{
			_serverKeyValueService = serverKeyValueService;
			_logger = logger;
		}

		public async ValueTask<KnowledgeProgressGrpcResponse> GetKnowledgeProgressAsync(GetKnowledgeProgressGrpcRequset request) => new KnowledgeProgressGrpcResponse
		{
			Progress = await CountProgress(request.UserId, request.Tutorial)
		};

		public async ValueTask<KnowledgeAllProgressGrpcResponse> GetAllKnowledgeProgressAsync(GetKnowledgeAllProgressGrpcRequset request)
		{
			Guid? userId = request.UserId;
			var list = new List<KnowledgeTutorialProgressGrpcModel>();

			foreach (KeyValuePair<EducationTutorial, EducationStructureTutorial> pair in EducationStructure.Tutorials)
			{
				EducationTutorial tutorial = pair.Value.Tutorial;

				list.Add(new KnowledgeTutorialProgressGrpcModel
				{
					Tutorial = tutorial,
					Progress = await CountProgress(userId, tutorial)
				});
			}

			return new KnowledgeAllProgressGrpcResponse
			{
				Items = list.ToArray()
			};
		}

		public async ValueTask<CommonGrpcResponse> SetKnowledgeAsync(SetKnowledgeGrpcRequset request)
		{
			EducationStructureTask structureTask = EducationHelper.GetTask(request.Tutorial, request.Unit, request.Task);
			if (structureTask == null)
			{
				_logger.LogError("Not valid parameters for set knowledge request: {request}", request);
				return CommonGrpcResponse.Fail;
			}

			if (!AllowedTaskTypes.Contains(structureTask.TaskType))
				return CommonGrpcResponse.Success;

			IList<KnowledgeDto> dtos = (await GetKnowledge(request.UserId)).ToList();

			EducationTutorial tutorial = request.Tutorial;

			if (dtos.All(dto => dto.Tutorial != tutorial))
				dtos.Add(new KnowledgeDto {Tutorial = tutorial});

			KnowledgeDto knowledge = dtos.First(dto => dto.Tutorial == tutorial);

			knowledge.Value++;

			return await SetKnowledge(request.UserId, dtos.ToArray());
		}

		private async ValueTask<int> CountProgress(Guid? userId, EducationTutorial tutorial)
		{
			KnowledgeDto[] dtos = await GetKnowledge(userId);

			KnowledgeDto knowledge = dtos.FirstOrDefault(dto => dto.Tutorial == tutorial);
			if (knowledge == null)
				return 0;

			int maxValue = GetTotalAllowedTasks(tutorial);

			return (int) Math.Round(knowledge.Value * 100 / (float) maxValue);
		}

		private static int GetTotalAllowedTasks(EducationTutorial tutorial) =>
			EducationStructure.Tutorials[tutorial].Units
				.SelectMany(unit => unit.Value.Tasks
					.Where(task => AllowedTaskTypes.Contains(task.Value.TaskType))
					.Select(task => task.Value.Task))
				.Count();

		private async ValueTask<KnowledgeDto[]> GetKnowledge(Guid? userId)
		{
			string value = (await _serverKeyValueService.GetSingle(new ItemsGetSingleGrpcRequest
			{
				UserId = userId,
				Key = KeyKnowledgeLevel
			}))?.Value;

			return value == null
				? Array.Empty<KnowledgeDto>()
				: JsonSerializer.Deserialize<KnowledgeDto[]>(value);
		}

		private async ValueTask<CommonGrpcResponse> SetKnowledge(Guid? userId, KnowledgeDto[] knowledgeDtos) => await _serverKeyValueService.Put(new ItemsPutGrpcRequest
		{
			UserId = userId,
			Items = new[]
			{
				new KeyValueGrpcModel
				{
					Key = KeyKnowledgeLevel,
					Value = JsonSerializer.Serialize(knowledgeDtos)
				}
			}
		});
	}
}