using System;
using Flunt.Notifications;
using MediatR;

namespace ToDo.Domain.Tasks.Commands.Insert
{
    /// <summary>
    /// Insert Request 
    /// </summary>
    public class Request : IRequest<Notifiable>
    {
        /// <summary>
        /// Task Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Task Descripton
        /// </summary>
        /// <value></value>
        public string Description { get; set; }
    }
}