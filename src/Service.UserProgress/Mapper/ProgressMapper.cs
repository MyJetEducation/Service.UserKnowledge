using System.Linq;
using Service.UserProgress.Domain.Models;
using Service.UserProgress.Grpc.Models;

namespace Service.UserProgress.Mapper
{
	public static class ProgressMapper
	{
		public static ProgressGrpcResponse ToGrpcModel(this ProgressDto dto) => new ProgressGrpcResponse
		{
			Progress = dto.Progress,
			Index = (int) dto.Tutorial
		};

		public static AllProgressGrpcResponse ToGrpcModel(this ProgressDto[] dtos) => new AllProgressGrpcResponse
		{
			Items = dtos.Select(dto => new TutorialProgressGrpcModel
			{
				Index = (int) dto.Tutorial,
				Progress = dto.Progress
			}).ToArray()
		};
	}
}