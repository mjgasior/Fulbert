using System;

namespace Fulbert.Commons.Models.Entities
{
    public class BaseEntity
    {
        public virtual Guid Id { get; protected set; }
    }
}
