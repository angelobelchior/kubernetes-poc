using Flunt.Notifications;

namespace ToDo.Domain.Tasks.Models
{
    /// <summary>
    /// Result of Notification
    /// </summary>
    public class Result :  Notifiable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Result(){}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property">Property</param>
        /// <param name="description">Description</param>
        public Result(string property, string description)
            => this.AddNotification(property, description);
    }
}