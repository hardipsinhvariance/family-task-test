using Domain.Commands;
using Domain.Queries;
using System.Threading.Tasks;

namespace Core.Abstractions.Services
{
    public interface ITaskService
    {
        /// <summary>
        /// Method to get all task.
        /// </summary>
        /// <returns></returns>
        Task<GetAllTasksQueryResult> GetAllTasksQueryHandler();
        /// <summary>
        /// Method to create task
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command);
        /// <summary>
        /// Method to Complete Task
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<CompleteTaskCommandResult> CompleteTaskCommandHandler(CompleteTaskCommand command);
        /// <summary>
        /// Method to assign task
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<AssignTaskCommandResult> AssignTaskCommandHandler(AssignTaskCommand command);
    }
}
