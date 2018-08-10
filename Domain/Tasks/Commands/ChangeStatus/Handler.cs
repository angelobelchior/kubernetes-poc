using Flunt.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ToDo.Domain.Tasks.Commands.ChangeStatus
{
    /// <summary>
    /// Change Status Handler
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
        /// Handler
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken">Cancelation Token</param>
        /// <returns></returns>
        public async Task<Notifiable> Handle(Request request, CancellationToken cancellationToken)
        {
            var task = await this._repositoryRead.GetById(request.Id, cancellationToken);
            if(task == null) 
                return new Models.Result("Id", "Task not found");

            task.ChangeStatus(request.Status);
            await this._repositoryWrite.Update(task, cancellationToken);
            await this._mediator.Publish(new TaskStatusChangedNotification
            {
                StatusFrom = task.Status,
                StatusTo = request.Status,
            });

            return task;
        }
    }
}