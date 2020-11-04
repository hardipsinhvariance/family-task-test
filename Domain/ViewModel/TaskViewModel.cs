using System;

namespace Domain.ViewModel
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public MemberVm Member { get; set; }
        public bool IsComplete { get; set; }
    }
}
