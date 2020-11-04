using System;

namespace Domain.Commands
{
    /// <summary>
    /// Create task input Command
    /// </summary>
    public class CreateTaskCommand
    {
        public string Subject { get; set; }
        public Guid? AssignedMemberId { get; set; }
    }
}
