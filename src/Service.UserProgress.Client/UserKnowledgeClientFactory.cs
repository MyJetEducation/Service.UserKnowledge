using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;
using Service.UserProgress.Grpc;

namespace Service.UserProgress.Client
{
    [UsedImplicitly]
    public class UserProgressClientFactory : MyGrpcClientFactory
    {
        public UserProgressClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IUserProgressService GetUserProgressService() => CreateGrpcService<IUserProgressService>();
    }
}
