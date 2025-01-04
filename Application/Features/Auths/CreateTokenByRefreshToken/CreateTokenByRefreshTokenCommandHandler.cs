using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Auths.CreateTokenByRefreshToken
{
    public class CreateTokenByRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, UserManager<User> userManager, ITokenService tokenService, IUnitOfWork unitOfWork) : IRequestHandler<CreateTokenByRefreshTokenCommand, ServiceResult<TokenDto>>
    {
        public async Task<ServiceResult<TokenDto>> Handle(CreateTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var existingRefreshToken = await refreshTokenRepository.Where(x => x.Code == request.RefreshToken).SingleOrDefaultAsync();

            if (existingRefreshToken is null) return ServiceResult<TokenDto>.Error("Refresh token not found", HttpStatusCode.NotFound);

            var user = await userManager.FindByIdAsync(existingRefreshToken.UserId);

            if (user is null) return ServiceResult<TokenDto>.Error("User not found", HttpStatusCode.NotFound);

            var tokenDto = tokenService.CreateToken(user);

            existingRefreshToken.Code = tokenDto.RefreshToken;
            existingRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await unitOfWork.SaveChangesAsync();

            return ServiceResult<TokenDto>.SuccessAsOk(tokenDto);
        }
    }
}
