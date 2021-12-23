using System;
using System.Runtime.Serialization;

namespace Service.UserKnowledge.Grpc.Models
{
    [DataContract]
    public class GetKnowledgeGrpcRequset
    {
        [DataMember(Order = 1)]
        public Guid? UserId { get; set; }
    }
}
