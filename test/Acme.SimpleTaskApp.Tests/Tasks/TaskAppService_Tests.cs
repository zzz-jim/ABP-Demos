using Abp.Runtime.Validation;
using Acme.SimpleTaskApp.Tasks;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Acme.SimpleTaskApp.Tests.Tasks
{
    public class TaskAppService_Tests : SimpleTaskAppTestBase
    {
        private readonly ITaskAppService _taskAppService;

        public TaskAppService_Tests()
        {
            _taskAppService = Resolve<ITaskAppService>();
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Get_AllTasks()
        {
            // Act
            var output = await _taskAppService.GetAllAsync(new SimpleTaskApp.Tasks.Dto.GetAllTasksInput());

            // Assert
            output.Items.Count.ShouldBe(2);

            output.Items.Count(t => t.AssignedPersonName != null).ShouldBe(1);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Get_Filtered_Tasks()
        {
            // Act 
            var output = await _taskAppService.GetAllAsync(new SimpleTaskApp.Tasks.Dto.GetAllTasksInput() { State = TaskState.Open });

            // Assert
            output.Items.ShouldAllBe(x => x.State == TaskState.Open);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Create_New_Task_With_Title()
        {
            // Act
            await _taskAppService.CreateAsync(new SimpleTaskApp.Tasks.Dto.CreateTaskInput
            {
                Title = "Newly created task #1"
            });

            UsingDbContext(context =>
            {
                var task1 = context.Tasks.FirstOrDefault(x => x.Title == "Newly created task #1");
                task1.ShouldNotBeNull();
            });
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Create_New_Task_With_Title_And_Assigned_Person()
        {
            var neo= UsingDbContext(context=>context.Persons.Single(x=>x.Name=="Neo"));

            // Act
            await _taskAppService.CreateAsync(new SimpleTaskApp.Tasks.Dto.CreateTaskInput
            {
                Title = "Newly created task #1",
                AssignedPersonId=neo.Id
            });

            UsingDbContext(context =>
            {
                var task1 = context.Tasks.FirstOrDefault(x => x.Title == "Newly created task #1");
                task1.ShouldNotBeNull();
                task1.AssignedPersonId.ShouldBe(neo.Id);
            });
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Create_New_Task_Without_Title()
        {
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _taskAppService.CreateAsync(new SimpleTaskApp.Tasks.Dto.CreateTaskInput
                {
                    Title = null
                });
            });
        }
    }
}
