using System;
using System.Threading.Tasks;
using Service.Core.Client.Education;
using Service.UserProgress.Models;

namespace Service.UserProgress.Services
{
	public interface IDtoRepository
	{
		ValueTask<ProgressDto> GetData(Guid? userId);

		ValueTask<ProgressDto[]> GetDataAll(Guid? userId);

		ValueTask SetData(Guid? userId, EducationTutorial tutorial, int unit, int task);
	}
}