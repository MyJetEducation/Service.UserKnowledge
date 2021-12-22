using Autofac;
using Service.UserKnowledge.Grpc;

// ReSharper disable UnusedMember.Global

namespace Service.UserKnowledge.Client
{
    public static class AutofacHelper
    {
        public static void RegisterUserKnowledgeClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new UserKnowledgeClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetHelloService()).As<IUserKnowledgeService>().SingleInstance();
        }
    }
}
