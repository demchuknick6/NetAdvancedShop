namespace NetAdvancedShop.Carting.Application.Common.Mappings;

public interface IMapper<TResult, TEntity>
{
    TResult Translate(TEntity entity);
}