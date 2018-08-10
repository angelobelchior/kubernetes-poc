using System;
using Flunt.Notifications;
using MediatR;

namespace ToDo.Domain.Tasks.Commands.Update
{
    /// <summary>
    /// Update Request
    /// </summary>
    public class Request : IRequest<Notifiable>
    {
        /// <summary>
        /// Task Id
        /// </summary>
        /// <value></value>
        public Guid Id { get; set; }
        /// <summary>
        /// Task Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Task Description
        /// </summary>
        public string Description { get; set; }
    }
}