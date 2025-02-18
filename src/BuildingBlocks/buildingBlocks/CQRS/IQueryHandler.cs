using MediatR;

namespace buildingBlocks.CQRS
{
    internal interface IQueryHandler
    {
        public interface IQueryHandler<in TQuery, TResponse>
        : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        where TResponse : notnull
        {
        }
    }
}