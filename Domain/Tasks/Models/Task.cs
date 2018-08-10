using System;
using Flunt.Notifications;
using Flunt.Validations;

namespace ToDo.Domain.Tasks.Models
{
    /// <summary>
    /// Task
    /// </summary>
    public class Task : Notifiable, IValidatable
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; private set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// Status
        /// </summary>
        public Models.Status Status { get; private set; }
        /// <summary>
        /// Created At
        /// </summary>
        public DateTime CreatedAt { get; private set; }
        /// <summary>
        /// Updated At
        /// </summary>
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="title">Title</param>
        /// <param name="description">Description</param>
        /// <param name="status">Status</param>
        /// <param name="createdAt">Created At</param>
        /// <param name="updatedAt">Updated At</param>
        public Task(Guid id, string title, string description, Status status, DateTime createdAt, DateTime updatedAt)
        {   
            this.ChangeTitle(title);
            this.ChangeDescription(description);
            this.ChangeStatus(status);
            
            this.Id = id;
            this.CreatedAt = createdAt;
            this.UpdatedAt = updatedAt;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="description">Description</param>
        public Task(string title, string description) 
            : this(Guid.NewGuid(), title, description, Status.Active, DateTime.Now, DateTime.Now)
        {
        }

        /// <summary>
        /// Change Title
        /// </summary>
        /// <param name="title">New Title</param>
        public void ChangeTitle(string title)
        {
            this.Title = title;
            this.UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// Change Description
        /// </summary>
        /// <param name="description">New Description</param>
        public void ChangeDescription(string description)
        {
            this.Description = description;
            this.UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// Change Status
        /// </summary>
        /// <param name="status">New Status</param>
        public void ChangeStatus(Status status)
        {
            this.Status = status;
            this.UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// Validate Task
        /// </summary>
        public void Validate()
        {
            var contract = new Contract()
                .HasMaxLen(this.Title, 40, nameof(this.Title), "Title should have no more than 40 chars")
                .HasMaxLen(this.Description, 250, nameof(this.Description), "Description should have no more than 250 chars");
            this.AddNotifications(contract);
        }
    }
}