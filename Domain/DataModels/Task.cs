﻿using System;

namespace Domain.DataModels
{
    public class Task
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Subject { get; set; }
        public bool IsComplete { get; set; }
        public Guid? AssignedMemberId { get; set; }
        public virtual Member AssignedMember { get; set; }
    }
}
