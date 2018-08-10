using System.Threading;
using System.Threading.Tasks;

namespace ToDo.Domain.Tasks.Repositories
{
    /// <summary>
    /// Task Repository Write Only
    /// </summary>
    public interface ITasksWrite
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="task">Task</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
         Task<Models.Task> Insert(Models.Task task, CancellationToken cancellationToken);
         
         /// <summary>
         /// Update
         /// </summary>
         /// <param name="task">Task</param>
         /// <param name="cancellationToken">Cancellation Token</param>
         /// <returns>Task</returns>
         Task<Models.Task> Update(Models.Task task, CancellationToken cancellationToken);
    }
}