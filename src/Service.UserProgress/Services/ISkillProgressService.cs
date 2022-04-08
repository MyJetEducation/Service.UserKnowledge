using System.Threading.Tasks;
using Service.Education.Structure;
using Service.UserProgress.Models;

namespace Service.UserProgress.Services
{
	public interface ISkillProgressService
	{
		ValueTask<SkillProgressDto> GetData(string userId);

		ValueTask SetData(string userId, EducationTutorial tutorial, int unit, int task, int progress);
	}
}