using System.Threading;
using System.Threading.Tasks;

namespace Nexus.Application.Common.Interfaces
{
    public interface INormalizationService
    {
        Task NormalizePendingEventsAsync(CancellationToken cancellationToken);
    }
}
