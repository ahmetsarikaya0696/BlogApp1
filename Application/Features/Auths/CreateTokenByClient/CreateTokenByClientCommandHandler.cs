using Application.Contracts.Infrastructure;
using Application.Dtos;
using Domain.Configuration;
using MediatR;
using Microsoft.Extensions.Options;
using System.Net;

namespace Application.Features.Auths.CreateTokenByClient
{
    public class CreateTokenByClientCommandHandler(ITokenService tokenService, IOptions<List<Client>> options) : IRequestHandler<CreateTokenByClientCommand, ServiceResult<ClientTokenDto>>
    {
        private readonly List<Client> clients = options.Value;

        public async Task<ServiceResult<ClientTokenDto>> Handle(CreateTokenByClientCommand request, CancellationToken cancellationToken)
        {
            var client = clients.SingleOrDefault(x => x.Id == request.ClientId && x.Secret == request.ClientSecret);

            if (client is null)
                return await Task.FromResult(ServiceResult<ClientTokenDto>.Error("ClientId or ClientSecret not found", HttpStatusCode.NotFound));

            var token = tokenService.CreateTokenByClient(client);

            return await Task.FromResult(ServiceResult<ClientTokenDto>.SuccessAsOk(token));
        }
    }
}
