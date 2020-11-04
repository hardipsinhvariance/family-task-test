using Domain.ViewModel;

namespace Domain.Commands
{
    /// <summary>
    /// Create task result
    /// </summary>
    public class CreateTaskCommandResult
    {
        public TaskViewModel Payload { get; set; }
    }
}
