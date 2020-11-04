using AutoMapper;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Domain.Commands;
using Domain.Queries;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        /// <summary>
        /// Method to get all task.
        /// </summary>
        /// <returns></returns>
        public async Task<GetAllTasksQueryResult> GetAllTasksQueryHandler()
        {
            var tasks = await _taskRepository.Reset().GetAllTasksWithMemberAsync();

            var taskViewModelList = tasks.Any()
                ? _mapper.Map<List<TaskViewModel>>(tasks)
                : new List<TaskViewModel>();

            return new GetAllTasksQueryResult()
            {
                Payload = taskViewModelList
            };
        }
        /// <summary>
        /// Method to create task
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            var task = _mapper.Map<Domain.DataModels.Task>(command);
            var persistedTask = await _taskRepository.CreateRecordAsync(task);

            TaskViewModel taskViewModel = _mapper.Map<TaskViewModel>(persistedTask);

            return new CreateTaskCommandResult
            {
                Payload = taskViewModel
            };
        }
        /// <summary>
        /// Method to Complete Task
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<CompleteTaskCommandResult> CompleteTaskCommandHandler(CompleteTaskCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            var task = await _taskRepository.ByIdAsync(command.Id);

            if (task.IsComplete)
            {
                return new CompleteTaskCommandResult()
                {
                    Succeed = true
                };
            }

            task.IsComplete = true;
            var affectedRecordsCount = await _taskRepository.UpdateRecordAsync(task);
            var succeed = affectedRecordsCount == 1;
            return new CompleteTaskCommandResult
            {
                Succeed = succeed
            };
        }
        /// <summary>
        /// Method to assign task
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<AssignTaskCommandResult> AssignTaskCommandHandler(AssignTaskCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            var task = await _taskRepository.ByIdAsync(command.Id);

            _mapper.Map(command, task);

            var affectedRecordsCount = await _taskRepository.UpdateRecordAsync(task);

            var succeed = affectedRecordsCount == 1;

            return new AssignTaskCommandResult
            {
                Succeed = succeed
            };
        }
    }
}
