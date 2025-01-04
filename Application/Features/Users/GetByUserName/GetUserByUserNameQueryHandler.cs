using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Application.Features.Users.GetByUserName
{
    public class GetUserByUserNameQueryHandler(UserManager<User> userManager, IMapper mapper) : IRequestHandler<GetUserByUserNameQuery, ServiceResult<GetUserByUserNameResponse>>
    {
        public async Task<ServiceResult<GetUserByUserNameResponse>> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.UserName);

            if (user is null) return ServiceResult<GetUserByUserNameResponse>.Error("UserName not found", HttpStatusCode.NotFound);

            return ServiceResult<GetUserByUserNameResponse>.SuccessAsOk(mapper.Map<GetUserByUserNameResponse>(user));
        }
    }
}
