using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Domain.Common
{

    public class Entitydm : IHasDomainEvent
    {

        //public virtual Guid Id { get; set; } = Guid.NewGuid();
        //public virtual int Id { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [NotMapped]
        public List<DomainEvent>? DomainEvents { get; set; } = new List<DomainEvent>();
        public virtual void AddDomainEvent<T>(string state, string idReference = "")
        {
            //se crea el evento de dominio
            DomainEvents.Add(new DomainEventDispatcher<T>(this, state, idReference));
        }
    }

    public class DomainEventDispatcher<T> : DomainEvent
    {
        public DomainEventDispatcher(object item, string state, string idReference) : base()
        {
            if (item != null)
            {
                ObjItem = (T)item;
                Item = item;
                ItemStr = JsonSerializer.Serialize(Item);
            }
            State = state;
            IdReference = idReference;
            Name = typeof(T).Name;
        }
        public T ObjItem { get; protected set; }
    }
}
