using BuildingBlocks.Domain.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain
{
    public abstract class Entity : IEntity<Guid>
    {
        private readonly Guid _id;


        public Entity()
        {
            // TODO: id generation
            _id = Guid.NewGuid();

            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = null;
        }

        public Entity(Guid id)
        {
            _id = id;
            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = null;
        }


        //public Guid Id => _id;

        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? ModifiedAt { get; protected set; }

        public Guid Id { get; set; }
    }
}
