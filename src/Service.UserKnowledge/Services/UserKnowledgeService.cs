using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Service.Core.Domain.Models.Constants;
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
			Knowledge = (await GetKnowledge(request.UserId))?.ToGrpcModel()
		};

		public async ValueTask<KnowledgeValueGrpcResponse> GetKnowledgeValueAsync(GetKnowledgeValueGrpcRequset request)
		{
			var result = new KnowledgeValueGrpcResponse();

			KnowledgeDto? knowledgeDto = await GetKnowledge(request.UserId);
			if (knowledgeDto == null)
				return result;

			KnowledgeDto knowledge = knowledgeDto.Value;
			int? value = null;

			switch (request.Tutorial)
			{
				case Tutorial.PersonalFinance:
					value = knowledge.PersonalFinance;
					break;
				case Tutorial.BehavioralFinance:
					value = knowledge.BehavioralFinance;
					break;
				case Tutorial.FinancialServices:
					value = knowledge.FinancialServices;
					break;
				case Tutorial.FinanceMarkets:
					value = knowledge.FinanceMarkets;
					break;
				case Tutorial.HealthAndFinance:
					value = knowledge.HealthAndFinance;
					break;
				case Tutorial.PsychologyAndFinance:
					value = knowledge.PsychologyAndFinance;
					break;
				case Tutorial.FinanceSecurity:
					value = knowledge.FinanceSecurity;
					break;
				case Tutorial.TimeManagement:
					value = knowledge.TimeManagement;
					break;
				case Tutorial.Economics:
					value = knowledge.Economics;
					break;
				case Tutorial.None:
					break;
			}

			result.Value = value;

			return result;
		}

		public async ValueTask<CommonGrpcResponse> SetKnowledgeAsync(SetKnowledgeGrpcRequset request) => await SetKnowledge(request.UserId, request.Knowledge.ToDto());

		public async ValueTask<CommonGrpcResponse> SetKnowledgeValueAsync(SetKnowledgeValueGrpcRequset request)
		{
			KnowledgeDto? knowledgeDto = await GetKnowledge(request.UserId);
			if (knowledgeDto == null)
				return CommonGrpcResponse.Fail;

			KnowledgeDto knowledge = knowledgeDto.Value;
			int? value = request.Value;

			switch (request.Tutorial)
			{
				case Tutorial.PersonalFinance:
					knowledge.PersonalFinance = value;
					break;
				case Tutorial.BehavioralFinance:
					knowledge.BehavioralFinance = value;
					break;
				case Tutorial.FinancialServices:
					knowledge.FinancialServices = value;
					break;
				case Tutorial.FinanceMarkets:
					knowledge.FinanceMarkets = value;
					break;
				case Tutorial.HealthAndFinance:
					knowledge.HealthAndFinance = value;
					break;
				case Tutorial.PsychologyAndFinance:
					knowledge.PsychologyAndFinance = value;
					break;
				case Tutorial.FinanceSecurity:
					knowledge.FinanceSecurity = value;
					break;
				case Tutorial.TimeManagement:
					knowledge.TimeManagement = value;
					break;
				case Tutorial.Economics:
					knowledge.Economics = value;
					break;
				case Tutorial.None:
				default:
					throw new ArgumentOutOfRangeException();
			}

			return await SetKnowledge(request.UserId, knowledge);
		}

		private async ValueTask<KnowledgeDto?> GetKnowledge(Guid? userId)
		{
			ItemsGrpcResponse getResponse = await _serverKeyValueService.Get(new ItemsGetGrpcRequest
			{
				UserId = userId,
				Keys = new[] {KeyKnowledgeLevel}
			});

			string value = getResponse.Items?.FirstOrDefault(model => model.Key == KeyKnowledgeLevel)?.Value;
			
			return value != null 
				? JsonSerializer.Deserialize<KnowledgeDto>(value) 
				: await ValueTask.FromResult<KnowledgeDto?>(null);
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