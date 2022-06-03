using BuildingBlocks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain
{
    public abstract class Entity : IEntity
    {
        private Guid _id;
        private DateTimeOffset _createdAt;
        private DateTimeOffset? _modifiedAt;
        private bool _isDeleted;

        //public Entity()
        //{
        //    CreatedAt = DateTimeOffset.UtcNow;
        //    ModifiedAt = null;

        //    _id = Guid.NewGuid();
        //}




        public Guid Id
        {
            get => _id;
            protected set => _id = value;
        }

        public DateTimeOffset CreatedAt
        {
            get => _createdAt;
            protected set => _createdAt = value;
        }

        public DateTimeOffset? ModifiedAt
        {
            get => _modifiedAt;
            protected set => _modifiedAt = value;
        }
        public bool IsDeleted
        {
            get => _isDeleted;
            protected set => _isDeleted = value;
        }



    }
}
