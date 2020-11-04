using System;

namespace Domain.Commands
{
    /// <summary>
    /// Complete task input command
    /// </summary>
    public class CompleteTaskCommand
    {
        public Guid Id { get; set; }
    }
}
