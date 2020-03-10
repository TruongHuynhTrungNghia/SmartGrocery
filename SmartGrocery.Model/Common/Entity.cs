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

        protected void InitializedId()
        {
            Id = GenerateNewGuidId();
        }

        private Guid GenerateNewGuidId()
        {
            return new Guid();
        }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}