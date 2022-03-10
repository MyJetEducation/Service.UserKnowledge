using System;
using System.Threading.Tasks;
using Service.Education.Structure;
using Service.UserProgress.Models;

namespace Service.UserProgress.Services
{
	public interface IProgressDtoRepository
	{
		ValueTask<ProgressDto> GetData(Guid? userId);

		ValueTask<ProgressDto[]> GetDataAll(Guid? userId);

		ValueTask SetData(Guid? userId, EducationTutorial tutorial, int unit, int task, int progress);
	}
}