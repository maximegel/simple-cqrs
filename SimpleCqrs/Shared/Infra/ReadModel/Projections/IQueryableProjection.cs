namespace SimpleCqrs.Shared.Infra.ReadModel.Projections;

public interface IQueryableProjection<out TModel> :
    IAsyncQueryable<TModel> 
{
}