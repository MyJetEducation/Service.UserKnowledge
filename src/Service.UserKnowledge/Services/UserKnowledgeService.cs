using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Service.Core.Domain.Models.Education;
using Service.Core.Grpc.Models;
using Service.ServerKeyValue.Grpc;
using Service.ServerKeyValue.Grpc.Models;
using Service.UserKnowledge.Domain.Models;
using Service.UserKnowledge.Grpc;
using Service.UserKnowledge.Grpc.Models;
using Service.UserKnowledge.Mappers;

namespace Service.UserKnowledge.Services
{
	public class UserKnowledgeService : IUserKnowledgeService
	{
		private readonly IServerKeyValueService _serverKeyValueService;

		private static readonly string KeyKnowledgeLevel = Program.Settings.KeyKnowledgeLevel;

		public UserKnowledgeService(IServerKeyValueService serverKeyValueService) => _serverKeyValueService = serverKeyValueService;

		public async ValueTask<KnowledgeGrpcResponse> GetKnowledgeAsync(GetKnowledgeGrpcRequset request) => new KnowledgeGrpcResponse
		{
			Knowledge = (await GetKnowledge(request.UserId)).ToGrpcModel()
		};

		public async ValueTask<KnowledgeValueGrpcResponse> GetKnowledgeValueAsync(GetKnowledgeValueGrpcRequset request)
		{
			var result = new KnowledgeValueGrpcResponse();

			KnowledgeDto knowledge = await GetKnowledge(request.UserId);

			int? value = null;

			switch (request.Tutorial)
			{
				case EducationTutorial.PersonalFinance:
					value = knowledge.PersonalFinance;
					break;
				case EducationTutorial.BehavioralFinance:
					value = knowledge.BehavioralFinance;
					break;
				case EducationTutorial.FinancialServices:
					value = knowledge.FinancialServices;
					break;
				case EducationTutorial.FinanceMarkets:
					value = knowledge.FinanceMarkets;
					break;
				case EducationTutorial.HealthAndFinance:
					value = knowledge.HealthAndFinance;
					break;
				case EducationTutorial.PsychologyAndFinance:
					value = knowledge.PsychologyAndFinance;
					break;
				case EducationTutorial.FinanceSecurity:
					value = knowledge.FinanceSecurity;
					break;
				case EducationTutorial.TimeManagement:
					value = knowledge.TimeManagement;
					break;
				case EducationTutorial.Economics:
					value = knowledge.Economics;
					break;
				case EducationTutorial.None:
					break;
			}

			result.Value = value;

			return result;
		}

		public async ValueTask<CommonGrpcResponse> SetKnowledgeAsync(SetKnowledgeGrpcRequset request) => await SetKnowledge(request.UserId, request.Knowledge.ToDto());

		public async ValueTask<CommonGrpcResponse> SetKnowledgeValueAsync(SetKnowledgeValueGrpcRequset request)
		{
			KnowledgeDto knowledge = await GetKnowledge(request.UserId);

			int? value = request.Value;

			switch (request.Tutorial)
			{
				case EducationTutorial.PersonalFinance:
					knowledge.PersonalFinance = value;
					break;
				case EducationTutorial.BehavioralFinance:
					knowledge.BehavioralFinance = value;
					break;
				case EducationTutorial.FinancialServices:
					knowledge.FinancialServices = value;
					break;
				case EducationTutorial.FinanceMarkets:
					knowledge.FinanceMarkets = value;
					break;
				case EducationTutorial.HealthAndFinance:
					knowledge.HealthAndFinance = value;
					break;
				case EducationTutorial.PsychologyAndFinance:
					knowledge.PsychologyAndFinance = value;
					break;
				case EducationTutorial.FinanceSecurity:
					knowledge.FinanceSecurity = value;
					break;
				case EducationTutorial.TimeManagement:
					knowledge.TimeManagement = value;
					break;
				case EducationTutorial.Economics:
					knowledge.Economics = value;
					break;
				case EducationTutorial.None:
				default:
					throw new ArgumentOutOfRangeException();
			}

			return await SetKnowledge(request.UserId, knowledge);
		}

		private async ValueTask<KnowledgeDto> GetKnowledge(Guid? userId)
		{
			ItemsGrpcResponse getResponse = await _serverKeyValueService.Get(new ItemsGetGrpcRequest
			{
				UserId = userId,
				Keys = new[] {KeyKnowledgeLevel}
			});

			string value = getResponse.Items?.FirstOrDefault(model => model.Key == KeyKnowledgeLevel)?.Value;

			return value == null
				? new KnowledgeDto()
				: JsonSerializer.Deserialize<KnowledgeDto>(value);
		}

		private async Task<CommonGrpcResponse> SetKnowledge(Guid? userId, KnowledgeDto knowledgeDto) => await _serverKeyValueService.Put(new ItemsPutGrpcRequest
		{
			UserId = userId,
			Items = new[]
			{
				new KeyValueGrpcModel
				{
					Key = KeyKnowledgeLevel,
					Value = JsonSerializer.Serialize(knowledgeDto)
				}
			}
		});
	}
}