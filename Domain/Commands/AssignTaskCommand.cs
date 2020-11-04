using System;

namespace Domain.Commands
{
    /// <summary>
    /// Assign task input command
    /// </summary>
    public class AssignTaskCommand
    {
        public Guid Id { get; set; }
        public Guid AssignedMemberId { get; set; }
    }
}
