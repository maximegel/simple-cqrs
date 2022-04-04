using System.Linq.Expressions;

namespace SimpleCqrs.Shared.Infra.ReadModel.Projections;

public abstract class QueryableProjection<TModel> :
    IQueryableProjection<TModel>
{
    public Type ElementType => AsyncQuery.ElementType;

    public Expression Expression => AsyncQuery.Expression;

    public IAsyncQueryProvider Provider => AsyncQuery.Provider;
    
    public IAsyncEnumerator<TModel> GetAsyncEnumerator(
        CancellationToken cancellationToken = default)
    {
        return AsyncQuery.GetAsyncEnumerator(cancellationToken);
    }

    protected abstract IAsyncEnumerable<TModel> Query { get; }

    private IAsyncQueryable<TModel> AsyncQuery => 
        Query.AsAsyncQueryable();
}