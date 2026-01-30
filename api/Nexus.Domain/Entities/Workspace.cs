using Nexus.Domain.Common;
using System.Collections.Generic;

namespace Nexus.Domain.Entities
{
    public class Workspace : BaseEntity
    {
        public required string Name { get; set; }
    }
}
