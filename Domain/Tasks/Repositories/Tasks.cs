using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ToDo.Domain.Tasks.Models;

namespace ToDo.Domain.Tasks.Repositories
{
    /// <summary>
    /// Task Repository
    /// </summary>
    public class Tasks : ITasksWrite, ITasksRead
    {
        private readonly IMongoCollection<Models.Task> _collection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public Tasks(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("TasksDB");
            var client = new MongoClient(connectionString);

            var database = client.GetDatabase("TasksDB");
            this._collection = database.GetCollection<Models.Task>(nameof(Models.Task));
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="task">Task</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        public async Task<Models.Task> Insert(Models.Task task, CancellationToken cancellationToken)
        {
            await this._collection.InsertOneAsync(task, cancellationToken: cancellationToken);
            return task;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="task">Task</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        public async Task<Models.Task> Update(Models.Task task, CancellationToken cancellationToken)
        {
            var update = Builders<Models.Task>
                    .Update
                    .Set(t => t.Status, task.Status)
                    .Set(t => t.Title, task.Title)
                    .Set(t => t.Description, task.Description)
                    .Set(t => t.CreatedAt, task.CreatedAt)
                    .Set(t => t.UpdatedAt, task.UpdatedAt);

            await this._collection.UpdateOneAsync(o => o.Id == task.Id, update, cancellationToken: cancellationToken);
            return task;
        }

        /// <summary>
        /// Get Task By Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        public async Task<Models.Task> GetById(Guid id, CancellationToken cancellationToken)
        {
            var filter = Builders<Models.Task>.Filter.Eq(t => t.Id, id);
            var results = await this._collection.Find(filter).ToListAsync(cancellationToken);
            var task = results.FirstOrDefault();
            if(task != null) return this.CreateTask(task);
            return null;
        }

        /// <summary>
        /// List Tasks by Status
        /// </summary>
        /// <param name="cancellationToken">Cancellation TOken</param>
        /// <returns>Task List</returns>
        public async Task<IEnumerable<Models.Task>> List(CancellationToken cancellationToken)
        {
            var tasks = await this._collection.AsQueryable().ToListAsync();
            return tasks?.Select(t => this.CreateTask(t));
        }

        //Hack....
        private Models.Task CreateTask(Models.Task task)
            => new Models.Task(task.Id, task.Title, task.Description, task.Status, task.CreatedAt, task.UpdatedAt);
    }
}