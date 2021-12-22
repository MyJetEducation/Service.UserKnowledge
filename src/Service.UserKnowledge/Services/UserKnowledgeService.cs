using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Service.Core.Domain.Extensions;
using Service.Core.Domain.Models.Constants;
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
		private readonly IServerKeyValueService _serverKeyValueService;

		private static readonly string KeyKnowledgeLevel = Program.Settings.KeyKnowledgeLevel;

		public UserKnowledgeService(IServerKeyValueService serverKeyValueService) => _serverKeyValueService = serverKeyValueService;

		public async ValueTask<KnowledgeLevelGrpcResponse> GetKnowledgeLevelAsync(GetKnowledgeLevelGrpcRequset request)
		{
			var result = new KnowledgeLevelGrpcResponse();

			ItemsGrpcResponse getResponse = await _serverKeyValueService.Get(new ItemsGetGrpcRequest
			{
				UserId = request.UserId,
				Keys = new[] {KeyKnowledgeLevel}
			});

			string level = getResponse.Items?.FirstOrDefault(model => model.Key == KeyKnowledgeLevel)?.Value;
			if (!level.IsNullOrWhiteSpace() && int.TryParse(level, out int intLevel))
				result.Level = (Tutorial) intLevel;

			return result;
		}

		public async ValueTask<CommonGrpcResponse> SetKnowledgeLevelAsync(SetKnowledgeLevelGrpcRequset request)
		{
			var putRequest = new ItemsPutGrpcRequest
			{
				UserId = request.UserId,
				Items = new[]
				{
					new KeyValueGrpcModel
					{
						Key = KeyKnowledgeLevel,
						Value = JsonSerializer.Serialize(new KnowledgeLevelDto(request.Level))
					}
				}
			};

			return await _serverKeyValueService.Put(putRequest);
		}
	}
}