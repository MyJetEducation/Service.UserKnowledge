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
		ValueTask<KnowledgeProgressGrpcResponse> GetKnowledgeProgressAsync(GetKnowledgeProgressGrpcRequset request);

		[OperationContract]
		ValueTask<KnowledgeAllProgressGrpcResponse> GetAllKnowledgeProgressAsync(GetKnowledgeAllProgressGrpcRequset request);

		[OperationContract]
		ValueTask<CommonGrpcResponse> SetKnowledgeAsync(SetKnowledgeGrpcRequset request);
	}
}