using System;
using System.Threading;
using System.Threading.Tasks;
using Flunt.Notifications;
using MediatR;

namespace ToDo.Domain.Tasks.Commands.Insert 
{
    /// <summary>
    /// Insert Handler
    /// </summary>
    public class Handler : IRequestHandler<Request, Notifiable> 
    {
        private readonly IMediator _mediator;
        private readonly Repositories.ITasksWrite _repositoryWrite;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">Mediator</param>
        /// <param name="repositoryWrite">Repository Write</param>
        public Handler (IMediator mediator, Repositories.ITasksWrite repositoryWrite) 
        {
            this._mediator = mediator;
            this._repositoryWrite = repositoryWrite;
        }

        /// <summary>
        /// Handle
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken">Cancelation Token</param>
        /// <returns>Notification</returns>
        public async Task<Notifiable> Handle (Request request, CancellationToken cancellationToken) 
        {
            var task = new Models.Task(request.Title, request.Description);
            task.Validate();
            if(task.Valid)
            {
                await this._repositoryWrite.Insert(task, cancellationToken);
                await this._mediator.Publish(new TaskInsertedNotification
                {
                    Title = task.Title
                });
            }

            return task;
        }
    }
}