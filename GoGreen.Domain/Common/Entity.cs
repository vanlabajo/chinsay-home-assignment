using System;

namespace GoGreen.Domain.Common
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public DateTime CreatedTime { get; protected set; }
        public DateTime LastUpdatedTime { get; protected set; }
        public byte[] RowVersion { get; protected set; }
    }
}
