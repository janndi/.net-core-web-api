using Application.Handlers.CommandHandlers;
using Application.Handlers.QueryHandlers;
using Application.Requests.Commands;
using Application.Requests.Queries;
using Domain.Models.DTO;
using Domain.ResultTypes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Application.Extention
{
    public static class AppDependencyResolver
    {
        public static void RegisterDataServices(this IServiceCollection services)
        {
            //Auth
            services.AddScoped<IRequestHandler<GenerateTokenCommand, IS4ApiResponse>, GenerateTokenHandler>()
                    .AddScoped<IRequestHandler<RefreshTokenCommand, IS4ApiResponse>, RefreshTokenHandler>()
                    .AddScoped<IRequestHandler<RevokeTokenCommand, BooleanResult>, RevokeTokenHandler>();

            ////User
            services.AddScoped<IRequestHandler<GetSingleUserQuery, UserDTO>, GetSingleUserQueryHandler>()
                    .AddScoped<IRequestHandler<GetAllUserQuery, IEnumerable<UserDTO>>, GetAllUserQueryHandler>()
                    .AddScoped<IRequestHandler<CreateUserCommand, UserDTO>, CreateUserHandler>()
                    .AddScoped<IRequestHandler<UpdateUserCommand, UserDTO>, UpdateUserHandler>()
                    .AddScoped<IRequestHandler<DeleteUserCommand, BooleanResult>, DeleteUserHandler>()
                    .AddScoped<IRequestHandler<DeleteBatchUserCommand, BooleanResult>, DeleteBatchUserHandler>();
        }
    }
}
