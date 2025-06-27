namespace Fundo.Core.Interfaces
{
	public interface IRepository<T> where T: class
	{
		Task<T> AddAsync(T entity, CancellationToken cancellationToken);

		Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken);

		Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
	}
}
