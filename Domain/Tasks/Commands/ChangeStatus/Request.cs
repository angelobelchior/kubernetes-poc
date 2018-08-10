using System;
using Flunt.Notifications;
using MediatR;

namespace ToDo.Domain.Tasks.Commands.ChangeStatus
{
    /// <summary>
    /// Change Status Request
    /// </summary>
    public class Request : IRequest<Notifiable>
    {
        /// <summary>
        /// Task Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// New Status
        /// </summary>
        public Models.Status Status { get; set; }
    }
}