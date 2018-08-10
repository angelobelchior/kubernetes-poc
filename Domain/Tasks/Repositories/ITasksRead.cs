using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ToDo.Domain.Tasks.Repositories
{
    /// <summary>
    /// Task Repository Read Only
    /// </summary>
    public interface ITasksRead
    {
        /// <summary>
        /// Get Task By Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
         Task<Models.Task> GetById(Guid id, CancellationToken cancellationToken);
         
         /// <summary>
         /// List Tasks by Status
         /// </summary>
         /// <param name="cancellationToken">Cancellation Token</param>
         /// <returns></returns>
         Task<IEnumerable<Models.Task>> List(CancellationToken cancellationToken);
    }
}