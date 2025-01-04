using Application.Contracts.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Auths.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork) : IRequestHandler<RevokeRefreshTokenCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var existingRefreshToken = await refreshTokenRepository.Where(x => x.Code == request.RefreshToken).SingleOrDefaultAsync();

            if (existingRefreshToken is null) return ServiceResult.Error("Refresh token not found", HttpStatusCode.NotFound);

            refreshTokenRepository.Delete(existingRefreshToken);

            await unitOfWork.SaveChangesAsync();

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
