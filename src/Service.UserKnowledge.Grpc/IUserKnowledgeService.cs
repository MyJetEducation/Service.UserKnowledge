using System.ServiceModel;
using System.Threading.Tasks;
using Service.Core.Grpc.Models;
using Service.UserKnowledge.Grpc.Models;

namespace Service.UserKnowledge.Grpc
{
	[ServiceContract]
	public interface IUserKnowledgeService
	{
		[OperationContract]
		ValueTask<KnowledgeLevelGrpcResponse> GetKnowledgeLevelAsync(GetKnowledgeLevelGrpcRequset request);

		[OperationContract]
		ValueTask<CommonGrpcResponse> SetKnowledgeLevelAsync(SetKnowledgeLevelGrpcRequset request);
	}
}