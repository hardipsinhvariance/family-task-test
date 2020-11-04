using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using WebClient.Abstractions;
using Microsoft.AspNetCore.Components;
using Core.Extensions.ModelConversion;
using Domain.Queries;
using Domain.ViewModel;
using Domain.Commands;
using Domain.DataModels;
using Task = System.Threading.Tasks.Task;

namespace WebClient.Services
{
    public class TaskDataService: ITaskDataService
    {
        private readonly HttpClient _httpClient;

        public TaskDataService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("FamilyTaskAPI");
            Tasks = new List<TaskViewModel>();
            LoadTasks();
        }

        public IEnumerable<TaskViewModel> Tasks { get; private set; }
        public TaskViewModel SelectedTask { get; private set; }
        public TaskViewModel DragedTask { get; private set; }


        public event EventHandler TaskSelected;
        public event EventHandler<string> CreateTaskFailed;
        public event EventHandler TasksUpdated;
        public event EventHandler<string> CompleteTaskFailed;


        public void SelectTask(Guid id)
        {
            SelectedTask = Tasks.SingleOrDefault(t => t.Id == id);
            TasksUpdated?.Invoke(this, null);
        }

        public void SelectDragedTask(Guid id)
        {
            DragedTask = Tasks.SingleOrDefault(t => t.Id == id);
        }

        private async Task<GetAllTasksQueryResult> GetAllTasks()
        {
            return await _httpClient.GetJsonAsync<GetAllTasksQueryResult>("tasks");
        }

        private async void LoadTasks()
        {
            var updatedList = (await GetAllTasks()).Payload;
            if (updatedList != null)
            {
                Tasks = updatedList;
                TasksUpdated?.Invoke(this, null);
                return;
            }
            CreateTaskFailed?.Invoke(this, "We didn't get the list of the tasks.");
        }
        public async Task CreateTask(TaskViewModel model)
        {
            var result = await Create(model.ToCreateTaskCommand());
            if (result != null)
            {
                LoadTasks();
            }
            else
            {
                CreateTaskFailed?.Invoke(this, "Create task unsuccessful.");
            }
        }

        public async Task AssignTask(TaskViewModel model)
        {
            var result = await Assign(model.ToAssignTaskCommand());
            if (result != null && result.Succeed)
            {
                LoadTasks();
            }
            else
            {
                CreateTaskFailed?.Invoke(this, "Update task unsuccessful.");
            }
        }

        public async Task ToggleTask(Guid id)
        {
            var taskViewModel = Tasks.Where(t => t.Id == id).FirstOrDefault();
            if (taskViewModel == null)
                throw new ArgumentNullException("Task was not chosen.");
            var result = await Complete(taskViewModel.ToCompleteTaskCommand());
            if (result != null && result.Succeed)
            {
                taskViewModel.IsComplete = true;
                TasksUpdated?.Invoke(this, null);
            }
            else
            {
                CompleteTaskFailed.Invoke(this, "Unable to complete task.");
            }
        }

        private async Task<CreateTaskCommandResult> Create(CreateTaskCommand command)
        {
            return await _httpClient.PostJsonAsync<CreateTaskCommandResult>("tasks", command);
        }

        private async Task<CompleteTaskCommandResult> Complete(CompleteTaskCommand command)
        {
            return await _httpClient.PutJsonAsync<CompleteTaskCommandResult>("tasks/complete", command);
        }

        private async Task<AssignTaskCommandResult> Assign(AssignTaskCommand command)
        {
            return await _httpClient.PutJsonAsync<AssignTaskCommandResult>("tasks/assign", command);
        }


    }
}