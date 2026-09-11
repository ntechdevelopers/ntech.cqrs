using MediatR;

namespace Ntech.CQRS.Application.Common.Wrappers;

public interface IRequestWrapper<TResponse> : IRequest<Response<TResponse>>
{
}
