using System;

namespace ToDo.Domain.Tasks.Commands
{
    /// <summary>
    /// Notification Base
    /// </summary>
    public abstract class NotificationBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Date Time
        /// </summary>
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}