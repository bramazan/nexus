using MediatR;
using Nexus.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Nexus.Application.Events.Commands
{
    public record NormalizeEventsCommand : IRequest<Unit>;

    public class NormalizeEventsCommandHandler : IRequestHandler<NormalizeEventsCommand, Unit>
    {
        private readonly INormalizationService _normalizationService;

        public NormalizeEventsCommandHandler(INormalizationService normalizationService)
        {
            _normalizationService = normalizationService;
        }

        public async Task<Unit> Handle(NormalizeEventsCommand request, CancellationToken cancellationToken)
        {
            await _normalizationService.NormalizePendingEventsAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
