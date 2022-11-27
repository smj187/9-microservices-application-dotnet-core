using BuildingBlocks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain
{
    public abstract class EntityBase : IEntityBase
    {
        private Guid _id;
        private DateTimeOffset _createdAt;
        private DateTimeOffset? _modifiedAt;
        private bool _isDeleted;

        protected EntityBase() { }

        protected EntityBase(Guid id)
        {
            _id = id;
        }

        public virtual Guid Id
        {
            get
            {
                return _id;
            }
            protected set
            {
                _id = value;
            }
        }

        public virtual DateTimeOffset CreatedAt
        {
            get => _createdAt;
            protected set => _createdAt = value;
        }

        public virtual DateTimeOffset? ModifiedAt
        {
            get => _modifiedAt;
            protected set => _modifiedAt = value;
        }

        public virtual void Modify()
        {
            _modifiedAt = DateTimeOffset.UtcNow;
        }

        public virtual bool IsDeleted
        {
            get => _isDeleted;
            protected set => _isDeleted = value;
        }
    }
}