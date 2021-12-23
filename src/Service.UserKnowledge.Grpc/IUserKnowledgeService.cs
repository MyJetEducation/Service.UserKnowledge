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
		ValueTask<KnowledgeGrpcResponse> GetKnowledgeAsync(GetKnowledgeGrpcRequset request);

		[OperationContract]
		ValueTask<CommonGrpcResponse> SetKnowledgeAsync(SetKnowledgeGrpcRequset request);

		[OperationContract]
		ValueTask<CommonGrpcResponse> SetKnowledgeValueAsync(SetKnowledgeValueGrpcRequset request);
	}
}