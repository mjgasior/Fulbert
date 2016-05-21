using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Fulbert.BLL.Services.Tests")]
namespace Fulbert.DAL.RepositoryModels.Models
{
    public class BaseEntity
    {
        public virtual Guid Id { get; protected set; }
    }
}
