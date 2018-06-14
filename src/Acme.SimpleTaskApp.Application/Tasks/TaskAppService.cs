using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Acme.SimpleTaskApp.Tasks.Dto;

using Abp.Linq.Extensions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Acme.SimpleTaskApp.Tasks
{
    public class TaskAppService : SimpleTaskAppAppServiceBase, ITaskAppService
    {
        private readonly IRepository<Task> _taskRepository;

        public TaskAppService(IRepository<Task> taskRepositry)
        {
            _taskRepository = taskRepositry;
        }

        public async System.Threading.Tasks.Task CreateAsync(CreateTaskInput input)
        {
            var task = ObjectMapper.Map<Task>(input);
            await _taskRepository.InsertAsync(task);
        }

        public async Task<ListResultDto<TaskListDto>> GetAllAsync(GetAllTasksInput input)
        {
            var tasks = await _taskRepository.GetAll()
                .Include(t => t.AssignedPerson)//And including the Task.AssignedPerson property to the query. Just added the Include line. Thus, GetAll method will return Assigned person information with the tasks. Since we used AutoMapper, new properties will also be copied to DTO automatically.
                .WhereIf(input.State.HasValue, t => t.State == input.State.Value)
                .OrderByDescending(t => t.CreationTime)
                .ToListAsync();

            return new ListResultDto<TaskListDto>(ObjectMapper.Map<List<TaskListDto>>(tasks));

            //var tasks = await _taskRepository
            //   .GetAll()
            //   .WhereIf(input.State.HasValue, t => t.State == input.State.Value)
            //   .OrderByDescending(t => t.CreationTime)
            //   .ToListAsync();

            //return new ListResultDto<TaskListDto>(
            //    ObjectMapper.Map<List<TaskListDto>>(tasks)
            //);
        }
    }
}
