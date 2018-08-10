using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Flunt.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ToDo.Controllers 
{
    /// <summary>
    /// Tasks API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase 
    {
        private readonly IMediator _mediator;
        private readonly Domain.Tasks.Repositories.ITasksRead _repositoryRead;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">Mediator</param>
        /// <param name="repositoryRead">Repository Read</param>
        public TasksController(IMediator mediator, Domain.Tasks.Repositories.ITasksRead repositoryRead)
        {
            this._mediator = mediator;
            this._repositoryRead = repositoryRead;
        }

        /// <summary>
        /// List all Tasks
        /// </summary>
        /// <returns>Task List</returns>
        [HttpGet]
        public async Task<IEnumerable<object>> Get()
        {
            var tasks = await this._repositoryRead.List(CancellationToken.None);
            return tasks.Select(t => new { t.Id, t.Title, t.Description, t.Status });
        }

        /// <summary>
        /// Get Task by Id
        /// </summary>
        /// <param name="id">Task Id</param>
        /// <returns>Task</returns>
        [HttpGet("{id}")]
        public async Task<object> Get(Guid id)
        {
            var task = await this._repositoryRead.GetById(id, CancellationToken.None);
            return new { task.Id, task.Title, task.Description, task.Status };
        }

        /// <summary>
        /// Post a new Task
        /// </summary>
        /// <param name="insert">New Task</param>
        /// <returns>Notifications</returns>
        [HttpPost]
        public async Task<object> Post([FromBody] Domain.Tasks.Commands.Insert.Request insert)
            => await this.Execute(insert);

        /// <summary>
        /// Update a Task
        /// </summary>
        /// <param name="id">Task Id</param>
        /// <param name="update">Task</param>
        /// <returns>Notifications</returns>
        [HttpPut("{id}")]
        public async Task<object> Put(Guid id, [FromBody] Domain.Tasks.Commands.Update.Request update)
        {
            update.Id = id;
            return await this.Execute(update);
        }

        /// <summary>
        /// Change a Task Status
        /// </summary>
        /// <param name="id">Task Id</param>
        /// <param name="change">New Status</param>
        /// <returns>Notifications</returns>
        [HttpPut("{id}/ChangeStatus")]
        public async Task<object> ChangeStatus(Guid id, [FromBody] Domain.Tasks.Commands.ChangeStatus.Request change)
        {
            change.Id = id;
            return await this.Execute(change);
        }

        private async Task<object> Execute(IRequest<Notifiable> notifiable)
        {
            var result = await this._mediator.Send(notifiable);
            return new { result.Valid, Notifications = result.Notifications?.Select(n => new { n.Property, n.Message }) };
        }
    }
}