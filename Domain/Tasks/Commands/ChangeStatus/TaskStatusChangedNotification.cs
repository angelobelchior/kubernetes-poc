using System;
using MediatR;

namespace ToDo.Domain.Tasks.Commands.ChangeStatus
{
    /// <summary>
    /// Task Status Changed Notification
    /// </summary>
    public class TaskStatusChangedNotification : NotificationBase, INotification
    {
        /// <summary>
        /// Task Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Status From
        /// </summary>
        /// <value></value>
        public Models.Status StatusFrom { get; set; }
        /// <summary>
        /// Status To
        /// </summary>
        public Models.Status StatusTo { get; set; }
    }
}