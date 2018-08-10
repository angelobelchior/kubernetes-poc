using MediatR;

namespace ToDo.Domain.Tasks.Commands.Insert
{
    /// <summary>
    /// Task Inserted Notification
    /// </summary>
    public class TaskInsertedNotification : NotificationBase, INotification
    {
        /// <summary>
        /// Task Title
        /// </summary>
        public string Title { get; set; }
    }
}