using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;
using Service.UserKnowledge.Grpc;

namespace Service.UserKnowledge.Client
{
    [UsedImplicitly]
    public class UserKnowledgeClientFactory : MyGrpcClientFactory
    {
        public UserKnowledgeClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IUserKnowledgeService GetUserKnowledgeService() => CreateGrpcService<IUserKnowledgeService>();
    }
}
