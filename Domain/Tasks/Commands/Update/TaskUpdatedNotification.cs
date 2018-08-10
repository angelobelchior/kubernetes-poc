using MediatR;

namespace ToDo.Domain.Tasks.Commands.Update
{
    /// <summary>
    /// Task Updated Notification
    /// </summary>
    public class TaskUpdatedNotification : NotificationBase, INotification
    {
        /// <summary>
        /// Title From
        /// </summary>
        /// <value></value>
        public string TitleFrom { get; set; }
        /// <summary>
        /// Title To
        /// </summary>
        /// <value></value>
        public string TitleTo { get; set; }
        /// <summary>
        /// Description From
        /// </summary>
        /// <value></value>
        public string DescriptionFrom { get; set; }
        /// <summary>
        /// Description To
        /// </summary>
        /// <value></value>
        public string DescriptionTo { get; set; }
    }
}