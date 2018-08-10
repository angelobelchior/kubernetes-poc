using Flunt.Notifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ToDo.Domain.Tasks.Commands.Update
{
    /// <summary>
    /// Update Handler 
    /// </summary>
    public class Handler : IRequestHandler<Request, Notifiable>
    {
        private readonly IMediator _mediator;
        private readonly Repositories.ITasksWrite _repositoryWrite;
        private readonly Repositories.ITasksRead _repositoryRead;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">Mediator</param>
        /// <param name="repositoryWrite">Repository Write</param>
        /// <param name="repositoryRead">Repository Read</param>
        public Handler (IMediator mediator, Repositories.ITasksWrite repositoryWrite, Repositories.ITasksRead repositoryRead) 
        {
            this._mediator = mediator;
            this._repositoryWrite = repositoryWrite;
            this._repositoryRead = repositoryRead;
        }

        /// <summary>
        /// Handle
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        public async Task<Notifiable> Handle(Request request, CancellationToken cancellationToken)
        {
            var task = await this._repositoryRead.GetById(request.Id, cancellationToken);
            if(task == null) 
                return new Models.Result("Id", "Task not found");

            var notification = new TaskUpdatedNotification
            {
                TitleFrom = task.Title,
                TitleTo = request.Title,
                DescriptionFrom = task.Description, 
                DescriptionTo = request.Description, 
            };

            task.ChangeTitle(request.Title);
            task.ChangeDescription(request.Description);
            task.Validate();
            if(task.Valid)
            {
                await this._repositoryWrite.Update(task, cancellationToken);
                await this._mediator.Publish(notification);
            }

            return task;
        }
    }
}