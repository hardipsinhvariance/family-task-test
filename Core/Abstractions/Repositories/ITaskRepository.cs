﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Abstractions.Repositories
{
    public interface ITaskRepository: IBaseRepository<Guid, Domain.DataModels.Task, ITaskRepository>
    {
        /// <summary>
        /// Returns list of <see cref="Domain.DataModels.Task"/> with all <see cref="Domain.DataModels.Member"/> based on the current attributes.
        /// </summary>
        /// <returns><see cref="Domain.DataModels.Task"/></returns>
        Task<IEnumerable<Domain.DataModels.Task>> GetAllTasksWithMemberAsync(CancellationToken cancellationToken = default);
    }
}
