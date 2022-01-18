using Autofac;
using Service.UserProgress.Grpc;

// ReSharper disable UnusedMember.Global

namespace Service.UserProgress.Client
{
    public static class AutofacHelper
    {
        public static void RegisterUserProgressClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new UserProgressClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetUserProgressService()).As<IUserProgressService>().SingleInstance();
        }
    }
}
