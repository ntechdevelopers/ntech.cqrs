using MediatR;

namespace Ntech.CQRS.Application.Common.Wrappers;

public interface IHandlerWrapper<TRequest, TResponse> : IRequestHandler<TRequest, Response<TResponse>>
    where TRequest : IRequestWrapper<TResponse>
{
}
