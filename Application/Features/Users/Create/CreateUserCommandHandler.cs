using Application.Contracts.Infrastructure;
using Application.Events;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Application.Features.Users.Create
{
    public class CreateUserCommandHandler(UserManager<User> userManager, IMapper mapper, IServiceBus serviceBus) : IRequestHandler<CreateUserCommand, ServiceResult<CreateUserResponse>>
    {
        public async Task<ServiceResult<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User() { FirstName = request.FirstName, LastName = request.LastName, Email = request.Email, UserName = request.UserName };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errorDescriptions = result.Errors.Select(x => x.Description).ToList();
                var errorDetail = string.Join(", ", errorDescriptions);
                return ServiceResult<CreateUserResponse>.Error("Kullanıcı oluşturma başarısız", errorDetail, HttpStatusCode.BadRequest);
            }

            await serviceBus.PublishAsync(new UserCreatedEvent(user.Id), cancellationToken);

            return ServiceResult<CreateUserResponse>.SuccessAsOk(mapper.Map<CreateUserResponse>(user));
        }
    }
}
