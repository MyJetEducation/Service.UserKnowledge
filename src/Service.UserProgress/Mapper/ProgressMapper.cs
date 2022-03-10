using System.Linq;
using Service.UserProgress.Grpc.Models;
using Service.UserProgress.Models;

namespace Service.UserProgress.Mapper
{
	public static class ProgressMapper
	{
		public static ProgressGrpcResponse ToGrpcModel(this ProgressDto dto) => new ProgressGrpcResponse
		{
			Progress = dto.Progress,
			Index = (int) dto.Tutorial,
			Count = dto.TaskProgress.Count
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