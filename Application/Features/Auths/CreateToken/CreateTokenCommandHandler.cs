using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Auths.CreateToken
{
    public class CreateTokenCommandHandler(UserManager<User> userManager, IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService, IUnitOfWork unitOfWork) : IRequestHandler<CreateTokenCommand, ServiceResult<TokenDto>>
    {
        public async Task<ServiceResult<TokenDto>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null) return ServiceResult<TokenDto>.Error("E-mail veya password yanlış", HttpStatusCode.BadRequest);

            if (!await userManager.CheckPasswordAsync(user, request.Password))
                return ServiceResult<TokenDto>.Error("E-mail veya password yanlış", HttpStatusCode.BadRequest);

            var token = tokenService.CreateToken(user);

            if (token is null) return ServiceResult<TokenDto>.Error("Token bulunamadı", HttpStatusCode.NotFound);

            var refreshToken = await refreshTokenRepository.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (refreshToken is null)
            {
                await refreshTokenRepository.AddAsync(new RefreshToken() { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }
            else
            {
                refreshToken.Code = token.RefreshToken;
                refreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await unitOfWork.SaveChangesAsync();

            return ServiceResult<TokenDto>.SuccessAsOk(token);
        }
    }
}
