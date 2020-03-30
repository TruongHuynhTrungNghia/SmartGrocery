using System;
using System.Runtime.Serialization;

namespace SmartGrocery.Model.Common
{
    public interface IEntity<TId>
    {
        TId Id { get; }

        void SetId(TId id);
    }

    public abstract class Entity : IEntity<Guid>
    {
        [IgnoreDataMember]
        public Guid Id { get; private set; }

        protected Entity()
        {
            Id = GenerateNewGuidId();
        }

        private Guid GenerateNewGuidId()
        {
            return Guid.NewGuid();
        }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}