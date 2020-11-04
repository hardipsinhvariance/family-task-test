using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.Shared.Models;

namespace WebClient.Abstractions
{
    /// <summary>
    /// This Service is currently using the TaskModel Class, and will need to use a shared view
    /// model after the model has been created.  For the moment, this pattern facilitates a client
    /// side storage mechanism to view functionality.  See work completed for the MemberDataService
    /// for an example of expectations.
    /// </summary>
    public interface ITaskDataService
    {
        IEnumerable<TaskViewModel> Tasks { get; }
        TaskViewModel SelectedTask { get; }
        TaskViewModel DragedTask { get; }

        event EventHandler TaskSelected;
        event EventHandler<string> CreateTaskFailed;
        event EventHandler TasksUpdated;
        event EventHandler<string> CompleteTaskFailed;

        Task CreateTask(TaskViewModel model);
        Task AssignTask(TaskViewModel model);
        Task ToggleTask(Guid id);

        void SelectTask(Guid id);
        void SelectDragedTask(Guid id);
        
    }
}