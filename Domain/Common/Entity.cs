
using System;
using System.ComponentModel.DataAnnotations;
using Domain.Common.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Common
{

    public class Entity : Entitydm
    {
        [Key]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public virtual Guid Id { get; set; } = Guid.NewGuid();
        public override void AddDomainEvent<T>(string state, string idReference = "")
        {
            base.AddDomainEvent<T>(state, idReference == "" ? Id.ToString() : idReference);
        }
    }

}
