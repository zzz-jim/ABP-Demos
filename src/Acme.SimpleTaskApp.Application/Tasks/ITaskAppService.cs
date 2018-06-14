using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Acme.SimpleTaskApp.Tasks.Dto;
using System.Threading.Tasks;

namespace Acme.SimpleTaskApp.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        Task<ListResultDto<TaskListDto>> GetAllAsync(GetAllTasksInput input);

        System.Threading.Tasks.Task CreateAsync(CreateTaskInput input);
    }
}
