using Flunt.Notifications;

namespace IWantApp.Domain.Products
{
    public abstract class Entity : Notifiable<Notification>
    {

        /// <summary>
        /// Constructor that generates a new GUID when the class is instantiated
        /// </summary>

        public Entity()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// class proprieties to inherit(herdadas)
        /// </summary> 
        
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string CreatedBy { get; set; } 
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedOn { get; set; } = null;


    }
}
