using Application.Contracts.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Features.Auths.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork) : IRequestHandler<RevokeRefreshTokenCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            string refreshToken = request.RefreshToken;

            var existingRefreshToken = await refreshTokenRepository
                .Where(x => x.Code == refreshToken)
                .SingleOrDefaultAsync(cancellationToken);

            if (existingRefreshToken is null) return ServiceResult.Error("Refresh token not found", HttpStatusCode.NotFound);

            refreshTokenRepository.Delete(existingRefreshToken);

            await unitOfWork.SaveChangesAsync();

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
