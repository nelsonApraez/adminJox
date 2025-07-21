using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{

    public class EntityBase : Entitydm
    {
        [Key]
        public virtual int Id { get; set; }
    }

}
